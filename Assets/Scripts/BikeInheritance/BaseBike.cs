using UnityEngine;

public class BaseBike : MonoBehaviour
{
    protected float[] _gearSpeeds; //必ず速度を子クラスで設定する
    protected int _currentGearIndex;
    protected float _clutchValue;
    protected float _maxSpeed;

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
        transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue);
    }


    public virtual void UpdateClutchValue(float value)
    {
        _clutchValue = value;
    }

}
