using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleGearChange : MonoBehaviour
{
    #region 変数
    private InputMap _gears = default;
    private BaseBike _baseBike = default;
    private SoundManager _sound = default;
    #endregion
    private void Start()
    {
        _sound = GetComponent<SoundManager>();
        _baseBike = GetComponent<BaseBike>();
        _gears = new InputMap();
        _gears.Enable();
    }
    private void Update()
    {
        ChangeGear();
    }

    private void ChangeGear()
    {
        if(_gears.GearChange.GearUp.triggered)
        {
            //Debug.Log("プレイヤーギア上げる");
            _sound.UpGear();
            _baseBike.UpGear();
            //ギアアップ
        }
        if(_gears.GearChange.GearDown.triggered)
        {
            //Debug.Log("プレイヤーギア下げる");
            _sound.DownGear();
            _baseBike.DownGear();
            //ギアダウン
        }
    }
}
