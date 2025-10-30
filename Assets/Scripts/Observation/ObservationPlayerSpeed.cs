using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationPlayerSpeed : MonoBehaviour
{
    private GameObject _player = default;
    private SpeedUI _speedUI = default;
    private BaseBike _baseBike = default;


    public void Initialize(GameObject player)
    {
        _player = player;
        _speedUI = GetComponent<SpeedUI>();
        _baseBike = _player.GetComponent<BaseBike>();
    }

    public void Run()
    {
        if(_baseBike != null)
        {
            _speedUI.UpdateSpeedText(_baseBike.CalcCurrentBikeSpeed());
        }
    }
}
