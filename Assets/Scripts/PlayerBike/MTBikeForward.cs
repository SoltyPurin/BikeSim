using UnityEngine;
using UnityEngine.UI;

public class MTBikeForward : MonoBehaviour
{
    //MTのバイクはクラッチを握らないと勝手に前に進む
    //ギアごとに最高速度が決まってる
    //左から順に1,N,2,3,4,5,6速
    //1速は1~30,2速は20~50,3速は40~70,4速は60~100,5速は80~130,6速は100~180
    //大体1.0fで50km
    private float[] _gearSpeeds = { 0.6f,0.0f, 1.0f, 1.4f, 2.0f, 2.6f, 3.2f };
    private readonly string[] GearNames = { "1", "N", "2", "3", "4", "5", "6" };
    private float _zero = 0.0f;
    [SerializeField] private Text _initGearText = default;

    [SerializeField] private Crach _crachScript;
    private float _crachValue = 0.0f;

    private int _gearIndex = 1;
    private const int MaxGearIndex = 6;
    private const int MinGearIndex = 0;

    private void FixedUpdate()
    {
        AutoMoveForward();
    }

    private void AutoMoveForward()
    {
        _crachValue = _crachScript.LeftTrigger;
            transform.Translate(_zero, _zero, _gearSpeeds[_gearIndex] * _crachValue);
        _initGearText.text = GearNames[_gearIndex];
    }

    public void UpGear()
    {
        if(_gearIndex < MaxGearIndex)
        {
            _gearIndex++;
        }
    }

    public void DownGear()
    {
        if(_gearIndex > MinGearIndex)
        {
            _gearIndex--;
        }
    }
}
