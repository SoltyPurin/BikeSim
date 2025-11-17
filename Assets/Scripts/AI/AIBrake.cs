using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrake : MonoBehaviour
{
    [SerializeField, Header("ボールのコライダー")]
    private SphereCollider _sphereCollider = default;
    [SerializeField, Header("最初の摩擦に足して最大摩擦を決める値")]
    private float _brakeFrictionMaxValue = 2.0f;
    [SerializeField, Header("物理マテリアルの最初の摩擦")]
    private float _firstFriction = 0.6f;
    [SerializeField, Header("テールランプ")]
    private GameObject _tailLamp = default;
    [SerializeField,Header("トレイルレンダラー")]
    private TrailRenderer _tailRenderer = default;
    [SerializeField, Header("テールランプの残像を出す閾値")]
    private float _tailLampThreshold = 0.2f;
    private PhysicMaterial _physicsMaterial = default;

    private float _brakeMultiplier = 0;
    private float _frictionLimitValue = 0;
    private float _brakeValue = 0;
    private float _frictionMinimumValue = 0;
    private bool _isInitialize = false;

    private void Start()
    {
        _tailRenderer.emitting = false;
        _tailLamp.SetActive(false);
    }

    public void Initialize()
    {
        _physicsMaterial = new PhysicMaterial();
        _physicsMaterial.staticFriction = _firstFriction;
        _physicsMaterial.dynamicFriction = _firstFriction;
        _physicsMaterial.frictionCombine = PhysicMaterialCombine.Maximum;
        _physicsMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
        _sphereCollider.material = _physicsMaterial;
        _frictionMinimumValue = _physicsMaterial.dynamicFriction;
        _frictionLimitValue = _frictionMinimumValue + _brakeFrictionMaxValue;
        _brakeMultiplier = 1 / _frictionLimitValue;
        _isInitialize = true;
    }

    /// <summary>
    /// ブレーキを行うプロトコル。引数は0~10に正規化して渡すこと。
    /// </summary>
    /// <param name="brakeValue"></param>
    public void BrakeProtocol(float brakeValue)
    {
        if (!_isInitialize)
        {
            return; 
        }
        _brakeValue = brakeValue * _brakeMultiplier + _frictionMinimumValue;
        _brakeValue = Mathf.Clamp(_brakeValue, _frictionMinimumValue, _frictionLimitValue);
        _physicsMaterial.dynamicFriction = _brakeValue;

        if(brakeValue >= _tailLampThreshold)
        {
            _tailLamp.SetActive(true);
            _tailRenderer.emitting = true;
        }
        else
        {
            _tailLamp.SetActive(false);
            _tailRenderer.emitting = false;
        }
    }
}
