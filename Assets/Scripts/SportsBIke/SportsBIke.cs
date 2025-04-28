using UnityEngine;
public class SportsBIke : BaseBike
{
    [SerializeField] private float boostMultiplier = 1.5f; //スポーツバイクの加速ブースト
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.6f, 0.0f, 1.2f, 1.8f, 2.4f, 3.0f, 3.6f };
        _gearNames = new string[] { "1", "N", "2", "3", "4", "5", "6" };
        _currentGearIndex = 1; //Nからスタートさせるため
        _gearChangeCoolTime = 0.5f;
        _decelerationMultiplication = 0.99f;
        _handringAdditionValue = 0.3f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    //public override void UpGear()
    //{
    //    if(_currentGearIndex < _gearSpeeds.Length - 1)
    //    {
    //        _currentGearIndex++;
    //    }
    //}

    //public override void DownGear()
    //{
    //    if(_currentGearIndex > 0)
    //    {
    //        _currentGearIndex--;
    //    }
    //}

    //public override void EngineStop()
    //{
    //    _currentGearIndex = 1;
    //}

    public override void UpdateClutchValue(float value)
    {
        _clutchValue = value;
    }
}
