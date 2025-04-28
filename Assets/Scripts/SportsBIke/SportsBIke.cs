using UnityEngine;
public class SportsBIke : BaseBike
{
    [SerializeField] private float boostMultiplier = 1.5f; //スポーツバイクの加速ブースト
    [SerializeField] private float _decelerationMultiplication = 0.99f;
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.6f, 0.0f, 1.2f, 1.8f, 2.4f, 3.0f, 3.6f };
        _gearNames = new string[] { "1", "N", "2", "3", "4", "5", "6" };
        _currentGearIndex = 1; //Nからスタートさせるため
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    public override void MoveForward()
    {
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue *boostMultiplier);
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                }
                break;

            default:
                if (_clutchValue <= 0.2f)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;

                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue *boostMultiplier);
                    _attenuationRate = ORIGINATTENUATIONVALUE;
                }
                _isFirst = false;

                break;
        }


    }

    public override void UpGear()
    {
        if(_currentGearIndex < _gearSpeeds.Length - 1)
        {
            _currentGearIndex++;
        }
    }

    public override void DownGear()
    {
        if(_currentGearIndex > 0)
        {
            _currentGearIndex--;
        }
    }

    public override void EngineStop()
    {
        _currentGearIndex = 1;
    }

    public override void UpdateClutchValue(float value)
    {
        _clutchValue = value;
    }
}
