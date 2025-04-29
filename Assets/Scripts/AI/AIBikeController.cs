using System.Collections.Generic;
using UnityEngine;

public class AIBikeController : MonoBehaviour
{
    [SerializeField] private BaseBike _bike;
    [SerializeField] private Clutch _aiClutch;
    [SerializeField] private List<Transform> _waypoints;
    private int _currentWaypointIndex = 0;
    private float _personalHandlingSpeed;
    private float _reachThreshold = 10f;
    private float _randomWaypointDeviationsX;
    private float _randomWaypointDeviationsZ;
    private Vector3 _waypointDeviationOffset;
    private void Start()
    {
        // 1速にギアを入れる
        _bike.DownGear();
        _randomWaypointDeviationsX = Random.Range(-10, 10);
        _randomWaypointDeviationsZ = Random.Range(-10, 10);
        _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
        _personalHandlingSpeed = Random.Range(6, 15);
    }



    private void FixedUpdate()
    {
        _aiClutch.SetClutchValue(1.0f);

        // 最初にクラッチを全開に離す
        _bike.UpdateClutchValue(1.0f);

        // 毎フレーム、自動で前に進む処理
        _bike.MoveForward();

        HandleWaypointMovement();
    }

    private void HandleWaypointMovement()
    {
        if (_waypoints == null || _waypoints.Count == 0) return; //リスト外参照を防止するため
        if (_currentWaypointIndex == 0) //初期化用ウェイポイントはスキップする
        {
            _currentWaypointIndex++;
            return;
        }

        Transform targetPoint = _waypoints[_currentWaypointIndex].transform ;
        Vector3 targetVectorPoint = targetPoint.position + _waypointDeviationOffset ;
        Vector3 direction = (targetVectorPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,Time.deltaTime * _personalHandlingSpeed);

        Debug.DrawRay(transform.position, direction * 10, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        if (Vector3.Distance(transform.position, targetVectorPoint) < _reachThreshold)
        {
            if(_currentWaypointIndex < _waypoints.Count - 1)
            {
                _currentWaypointIndex++;

            }
            else
            {
                _currentWaypointIndex = 0;
                _personalHandlingSpeed = Random.Range(6, 15);
                _randomWaypointDeviationsX = Random.Range(-10, 10);
                _randomWaypointDeviationsZ = Random.Range(-10, 10);
                _waypointDeviationOffset = new Vector3(_randomWaypointDeviationsX, 0, _randomWaypointDeviationsZ);
            }
        }
    }
}
