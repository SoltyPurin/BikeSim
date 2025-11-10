using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake : MonoBehaviour
{
    [SerializeField, Header("最初の摩擦に足して最大摩擦を決める値")]
    private float _brakeFrictionMaxValue = 2.0f;
    [SerializeField, Header("トレイルレンダラー")]
    private TrailRenderer _trailRenderer = default;
    [SerializeField,Header("物理マテリアル")]
    private PhysicMaterial _physicsMaterial = default;
    private InputMap _inputMap = default;

    private float _brakeMultiplier = 0;
    private float _frictionLimitValue = 0;
    private float _leftTriggerValue = default;
    private float _brakeValue = 0;
    private float _frictionMinimumValue = 0;

    private void Awake()
    {
        _frictionMinimumValue = _physicsMaterial.dynamicFriction;
        _frictionLimitValue = _frictionMinimumValue + _brakeFrictionMaxValue;
        _brakeMultiplier = 1/ _frictionLimitValue;
        _inputMap = new InputMap();
        _inputMap.Enable();
    }

    private void Update()
    {

        _leftTriggerValue = _inputMap.Engine.Brake.ReadValue<float>();
        _brakeValue = _leftTriggerValue * 10 * _brakeMultiplier + _frictionMinimumValue;
        _brakeValue = Mathf.Clamp(_brakeValue,_frictionMinimumValue,_frictionLimitValue);
        _physicsMaterial.dynamicFriction = _brakeValue;
        if(_leftTriggerValue >= 1)
        {
            _trailRenderer.emitting = true;
        }
        else
        {
            _trailRenderer.emitting = false;
        }
    }
}
