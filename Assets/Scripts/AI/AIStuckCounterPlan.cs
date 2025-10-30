using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AIStuckCounterPlan : MonoBehaviour
{
    [SerializeField, Header("前方検知のレイの長さ")]
    private float _rayDistance = 5;
    [SerializeField, Header("スタック判定とする時間")]
    private float _stuckCheckTime = 3f;
    [SerializeField,Header("検知するレイヤー")]
    private LayerMask _layerMask = default;

    private AIReturnPrevPosition _returnPos = default;
    private Vector3 _rayStartPoint = Vector3.zero;
    private float _currentFrontObstacleTime = 0;

    private void Awake()
    {
        _returnPos = GetComponent<AIReturnPrevPosition>();
    }

    private void FixedUpdate()
    {
        Vector3 origin = transform.position + _rayStartPoint;
        RaycastHit hit;
        bool isFront = Physics.Raycast(origin, transform.forward, out hit, _rayDistance, _layerMask, QueryTriggerInteraction.Ignore);
        if (isFront)
        {
            Debug.Log("スタック中");
            _currentFrontObstacleTime += Time.fixedDeltaTime;
        }
        else
        {
            Debug.Log("スタックじゃない");
            _currentFrontObstacleTime = 0;
        }

        if(_currentFrontObstacleTime > _stuckCheckTime)
        {
            _returnPos.Jugemu();
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + _rayStartPoint, transform.forward * _rayDistance, Color.red);

    }
}
