using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake : MonoBehaviour
{
    [SerializeField, Header("ブレーキ時に摩擦をどれくらいまであげるか")]
    private float _brakeFrictionMaxValue = 2.0f;
    [SerializeField, Header("入力値に乗算する値")]
    private float _brakeMultiplier = 2f;
    [SerializeField,Header("物理マテリアル")]
    private PhysicMaterial _physicsMaterial = default;
    private Gamepad _gamePad;

    private float _leftTriggerValue = default;
    private float _brakeValue = 0;
    private float _frictionMinimumValue = 0;

    private void Awake()
    {
        _gamePad = Gamepad.current;
        _frictionMinimumValue = _physicsMaterial.dynamicFriction;
    }

    private void FixedUpdate()
    {
        _leftTriggerValue = _gamePad.leftTrigger.ReadValue();
        _brakeValue = _leftTriggerValue * _brakeMultiplier;
        _brakeValue = Mathf.Clamp(_brakeValue,_frictionMinimumValue,_brakeFrictionMaxValue);
        _physicsMaterial.dynamicFriction = _brakeValue;
    }
}
