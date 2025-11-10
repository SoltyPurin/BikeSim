using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField, Header("中心から見て前のどれくらいの座標にレイを出すか")]
    private float _rayStartDistance = 0.5f;
    [SerializeField, Header("レイの長さ")]
    private float _rayDistance = 1f;
    [SerializeField, Header("下げる値")]
    private float _downValue = 0.1f;
    [SerializeField, Header("上げる値")]
    private float _upValue = 0.001f;

    private int _roadLayer = 3;

    private readonly string ROAD_TAG = "Road";

    private void Start()
    {
        GameObject road = GameObject.FindWithTag(ROAD_TAG);
        _roadLayer = road.layer;
    }
    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;

        if (IsTouchTheGround())
        {
            currentPos.y += _upValue;
        }
        else
        {
            currentPos.y -= _downValue;
        }

        transform.position = currentPos;
    }

    private bool IsTouchTheGround()
    {
        RaycastHit hit;
        bool isTouchGround = false;
        Vector3 origin = transform.position+ transform.forward * _rayStartDistance;
        Vector3 direction = Vector3.down;
        if (Physics.Raycast(origin, direction, out hit,_rayDistance))
        {
            int hitObjLayer = hit.collider.gameObject.layer;
            if ((hitObjLayer ==_roadLayer))
            {
                isTouchGround = true;
            }
        }

        return isTouchGround;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + transform.forward * _rayStartDistance;
        Vector3 direction = Vector3.down * _rayDistance;
        Debug.DrawRay(origin, direction, Color.blue);
    }
}
