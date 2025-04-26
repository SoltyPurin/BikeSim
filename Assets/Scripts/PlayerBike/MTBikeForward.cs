using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MTBikeForward : MonoBehaviour
{
    //MTのバイクはクラッチを握らないと勝手に前に進む
    //ギアごとに最高速度が決まってる
    //左から順に1,N,2,3,4,5,6速
    private float[] _gearSpeeds = { 0.1f,0.0f, 0.3f, 0.5f, 0.6f, 0.8f, 1.0f };
    private readonly string[] GearNames = { "1", "N", "2", "3", "4", "5", "6" };
    private float _zero = 0.0f;
    [SerializeField] private Text _initGearText = default;

    private int _gearIndex = 1;
    private const int MaxGearIndex = 6;
    private const int MinGearIndex = 0;

    private void FixedUpdate()
    {
        AutoMoveForward();
    }

    private void AutoMoveForward()
    {
        transform.Translate(_zero, _zero, _gearSpeeds[_gearIndex]);
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
