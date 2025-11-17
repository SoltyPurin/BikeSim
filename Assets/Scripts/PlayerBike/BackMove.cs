using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMove : MonoBehaviour
{
    [SerializeField, Header("下がるときの速度")]
    private float _backSpeed = 10f;
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;

    public void MoveBack(float backValue)
    {
        Vector3 force = (transform.forward * _backSpeed * backValue) * -1;
        _ballRigidBody.AddForce(force);
    }
}
