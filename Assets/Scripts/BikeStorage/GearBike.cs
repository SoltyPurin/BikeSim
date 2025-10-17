using UnityEngine;

public class GearBike : BaseBike
{
    private void Awake()
    {
        for (int i = 0; i < _status.GearSpeeds.Count; i++)
        {
            _gearSpeeds.Add(_status.GearSpeeds[i]);
            Debug.Log(i + "速のスピードは" + _gearSpeeds[i]);
        }

        //_gearChangeCoolTime = 0.3f;
        //_attenuationRate = 0.9f;
        _gearChangeCoolTime = _status.GearChangeCoolTime;
        _attenuationRate = _status.AttemiationRate;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }


}
