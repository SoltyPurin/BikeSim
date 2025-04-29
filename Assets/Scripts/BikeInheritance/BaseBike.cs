using UnityEngine;

public class BaseBike : MonoBehaviour
{
    protected float[] _gearSpeeds; //必ず速度を子クラスで設定する
    protected int _currentGearIndex = 1;
    public int CurrentGearIndex
    {
        get { return _currentGearIndex; }
    }
    protected float _gearChangeCoolTime;
    public float GearChangeCoolTime
    {
        get { return _gearChangeCoolTime; }
    }
    protected float _handringAdditionValue;
    public float HandringAdditionValue
    {
        get { return _handringAdditionValue; }
    }
    protected float _decelerationMultiplication = 0.98f; //_attenuationRateに乗算する値
    protected float _attenuationRate ; //惰性で動かすために速度に乗算する値、子クラスで書き換える
    protected float _clutchValue;
    protected float _maxSpeed;
    protected string[] _gearNames = new string[] { "1", "N", "2", "3", "4", "5", "6" };
protected const int NEUTRALGEARINDEX = 1;
    protected bool _isFirst = true;
    protected const float ORIGINATTENUATIONVALUE = 0.6f;
    protected float _clutchEngageThreshold = 0.2f;
    protected float _gearChangeTorelance = 0.7f; //しっかり半クラにしないとエンストするための変数


    public virtual void UpGear()
    {
        if(_currentGearIndex < _gearSpeeds.Length -1)
        {
            _currentGearIndex++;
        }
    }

    public virtual void DownGear()
    {
        if(_currentGearIndex > 0)
        {
            _currentGearIndex--;
        }
    }

    public virtual void MoveForward()
    {
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue);
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                }
                break;

            default:
                if (_clutchValue <= _clutchEngageThreshold)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;

                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue);
                    _attenuationRate = ORIGINATTENUATIONVALUE;
                }
                _isFirst = false;

                break;
        }

    }

    public virtual void EngineStop()
    {
        _currentGearIndex = 1;
    }


    public virtual void UpdateClutchValue(float value)
    {
        _clutchValue = value;
    }

}
