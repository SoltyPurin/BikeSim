using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBikeController : MonoBehaviour,IAiInitializer,IAIUpdater
{
    private enum EnemyState
    {
        PlayerFront,
        BehindThePlayer,
        NoChange,
    }
    private EnemyState _state = EnemyState.NoChange;
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
    //[SerializeField, Header("どれくらいの角度差までなら進行方向が合致しているかの数字")]
    //private float _valueWithAllowableError = 40f;
    [SerializeField, Header("どれくらいの時間プレイヤーの前にいたら減速するか")]
    private float _downSpeedTime = 10f;
    [SerializeField, Header("どれくらいプレイヤーの後ろにいたら強制的にギアを上げるか")]
    private float _forceGearUpTime = 5f;
    [SerializeField,Header("どれくらいウェイポイントに近づいたら通過判定にするか")]
    private float _reachThreshold = 10f;
    [SerializeField, Header("ブレーキの時どれくらい値を加算するか")]
    private float _brakeAddValue = 2;

    private AIMesureDistanceToPlayer _mesureDistance = default;
    private AIDetectGearChangeCurve _detectCurve = default;
    private AIGearChange _gearChange = default;
    private AICompareWhitchSidePlayer _frontAndBack = default;
    private AIBrake _brake = default;
    private float _currentPlayerBehindTime = 0;
    private float _personalHandlingSpeed;
    private float _randomWaypointDeviationsX;
    private float _randomWaypointDeviationsZ;
    private Vector3 _waypointDeviationOffset;
    private float _currentAxelValue = 0;
    private float _currentPlayerFrontTime = 0;
    private bool _isFrontPlayer = false;
    private float _brakeValue = 0;

    private int _currentWaypointIndex = 0;
    public int CurrentWaypointIndex
    {
        get { return _currentWaypointIndex; }
    }

    private List<Transform> _waypoints = new List<Transform>();
    public List<Transform> WayPoints
    {
        get { return _waypoints; }
    }


    private readonly string PLAYER_TAG = "Player";
    private readonly string WAYPOINT_TAG = "WayPoint";
    public void Initialize()
    {
        if(_wayPointsParent == null)
        {
            _wayPointsParent = GameObject.FindGameObjectWithTag(WAYPOINT_TAG).GetComponent<Transform>();
        }
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
        _brake = GetComponent<AIBrake>();
        _brake.Initialize();
        _gearChange = GetComponent<AIGearChange>();
        _mesureDistance = GetComponent<AIMesureDistanceToPlayer>();
        _frontAndBack = GetComponent<AICompareWhitchSidePlayer>();
        GameObject playerObj = GameObject.FindWithTag(PLAYER_TAG);
        _mesureDistance.Initialize(playerObj);
        _frontAndBack.Initialize(playerObj);
        CheckCurve();
        ShiftUpProtocol();
    }



    public void InterfaceUpdate(ObservationPlayerNearWayPoint observation)
    {
        //Debug.Log("現在追跡しているウェイポイントは" + _currentWaypointIndex + ",プレイヤーに一番近いポイントは" + observation.MostPlayerNearPointIndex);
        switch (_state)
        {
            case EnemyState.PlayerFront:
                PlayerFrontProtocol();
                break;  
            case EnemyState.BehindThePlayer:
                BehindThePlayerProtocol();
                break;
            case EnemyState.NoChange:
                NoChangeProtocol();
                break;
        }

        Debug.Log("敵のステートは" +  _state); 
        //アクセルを徐々にふかす
        // 自動で前に進む処理
        _bike.MoveForward();

        HandleWaypointMovement();

        LongTimeCheckFront(observation);
        bool isSpeedEnough = _gearChange.MesureSpeed(_bike.Status.GearMaxSpeeds[_bike.CurrentGearIndex]);
        if (!isSpeedEnough)
        {
            return;
        }
        if (_isFrontPlayer)
        {
            ShiftUpProtocol();
        }
    }
    /// <summary>
    /// プレイヤーの前にいる時のプロトコル。やることはアクセルのダウン
    /// </summary>
    private void PlayerFrontProtocol()
    {
        AxelDown();
    }
    /// <summary>
    /// プレイヤーの後ろにいる時のプロトコル。やることはアクセルのアップ
    /// もし後ろすぎたら強制的にギアを上げる
    /// </summary>
    private void BehindThePlayerProtocol()
    {
        _bike.UpdateAxelValue(AxelPlus());
        _currentPlayerBehindTime += Time.fixedDeltaTime;
        if(_currentPlayerBehindTime >= _forceGearUpTime)
        {
            Debug.Log("強制ギアアップ");
            ShiftUpProtocol();
            _currentPlayerBehindTime = 0;
        }
    }
    /// <summary>
    /// 特に今の状況を変えたくない時のプロトコル。やることは不定
    /// </summary>
    private void NoChangeProtocol()
    {
        _bike.UpdateAxelValue(AxelPlus());
    }

    /// <summary>
    /// どれくらいの時間プレイヤーの前にいるかを計測する
    /// </summary>
    private void LongTimeCheckFront(ObservationPlayerNearWayPoint observation)
    {
        if (!_frontAndBack.IsCurrentFront(observation.MostPlayerNearPointIndex,_currentWaypointIndex))
        {
            Debug.Log("プレイヤーの方が前");
            _isFrontPlayer = true;
            _currentPlayerFrontTime = 0;
            _state = EnemyState.BehindThePlayer;
            return;
        }
        _currentPlayerFrontTime += Time.fixedDeltaTime;

        _isFrontPlayer = false;
        if (_downSpeedTime <= _currentPlayerFrontTime)
        {
            Debug.Log("長く前に居すぎたので下がります");
            _currentPlayerFrontTime = 0;
            ShiftDownProtocol();
            _state = EnemyState.PlayerFront;
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
        _currentAxelValue = Mathf.Clamp(_currentAxelValue, 1f, 100);
        return _currentAxelValue;
    }

    /// <summary>
    /// アクセルをふかすメソッド
    /// </summary>
    /// <returns></returns>
    private float AxelPlus()
    {
        _currentAxelValue += _axelPlusValue;
        _currentAxelValue = Mathf.Clamp(_currentAxelValue, 0.1f, 1);
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
                PointDeviationReset();
                _currentWaypointIndex = 0;
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
                //Debug.Log("カーブ無し!ヨシ！");
                ResetAxel();
                ShiftUpProtocol();
                _brakeValue = 0;
                //ResetAxel();
                break;

            case 1:
                //Debug.Log("カーブあるやんけ！");
                ShiftDownProtocol();
                BrakeProtocol();

                break;

            default:
                //Debug.Log("AIそのまま");
                break;
        }
    }

    private void ShiftUpProtocol()
    {
        //Debug.Log("AIギア上げる");
        _bike.UpGear();
    }

    private void ShiftDownProtocol()
    {
        if (_bike.CurrentGearIndex <= 1)
        {
            return;
        }
        //Debug.Log("AIギア下げる");
        _bike.DownGear();
    }

    private void BrakeProtocol()
    {
        Debug.Log("ブレーキ中");
        _brakeValue += _brakeAddValue * Time.fixedDeltaTime;
        _brake.BrakeProtocol(_brakeValue);
    }

    public void PointDeviationReset()
    {
        _personalHandlingSpeed = Random.Range(6, 15);
        _randomWaypointDeviationsX = Random.Range(-10, 10);
        _randomWaypointDeviationsZ = Random.Range(-10, 10);
        _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
    }

}
