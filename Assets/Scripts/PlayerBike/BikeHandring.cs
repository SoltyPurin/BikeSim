using UnityEngine;

public class BikeHandring : MonoBehaviour
{
    //ハンドリングは車体のY軸とZ軸を傾けることで表現する
    //左曲がりはY軸がマイナスのZ軸がプラス
    //右曲がりはY軸がプラスのZ軸がマイナス
    #region Serialize変数
    [SerializeField] private BaseBike _baseBike = default;
    [SerializeField,Header("バイク本体の見た目")]
    private GameObject _bikeObject = default;
    [SerializeField, Header("ステータスを読み込むScriptableObject")]
    private BikeStatus _status = default;
    [SerializeField,Header("上のリジッドボディ")]
    private Rigidbody _onBallRigidBody = default;

    #endregion
    #region 変数
    private float _zero = 0.0f;
    private float _currentZ = 0;
    private float _yawRoll = 0;
    private float _prevY = 0;
    private float _zClampValue = 60;
    private float _yReturnAddValue ;
    private float _horizontalValue = 0;
    private float _initialY;
    #endregion
    #region 定数
    private const float TORELANCE = 0.5f; //許容範囲
    private const float FIRST_Z_ROTATION = 0.0f;
    private const float Z_RETURN_ADDVALUE = 1.0f;
    private readonly string HORIZONTAL = "Horizontal";

    #endregion

    private void Awake()
    {
        _yReturnAddValue = _status.CurveAddValue;
        _initialY = _onBallRigidBody.transform.eulerAngles.y;
        //_yawRoll = _onBallRigidBody.transform.rotation.eulerAngles.y;
        //_prevY = _onBallRigidBody.transform.rotation.eulerAngles.y;
    }
    private void FixedUpdate()
    {
        InputHandring();
    }

    private void InputHandring()
    {
        _horizontalValue = Input.GetAxis(HORIZONTAL);
        if (Input.GetAxis(HORIZONTAL) > _zero) //右曲がり
        {
            //_prevY += _yReturnAddValue;
            _currentZ -= Z_RETURN_ADDVALUE;
        }
        else if (Input.GetAxis(HORIZONTAL) < _zero) //左曲がり
        {
            //_prevY -= _yReturnAddValue;
            _currentZ += Z_RETURN_ADDVALUE;
        }
        else //入力無し
        {
            bool isRightRotate = NormalizeAngle(_currentZ) < FIRST_Z_ROTATION - TORELANCE;
            bool isLeftRotate = NormalizeAngle(_currentZ) > FIRST_Z_ROTATION + TORELANCE;
            bool isStraight = Mathf.Abs(NormalizeAngle(_currentZ) - FIRST_Z_ROTATION) <= TORELANCE;
            if (isRightRotate)
            {
                _currentZ += Z_RETURN_ADDVALUE;

            }
            else if (isLeftRotate)
            {
                _currentZ -= Z_RETURN_ADDVALUE;
            }
        }
        _yawRoll = _prevY;
        _currentZ= Mathf.Clamp(_currentZ, -_zClampValue, _zClampValue);
        float initZ = transform.rotation.eulerAngles.z;
        float yaw = _onBallRigidBody.transform.eulerAngles.y - _initialY;

        //_bikeObject.transform.rotation = Quaternion.Euler(0,0, _currentZ);
        _bikeObject.transform.localRotation = Quaternion.Euler(0, 0, _currentZ);
        //Quaternion rotation = Quaternion.Euler(0, _yawRoll, initZ);
        //_onBallRigidBody.MoveRotation(rotation);
        _onBallRigidBody.AddTorque(Vector3.up * (_horizontalValue * 10));
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

