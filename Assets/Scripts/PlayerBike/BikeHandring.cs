using UnityEngine;

public class BikeHandring : MonoBehaviour
{
    //ハンドリングは車体のY軸とZ軸を傾けることで表現する
    //左曲がりはY軸がマイナスのZ軸がプラス
    //右曲がりはY軸がプラスのZ軸がマイナス
    [SerializeField] private BaseBike _baseBike = default;
    [SerializeField] private GameObject _playerBike = default;
    private float _additionValue = 0.0f;
    private const float RETURNADDVALUE = 1.0f;
    private const float TORELANCE = 0.5f; //許容範囲
    private float _zero = 0.0f;
    private const float FirstZRotation = 0.0f;
    [SerializeField] Rigidbody _rigidBody = default;
    private float _currentZ = 0;
    private float _yawRoll = -90;

    private void Awake()
    {
        Invoke("GetAdditionValue", 0.1f);
    }
    private void GetAdditionValue()
    {
        _additionValue = _baseBike.HandringAdditionValue;
    }
    private void FixedUpdate()
    {
        InputHandring();
        //RotationFix();
    }

    private void InputHandring()
    {
        if (Input.GetAxis("Horizontal") > _zero) //右曲がり
        {
            _yawRoll += _additionValue;
            _currentZ -= _additionValue;
        }
        else if (Input.GetAxis("Horizontal") < _zero) //左曲がり
        {
            _yawRoll -= _additionValue;
            _currentZ += _additionValue;
        }
        else //入力無し
        {
            bool isRightRotate = NormalizeAngle(_currentZ) < FirstZRotation - TORELANCE;
            bool isLeftRotate = NormalizeAngle(_currentZ) > FirstZRotation + TORELANCE;
            bool isStraight = Mathf.Abs(NormalizeAngle(_currentZ) - FirstZRotation) <= TORELANCE;
            if (isRightRotate)
            {
                _currentZ += RETURNADDVALUE;
            }
            else if (isLeftRotate)
            {
                _currentZ -= RETURNADDVALUE;
            }
        }
        _currentZ = Mathf.Clamp(_currentZ, -60f, 60f);
        Quaternion rotation = Quaternion.Euler(0, _yawRoll, _currentZ);
        _rigidBody.MoveRotation(rotation);
        //_rigidBody.MoveRotation(Quaternion.Euler(worldAngle));

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

    private void RotationFix()
    {
        float zRotation = NormalizeAngle(transform.eulerAngles.z);

        if (zRotation >= 60f || zRotation <= -60f)
        {
            //_baseBike.EngineStop();
            Vector3 fixedEuler = transform.eulerAngles;
            fixedEuler.z = 0f;
            _rigidBody.rotation = Quaternion.Euler(fixedEuler);
            //transform.eulerAngles = fixedEuler;
        }
    }

}

