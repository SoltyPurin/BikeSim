using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBikeController : MonoBehaviour,IAiInitializer
{
    [SerializeField,Header("バイク本体のスクリプト")]
    private BaseBike _bike;
    [SerializeField,Header("クラッチのスクリプト")]
    private Clutch _aiClutch;
    [SerializeField,Header("目標地点を登録する")]
    private List<Transform> _waypoints;
    [SerializeField, Header("ハンドリング精度の最低値")]
    private float _handringMinValue = 10;
    [SerializeField, Header("ハンドリング精度の最大値")]
    private float _handringMaxValue = 30;
    private AIDetectGearChangeCurve _detectCurve = default;
    private int _currentWaypointIndex = 0;
    private float _personalHandlingSpeed;
    private float _reachThreshold = 10f;
    private float _randomWaypointDeviationsX;
    private float _randomWaypointDeviationsZ;
    private Vector3 _waypointDeviationOffset;
    private float _initClutchValue = 0;
    private float _initAxelValue = 0;
    public void Initialize()
    {
        _randomWaypointDeviationsX = Random.Range(-10, 10);
        _randomWaypointDeviationsZ = Random.Range(-10, 10);
        _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
        _personalHandlingSpeed = Random.Range(_handringMinValue, _handringMaxValue);
        _detectCurve =GetComponent<AIDetectGearChangeCurve>();
        _detectCurve.Initialize();
        CheckCurve();
    }



    private void FixedUpdate()
    {
        // 最初にクラッチを全開に離す
        _bike.UpdateClutchValue(ClutchPlus());

        //アクセルを徐々にふかす
        _bike.UpdateAxelValue(AxelPlus());
        // 自動で前に進む処理
        _bike.MoveForward();

        HandleWaypointMovement();

        //Debug.Log(_bike.CurrentGearIndex);
    }

    /// <summary>
    /// アクセルをふかすメソッド
    /// </summary>
    /// <returns></returns>
    private float AxelPlus()
    {
        _initAxelValue += 0.5f;
        _initAxelValue = Mathf.Clamp(_initAxelValue, 0.1f, 100);
        return _initAxelValue;
    }

    public void ResetAxel()
    {
        //if(_)
        _initAxelValue = 0;
    }

    /// <summary>
    /// クラッチを徐々に開いていく
    /// </summary>
    /// <returns>現在のクラッチレバーの値</returns>
    private float ClutchPlus()
    {
        if(_initClutchValue >0.5f &&  _initClutchValue < 0.7f)
        {
            _initClutchValue += 0.001f;
        }
        else
        {
            _initClutchValue += 0.01f;
        }
        _initClutchValue = Mathf.Clamp(_initClutchValue, 0, 1);
        return _initClutchValue;
    }

    public void ResetClutch()
    {
        _initClutchValue = 0;
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
                _bike.DownGear();
                if (_bike.CurrentGearIndex == 1)
                {
                    _bike.DownGear();
                }
                ResetClutch();
                ResetAxel();
                Debug.Log("AIギア下げる");
                break;

            case 1:
                _bike.UpGear();
                if (_bike.CurrentGearIndex == 1)
                {
                    _bike.UpGear();
                }
                ResetClutch();
                ResetAxel();
                Debug.Log("AIギア上げる");
                break;

            default:
                Debug.Log("AIそのまま");
                break;
        }
    }

}
