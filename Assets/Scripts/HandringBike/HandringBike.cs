using UnityEngine;

public class HandringBike : BaseBike
{
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.2f, 0.0f, 0.8f, 1.4f, 2.0f, 2.6f, 3.2f };
        _gearNames = new string[] { "1", "N", "2", "3", "4", "5", "6" };
        _currentGearIndex = 1; //Nからスタートさせるため
        _gearChangeCoolTime = 0.3f;
        _decelerationMultiplication = 0.995f;
        _handringAdditionValue = 0.8f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }


}
