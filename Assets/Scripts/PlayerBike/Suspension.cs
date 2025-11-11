using UnityEngine;

public class Suspension : MonoBehaviour
{
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField,Header("上に乗っけてるリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("重力")]
    private float _downForce = 5;

    private RaycastHit _hit;
    private float _sphereRadius = 0;

    private void Start()
    {
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
    }

    private void Update()
    {
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);
    }

    private void FixedUpdate()
    {
        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);

        Quaternion rota = Quaternion.Slerp(_onBallRigidBody.transform.rotation, Quaternion.FromToRotation(_onBallRigidBody.transform.up, _hit.normal) * _onBallRigidBody.transform.rotation, 0.02f);
        _onBallRigidBody.MoveRotation(rota);
    }
}

//[SerializeField, Header("中心から見て前のどれくらいの座標にレイを出すか")]
//private float _rayStartDistance = 0.5f;
//[SerializeField, Header("レイの長さ")]
//private float _rayDistance = 1f;
//[SerializeField, Header("下げる値")]
//private float _downValue = 0.1f;
//[SerializeField, Header("上げる値")]
//private float _upValue = 0.001f;

//private int _roadLayer = 3;
//private Rigidbody _rigidBody = default;

//private readonly string ROAD_TAG = "Road";

//private void Start()
//{
//    GameObject road = GameObject.FindWithTag(ROAD_TAG);
//    _roadLayer = road.layer;
//    _rigidBody = GetComponent<Rigidbody>();
//}
//private void FixedUpdate()
//{
//    Vector3 currentPos = transform.position;

//    if (IsTouchTheGround())
//    {
//        currentPos.y += _upValue;
//        transform.position = currentPos;
//    }
//    else
//    {
//        Debug.Log("落下");
//        _rigidBody.AddForce(Vector3.down * _downValue);
//    }

//}

//private bool IsTouchTheGround()
//{
//    RaycastHit hit;
//    bool isTouchGround = false;
//    Vector3 origin = transform.position+ transform.forward * _rayStartDistance;
//    Vector3 direction = Vector3.down;
//    if (Physics.Raycast(origin, direction, out hit,_rayDistance))
//    {
//        int hitObjLayer = hit.collider.gameObject.layer;
//        if ((hitObjLayer ==_roadLayer))
//        {
//            isTouchGround = true;
//        }
//    }

//    return isTouchGround;
//}

//private void OnDrawGizmos()
//{
//    Vector3 origin = transform.position + transform.forward * _rayStartDistance;
//    Vector3 direction = Vector3.down * _rayDistance;
//    Debug.DrawRay(origin, direction, Color.blue);
//}
