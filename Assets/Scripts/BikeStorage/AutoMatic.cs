using Unity.VisualScripting;
using UnityEngine;

public class AutoMatic : BaseBike
{
    private float _attenuation = 0.8f;
    //private float _accelHoldTime = 0.0f;
    [SerializeField] private const float GEARUPTIME = 5.0f;
    [SerializeField, Header("ギアを変える時のクールダウン")]
    private float _gearCoolDown = 3;
    private float _currentGearCoolTime = 0;
    private float _curNotHoldAxelTime = 0.0f;   
    private bool _isHoldAxel = false;  
    private void Start()
    {
        for (int i = 0; i < _status.GearSpeeds.Count; i++)
        {
            _gearSpeeds.Add(_status.GearSpeeds[i]);
        }

        _attenuationRate = _attenuation;
        _currentGearIndex = 1;
    }

    private void FixedUpdate()
    {
        //_clutchValue = 1.0f;
        MoveForward();
    }

    public override void MoveForward()
    {
        base.MoveForward();
        _currentGearCoolTime += Time.fixedDeltaTime;
        AutoGearDown();
        if(_axelValue > 0)
        {
            _isHoldAxel = true;
        }
        else
        {
            _isHoldAxel = false;
        }

        bool canChangeGear = _currentGearCoolTime >= _gearCoolDown;
        //Debug.Log("現在のクールタイムは" + _currentGearCoolTime);
        float gearConnectValue = _status.GearMaxSpeeds[_currentGearIndex] * _status.SuccessGearChangeRatio;
        if (!canChangeGear)
        {
            return;
        }
        if (CalcCurrentBikeSpeed() >= gearConnectValue)
        {
            _currentGearCoolTime = 0;
            Debug.Log("オートマがギアアップ");
            UpGear();
        }


    }

    private void AutoGearDown()
    {
        if (!_isHoldAxel)
        {
            _curNotHoldAxelTime += Time.fixedDeltaTime;
        }
        if (_currentGearIndex <= 1)
        {
            return;
        }
        if (_curNotHoldAxelTime >= _gearCoolDown)
        {
            _curNotHoldAxelTime = 0;
            Debug.Log("オートマがギアダウン");
            DownGear();
            _currentGearCoolTime = 0;
        }

    }

}
