using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _leftTriggerValue = default;
    private BaseBike _baseBike;

    private void Awake()
    {
        _gamePad = Gamepad.current;
        _baseBike = GetComponent<BaseBike>();
    }

    private void FixedUpdate()
    {
        _leftTriggerValue = _gamePad.leftTrigger.ReadValue();
        if( _leftTriggerValue >= 1)
        {
            _baseBike.BrakeProtocol();
        }
    }
}
