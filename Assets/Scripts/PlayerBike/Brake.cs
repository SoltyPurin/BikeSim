using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brake : MonoBehaviour
{
    [SerializeField, Header("最初の摩擦に足して最大摩擦を決める値")]
    private float _brakeFrictionMaxValue = 2.0f;
    [SerializeField, Header("タイヤ痕のトレイルレンダラー")]
    private TrailRenderer _tireRenderer = default;
    [SerializeField,Header("物理マテリアル")]
    private PhysicMaterial _physicsMaterial = default;
    [SerializeField, Header("ハンドリング時に加算する値")]
    private float _handringAddValue = 10;
    [SerializeField, Header("バイクのステータス")]
    private BikeStatus _status = default;
    [SerializeField,Header("テールランプ")]
    private GameObject _tailLamp = default;
    private InputMap _inputMap = default;

    private float _originHandringValue = 0;
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
        _originHandringValue = _status.CurveAddValue;
        _inputMap = new InputMap();
        _inputMap.Enable();
    }

    private void Update()
    {
        _leftTriggerValue = _inputMap.Engine.Brake.ReadValue<float>();
        _leftTriggerValue *= 10;
        _leftTriggerValue = Mathf.Floor( _leftTriggerValue );
        _leftTriggerValue /= 10;

        float handringValue = _leftTriggerValue * _handringAddValue + _originHandringValue;
        _status.CurveAddValue = handringValue;
        //Debug.Log("現在の曲がりやすさは" + _status.CurveAddValue);
        _brakeValue = _leftTriggerValue * 10 * _brakeMultiplier + _frictionMinimumValue;
        _brakeValue = Mathf.Clamp(_brakeValue,_frictionMinimumValue,_frictionLimitValue);
        _physicsMaterial.dynamicFriction = _brakeValue;
        if(_leftTriggerValue >= 1)
        {
            _tireRenderer.emitting = true;
            _tailLamp.SetActive(true);
        }
        else
        {
            _tireRenderer.emitting = false;
            _tailLamp.SetActive(false);
        }
    }
}
