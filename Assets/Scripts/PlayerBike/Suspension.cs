using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField, Header("中心から見て前のどれくらいの座標にレイを出すか")]
    private float _rayStartDistance = 0.5f;
    [SerializeField, Header("レイの長さ")]
    private float _rayDistance = 1f;
    [SerializeField, Header("下げる値")]
    private float _downValue = 0.1f;
    private void FixedUpdate()
    {
        if (IsTouchTheGround())
        {
            return;
        }

        Vector3 currentPos = transform.position;    
        currentPos.y -= _downValue;
        transform.position = currentPos;
    }

    private bool IsTouchTheGround()
    {
        RaycastHit hit;
        bool isTouchGround = false;
        Vector3 origin = transform.position;
        origin.x += _rayStartDistance;
        Vector3 direction = Vector3.down;
        if (Physics.Raycast(origin, direction, out hit,_rayDistance))
        {
            int hitObjLayer = hit.collider.gameObject.layer;
            if ((hitObjLayer ==3))
            {
                Debug.Log("接地");
                isTouchGround = true;
            }
            else
            {
                Debug.Log("飛んでる");
            }
        }

        return isTouchGround;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        origin.x += _rayStartDistance;
        Vector3 direction = Vector3.down * _rayDistance;
        Debug.DrawRay(origin, direction, Color.blue);
    }
}
