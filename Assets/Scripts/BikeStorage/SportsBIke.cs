using UnityEngine;
public class SportsBIke : BaseBike
{
    [SerializeField] private float boostMultiplier = 1.5f; //スポーツバイクの加速ブースト
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.6f, 0.0f, 1.2f, 1.8f, 2.4f, 3.0f, 3.6f };
        _gearChangeCoolTime = 0.5f;
        _handringAdditionValue = 0.3f;
        _attenuationRate = 0.6f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

}
