using UnityEngine;

public class AutoMatic : BaseBike
{
    private float _attenuation = 0.8f;
    private float _accelHoldTime = 0.0f;
    [SerializeField] private const float GEARUPTIME = 5.0f;
    [SerializeField, Header("ギアを一段階下げるまでの時間")]
    private float _gearOneDownTime = 3;
    private float _curNotHoldAxelTime = 0.0f;   
    private bool _isHoldAxel = false;  
    private void Start()
    {
        for (int i = 0; i < _status.GearSpeeds.Count; i++)
        {
            _gearSpeeds.Add(_status.GearSpeeds[i]);
        }

        _attenuationRate = _attenuation;
        _currentGearIndex = 0;
    }

    private void FixedUpdate()
    {
        Debug.Log(_currentGearIndex);
        //_clutchValue = 1.0f;
        MoveForward();
    }

    public override void MoveForward()
    {
        base.MoveForward();
        Vector3 force = transform.forward;
        if(_axelValue > 0)
        {
            _isHoldAxel = true;
        }
        if (_isHoldAxel)
        {
            _accelHoldTime += Time.deltaTime;
        }
        if(_accelHoldTime >= GEARUPTIME)
        {

            UpGear();
            _accelHoldTime = 0;
            Debug.Log("ギアチェンジ");
        }

        if(!_isHoldAxel)
        {
            _curNotHoldAxelTime += Time.fixedDeltaTime;
        }
        if(_currentGearIndex <= 0)
        {
            return;
        }
        if(_curNotHoldAxelTime >= _gearOneDownTime)
        {
            Debug.Log("ギア一段階ダウン");
            _currentGearIndex--;
            _curNotHoldAxelTime = 0;    
        }
    }

}
