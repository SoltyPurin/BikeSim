using UnityEngine;

public class BikeHandring : MonoBehaviour
{
    //ハンドリングは車体のY軸とZ軸を傾けることで表現する
    //左曲がりはY軸がマイナスのZ軸がプラス
    //右曲がりはY軸がプラスのZ軸がマイナス
    [SerializeField] private BaseBike _baseBike = default;
    [SerializeField] private GameObject _playerBike = default;
    private const float LeftInclination = 25.0f;
    private const float RightInclination = -25.0f;
    private float _additionValue = 0.0f;
    private const float RETURNADDVALUE = 1.0f;
    private const float TORELANCE = 0.5f; //許容範囲
    private float _zero = 0.0f;
    private const float FirstYRotation = -90.0f;
    private const float FirstZRotation = 0.0f;
    [SerializeField] Rigidbody _rigidBody = default;

    private void Awake()
    {
        Invoke("GetAdditionValue", 0.1f);
    }
    private void GetAdditionValue()
    {
        _additionValue = _baseBike.HandringAdditionValue;
        Debug.Log(_additionValue);
    }
    private void FixedUpdate()
    {
        InputHandring();
    }

    private void InputHandring()
    {
        Vector3 worldAngle = transform.eulerAngles;
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
            bool isRightRotate = NormalizeAngle(worldAngle.z) < FirstZRotation - TORELANCE;
            bool isLeftRotate = NormalizeAngle(worldAngle.z) > FirstZRotation + TORELANCE;
            bool isStraight = Mathf.Abs(NormalizeAngle(worldAngle.z) - FirstZRotation) <= TORELANCE;
            if (isRightRotate)
            {
                worldAngle.z += RETURNADDVALUE;
            }
            else if (isLeftRotate)
            {
                worldAngle.z -= RETURNADDVALUE;
            }
        }
        _rigidBody.MoveRotation(Quaternion.Euler(worldAngle));

    }

    /// <summary>
    /// オイラー角の変換
    /// </summary>
    /// <param name="angle">バイクの角度</param>
    /// <returns>変換後の角度</returns>
    private float NormalizeAngle(float angle)
    {
        if (angle > 180.0f) angle -= 360;
        return angle;
    }
}

