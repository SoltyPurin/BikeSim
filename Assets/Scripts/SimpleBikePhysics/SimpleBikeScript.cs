using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBikeScript : MonoBehaviour
{
    [SerializeField, Header("玉のリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    [SerializeField, Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;
    [SerializeField, Header("どれくらい加速しやすいか")]
    private float _accelarationValue = 0.5f;
    [SerializeField, Header("速度")]
    private float _speed = 50;
    [SerializeField, Header("重力")]
    private float _downForce = 5;

    private RaycastHit _hit;
    private float _verticalValue = 0.0f;
    private float _horizontalValue = 0.0f;
    private float _sphereRadius = 0;

    private void Start()
    {
        _sphereRadius = _ballRigidBody.gameObject.GetComponent<SphereCollider>().radius + 0.2f;
    }
    private void Update()
    {
        _verticalValue = Input.GetAxis("Vertical");
        _horizontalValue = Input.GetAxis("Horizontal");
        Physics.Raycast(_ballRigidBody.position, Vector3.down, out _hit, _sphereRadius);
    }

    private void FixedUpdate()
    {
        Vector3 curSpeed = Vector3.Lerp(_ballRigidBody.velocity, this.gameObject.transform.forward * _verticalValue * _speed, _accelarationValue);
        _ballRigidBody.velocity = curSpeed;
        _onBallRigidBody.AddTorque(Vector3.up * (_horizontalValue * 10));

        _ballRigidBody.AddForce(-transform.up * _downForce * _ballRigidBody.mass);

        //Lerpは現状の回転量と目的の回転量で、現状はRigidBody.rotationで出る
        //第一は上物のrotation、第二は↓のやつ
        Quaternion rota = Quaternion.Slerp(_onBallRigidBody.transform.rotation, Quaternion.FromToRotation(_onBallRigidBody.transform.up, _hit.normal) * _onBallRigidBody.transform.rotation, 0.02f);
        _onBallRigidBody.MoveRotation(rota);
    }
}
