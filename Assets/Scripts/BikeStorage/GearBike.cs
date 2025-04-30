using UnityEngine;

public class GearBike : BaseBike
{
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.3f, 0.0f, 0.7f, 0.9f, 1.5f, 2.1f, 2.7f };
        _gearChangeCoolTime = 0.3f;
        _handringAdditionValue = 0.45f;
        _attenuationRate = 0.9f;
        _gearChangeTorelance = 0.9f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }


}
