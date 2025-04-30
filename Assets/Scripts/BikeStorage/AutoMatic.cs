using UnityEngine;

public class AutoMatic : BaseBike
{
    private float[] _speeds = new float[] { 0.2f, 0.0f, 0.5f, 0.7f, 1.3f, 1.9f, 2.5f };
    private float _handring = 0.6f;
    private float _attenuation = 0.8f;
    private float _accelHoldTime = 0.0f;
    [SerializeField] private const float GEARUPTIME = 5.0f;
    private void Awake()
    {
        _gearSpeeds = _speeds;
        _handringAdditionValue = _handring;
        _attenuationRate = _attenuation;
        _currentGearIndex = 0;
    }

    private void FixedUpdate()
    {
        _clutchValue = 1.0f;
        MoveForward();
        if(_clutchValue != 0.0f && _currentGearIndex == 1)
        {
            _currentGearIndex = 0;
        }
    }

    public override void MoveForward()
    {
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _axelValue);
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                    _currentGearIndex = 1;
                }
                break;

            default:
                if (_axelValue <= 0)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                    _currentGearIndex = 0;
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _axelValue);
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

    }

}
