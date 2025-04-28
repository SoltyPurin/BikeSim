using UnityEngine;

public class BaseBike : MonoBehaviour
{
    protected float[] _gearSpeeds; //必ず速度を子クラスで設定する
    protected int _currentGearIndex;
    public int CurrentGearIndex
    {
        get { return _currentGearIndex; }
    }
    protected float _clutchValue;
    protected float _maxSpeed;
    protected string[] _gearNames;
    protected const int NEUTRALGEARINDEX = 1;
    protected bool _isFirst = true;
    [SerializeField] protected float _attenuationRate = 0.6f;
    protected const float ORIGINATTENUATIONVALUE = 0.6f;


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
                    _attenuationRate *= 0.98f;
                }
                break;

            default:
                if (_clutchValue <= 0.2f)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= 0.98f;

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
