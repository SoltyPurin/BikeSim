using UnityEngine;

public class AutoMatic : BaseBike
{
    private float[] _speeds = new float[] { 3.2f, 0.0f, 3.5f, 3.7f, 5.3f, 5.9f, 6.6f };
    private float _handring = 0.6f;
    private float _attenuation = 0.8f;
    private float _accelHoldTime = 0.0f;
    [SerializeField] private const float GEARUPTIME = 5.0f;
    [SerializeField, Header("ギアをリセットする速度")]
    private float _gearResetThreshold = 10;
    private bool _isHoldAxel = false;  
    private void Awake()
    {
        _gearSpeeds = _speeds;
        _handringAdditionValue = _handring;
        _attenuationRate = _attenuation;
        _currentGearIndex = 0;
    }

    private void FixedUpdate()
    {
        //_clutchValue = 1.0f;
        MoveForward();
        if(_clutchValue != 0.0f && _currentGearIndex == 1)
        {
            _currentGearIndex = 0;
        }
    }

    public override void MoveForward()
    {
        Vector3 force = transform.forward;
        if(_axelValue > 0)
        {
            _isHoldAxel = true;
        }
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
                    _rigidBody.AddForce(force);
                    //transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _axelValue);
                }
                else
                {
                    force = (transform.forward * _gearSpeeds[0] * _attenuationRate * _axelValue);
                    _rigidBody.AddForce(force);
                    //transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                    _accelHoldTime += Time.deltaTime;
                }
                break;

            default:
                if (_axelValue <= 0)
                {
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _attenuationRate/* * _axelValue*/);
                    _rigidBody.AddForce(force);
                    //transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                    //_currentGearIndex = 0;
                    _isHoldAxel = false;
                }
                else
                {
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
                    _rigidBody.AddForce(force);
                    //transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _axelValue);
                    _attenuationRate = ORIGINATTENUATIONVALUE;
                    _accelHoldTime += Time.deltaTime;
                }
                _isFirst = false;

                break;
        }
        if(_accelHoldTime >= GEARUPTIME)
        {
            if(_currentGearIndex == 0)
            {
                _currentGearIndex = 2;
            }
            else
            {
                UpGear();
            }
                _accelHoldTime = 0;
            Debug.Log("ギアチェンジ");
        }

        if(!_isHoldAxel && _rigidBody.velocity.magnitude <= _gearResetThreshold)
        {
            Debug.Log("ギアリセット");
            _currentGearIndex = 0;
        }

        Debug.Log(_isHoldAxel);

    }

}
