using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWallCollision : MonoBehaviour
{
    [SerializeField, Header("何km以上で突っ込んできたらクラッシュ判定にするか")]
    private float _crashJudgementSpeed = 40;
    [SerializeField, Header("ボールのリジッドボディ")]
    private Rigidbody _ballRigidBody = default;
    private PlayerCatcher _catcher = default;
    private Rigidbody _onBallRigidBody = default;

    private float _lastVelocity = 0;

    private void Start()
    {
        _onBallRigidBody = GetComponent<Rigidbody>();
        _catcher = GameObject.FindWithTag("Observer").GetComponent<PlayerCatcher>();
    }

    private void FixedUpdate()
    {
        _lastVelocity = _onBallRigidBody.velocity.magnitude;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("壁に接触" + _lastVelocity);
        //Debug.Log("ぶつかった物のタグは" + collision.gameObject.tag);
        //Debug.Log("ぶつかった物は" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("壁に追突");
            ClashJudgementProtocol(_lastVelocity);
        }
    }

    private void ClashJudgementProtocol(float prevSpeed)
    {
        if(prevSpeed >= _crashJudgementSpeed)
        {
            _ballRigidBody.velocity = Vector3.zero;
            _catcher.WarpTheNearWayPoint(this.gameObject);
        }
    }
}
