using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBikeController : MonoBehaviour,IAiInitializer
{
    [SerializeField,Header("バイク本体のスクリプト")]
    private BaseBike _bike;
    [SerializeField, Header("ウェイポイントが登録してある親")]
    private Transform _wayPointsParent = default;
    [SerializeField, Header("ハンドリング精度の最低値")]
    private float _handringMinValue = 10;
    [SerializeField, Header("ハンドリング精度の最大値")]
    private float _handringMaxValue = 30;
    [SerializeField,Header("アクセルに追加していく値")]
    private float _axelPlusValue = 1;
    [SerializeField, Header("プレイヤーとどれくらい離れたらギアを下げるか決める距離")]
    private float _gearDownDistance = 10f;
    [SerializeField, Header("どれくらいの角度差までなら進行方向が合致しているかの数字")]
    private float _valueWithAllowableError = 40f;
    [SerializeField, Header("どれくらいの時間プレイヤーの前にいたら減速するか")]
    private float _downSpeedTime = 10f;
    private AIMesureDistanceToPlayer _mesureDistance = default;
    private AIDetectGearChangeCurve _detectCurve = default;
    private AIGearChange _gearChange = default;
    private AICompareWhitchSidePlayer _frontAndBack = default;
    private List<Transform> _waypoints = new List<Transform>();
    private int _currentWaypointIndex = 0;
    private float _personalHandlingSpeed;
    private float _reachThreshold = 10f;
    private float _randomWaypointDeviationsX;
    private float _randomWaypointDeviationsZ;
    private Vector3 _waypointDeviationOffset;
    private float _currentAxelValue = 0;
    private float _currentPlayerFrontTime = 0;

    private readonly string PLAYER_TAG = "Player";
    public void Initialize()
    {
        int wayPointCount = _wayPointsParent.childCount;
        for(int i = 0; i< wayPointCount; i++)
        {
            _waypoints.Add(_wayPointsParent.GetChild(i));
        }
        _randomWaypointDeviationsX = Random.Range(-10, 10);
        _randomWaypointDeviationsZ = Random.Range(-10, 10);
        _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
        _personalHandlingSpeed = Random.Range(_handringMinValue, _handringMaxValue);
        _detectCurve =GetComponent<AIDetectGearChangeCurve>();
        _detectCurve.Initialize();
        _gearChange = GetComponent<AIGearChange>();
        _mesureDistance = GetComponent<AIMesureDistanceToPlayer>();
        _frontAndBack = GetComponent<AICompareWhitchSidePlayer>();
        GameObject playerObj = GameObject.FindWithTag(PLAYER_TAG);
        _mesureDistance.Initialize(playerObj);
        _frontAndBack.Initialize(playerObj);
        CheckCurve();
    }



    private void FixedUpdate()
    {
        //アクセルを徐々にふかす
        _bike.UpdateAxelValue(AxelPlus());
        // 自動で前に進む処理
        _bike.MoveForward();

        if (_gearChange.MesureSpeed(_bike.Status.GearMaxSpeeds[_bike.CurrentGearIndex]))
        {
            ShiftUpProtocol();
        }
        HandleWaypointMovement();
        //if (_mesureDistance.MesureDistance(_gearDownDistance))
        //{
        //    _bike.UpdateAxelValue(AxelDown());
        //     ShiftDownProtocol();
        //}

        LongTimeCheckFront();

    }

    /// <summary>
    /// どれくらいの時間プレイヤーの前にいるかを計測する
    /// </summary>
    private void LongTimeCheckFront()
    {
        if (!_frontAndBack.IsCurrentFront(_waypoints[_currentWaypointIndex].position))
        {
            Debug.Log("プレイヤーの方が前");
            _currentPlayerFrontTime = 0;
            return;
        }
        _currentPlayerFrontTime += Time.fixedDeltaTime;

        if (_downSpeedTime <= _currentPlayerFrontTime)
        {
            Debug.Log("長く前に居すぎたので下がります");
            _currentPlayerFrontTime = 0;
            ShiftDownProtocol();
        }

    }

    /// <summary>
    /// アクセルを徐々に解除していく
    /// </summary>
    /// <returns>現在のアクセルの値</returns>
    private float AxelDown()
    {
        _currentAxelValue -= _axelPlusValue;
        _currentAxelValue *= 100;
        _currentAxelValue = Mathf.Clamp(_currentAxelValue, 0.1f, 100);
        return _currentAxelValue;
    }

    /// <summary>
    /// アクセルをふかすメソッド
    /// </summary>
    /// <returns></returns>
    private float AxelPlus()
    {
        _currentAxelValue += _axelPlusValue;
        _currentAxelValue *= 100;
        _currentAxelValue = Mathf.Clamp(_currentAxelValue, 0.1f, 100);
        return _currentAxelValue;
    }

    public void ResetAxel()
    {
        _currentAxelValue = 0.1f;
    }

    /// <summary>
    /// バイクを指定の方向に向かわせる
    /// </summary>
    private void HandleWaypointMovement()
    {
        //リスト外参照を防止するためリターン
        if (_waypoints == null || _waypoints.Count == 0) return;
        //初期化用ウェイポイントはスキップする
        if (_currentWaypointIndex == 0) 
        {
            _currentWaypointIndex++;
            return;
        }
        //目標地点を設定
        Transform targetPoint = _waypoints[_currentWaypointIndex].transform ;
        //目標地点をVector形式に変換、若干の揺らぎを作って目標地点をそれぞれ変わるようにする
        Vector3 targetVectorPoint = targetPoint.position + _waypointDeviationOffset ;
        //方向を求める
        Vector3 direction = (targetVectorPoint - transform.position).normalized;
        //目標に対する回転角度を求める
        Quaternion targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        //目標地点に対して徐々に回転させる
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime * _personalHandlingSpeed);

        //目標地点に辿り着いたときの処理
        if (Vector3.Distance(transform.position, targetVectorPoint) < _reachThreshold)
        {
            //次の目標地点がある場合は普通にインデックス+1
            if(_currentWaypointIndex < _waypoints.Count - 1)
            {
                _currentWaypointIndex++;

            }
            //配列の終点だった場合はインデックスをリセットし、揺らぎ及びハンドリングの制度を設定しなおす
            else
            {
                _currentWaypointIndex = 0;
                _personalHandlingSpeed = Random.Range(6, 15);
                _randomWaypointDeviationsX = Random.Range(-10, 10);
                _randomWaypointDeviationsZ = Random.Range(-10, 10);
                _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
            }
            CheckCurve();

        }

    }

    private void CheckCurve()
    {
        int curve = _detectCurve.CheckCurve(_waypoints[_currentWaypointIndex].position);
        switch (curve)
        {
            case 0:
                ShiftDownProtocol();
                ResetAxel();
                break;

            case 1:
                ResetAxel();
                ShiftUpProtocol();
                break;

            default:
                Debug.Log("AIそのまま");
                break;
        }
    }

    private void ShiftUpProtocol()
    {
        Debug.Log("AIギア上げる");
        _bike.UpGear();
    }

    private void ShiftDownProtocol()
    {
        if (_bike.CurrentGearIndex <= 1)
        {
            return;
        }
        Debug.Log("AIギア下げる");
        _bike.DownGear();
    }

}
