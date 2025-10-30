using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIReturnPrevPosition : MonoBehaviour
{
    private AIBikeController _controller = default;

    private void Awake()
    {
        _controller = GetComponent<AIBikeController>();
    }

    public void Jugemu()
    {
        Debug.Log("前のポイントに戻るよ");
        Vector3 prevWaypoint = _controller.WayPoints[_controller.CurrentWaypointIndex - 1].position;
        this.transform.position = prevWaypoint;
        _controller.PointDeviationReset();
    }
}
