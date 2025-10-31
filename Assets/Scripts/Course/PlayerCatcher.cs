using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    [SerializeField, Header("プレイヤーに一番近いウェイポイントを持っているスクリプト")]
    private ObservationPlayerNearWayPoint _havingMostNearWaypointScript = default;
    [SerializeField, Header("ウェイポイントからどれだけ高度を上げて復活させるか")]
    private float _verticalPlusValue = 5.0f;
    private readonly string PLAYER_TAG = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            WarpTheNearWayPoint(other.gameObject);
        }
    }

    private void WarpTheNearWayPoint(GameObject playerObj)
    {
        Vector3 nearPos = _havingMostNearWaypointScript.NearPosition;
        nearPos.y += _verticalPlusValue;
        playerObj.transform.position = nearPos;
    }
}
