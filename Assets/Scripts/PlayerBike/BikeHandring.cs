using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeHandring : MonoBehaviour
{
    //ハンドリングは車体のY軸とZ軸を傾けることで表現する
    //左曲がりはY軸がマイナスのZ軸がプラス
    //右曲がりはY軸がプラスのZ軸がマイナス
    [SerializeField] private GameObject _playerBike = default;
    private const float LeftInclination = 25.0f;
    private const float RightInclination = -25.0f;
    private const float _additionValue = 0.5f;
    private const float _returnAddValue = 1.0f;
    private const float Tolerance = 0.5f; //許容範囲
    private float _zero = 0.0f;
    private const float FirstYRotation = -90.0f;
    private const float FirstZRotation = 0.0f;
    [SerializeField] Rigidbody _rigidBody = default;

    private void FixedUpdate()
    {
        InputHandring();
    }

    private void InputHandring()
    {
        Transform myTransform = this.transform;
        Vector3 worldAngle = myTransform.eulerAngles;
        if (Input.GetAxis("Horizontal") > _zero) //右曲がり
        {
            worldAngle.y += _additionValue;
            worldAngle.z -= _additionValue;
        }
        else if (Input.GetAxis("Horizontal") < _zero) //左曲がり
        {
            worldAngle.y -= _additionValue;
            worldAngle.z += _additionValue;
        }
        else //入力無し
        {
            bool isRightRotate = NormalizeAngle(worldAngle.z) < FirstZRotation - Tolerance;
            bool isLeftRotate = NormalizeAngle(worldAngle.z) > FirstZRotation + Tolerance;
            bool isStraight = Mathf.Abs(NormalizeAngle(worldAngle.z) - FirstZRotation) <= Tolerance;
            if (isRightRotate)
            {
                worldAngle.z += _returnAddValue;
            }
            else if (isLeftRotate)
            {
                worldAngle.z -= _returnAddValue;
            }
        }
        _rigidBody.MoveRotation(Quaternion.Euler(worldAngle));

    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180.0f) angle -= 360;
        return angle;
    }
}

