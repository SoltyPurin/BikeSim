using UnityEngine;

public class HandringBike : BaseBike
{
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.2f, 0.0f, 0.6f, 0.8f, 1.4f, 2.0f, 2.6f };
        _gearChangeCoolTime = 0.3f;
        _handringAdditionValue = 0.8f;
        _attenuationRate = 0.8f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }


}
