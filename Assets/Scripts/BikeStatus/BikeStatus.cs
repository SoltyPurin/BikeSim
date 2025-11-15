using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bike_Status")]
public class BikeStatus : ScriptableObject
{
    [SerializeField,Header("各ギアで与える倍率")]
    private  List<float> _gearSpeeds = new List<float>();
    public List<float> GearSpeeds
    {
        get { return _gearSpeeds; }
    }
    [SerializeField,Header("各ギアの最高速度")]
    private List<float> _gearMaxSpeeds = new List<float>();
    public List<float> GearMaxSpeeds
    {
        get { return _gearMaxSpeeds; }
    }
    [SerializeField, Header("そのギアの最大速度の何%が出ていればギアチェンジ成立するか")]
    private float _successGearChangeRatio = 0.7f;
    public float SuccessGearChangeRatio
    {
        get{ return _successGearChangeRatio; }
    }
    [SerializeField, Header("AIはそのギアの何%出てたらギアチェンさせるか")]
    private float _aiSuccessGearChangeRatio = 0.5f;

    public float AISuccessGearChangeRatio
    {
        get { return _aiSuccessGearChangeRatio; }
    }
    [SerializeField,Header("各ギアごとの速度の上昇値を決めるカーブ")]
    private List<AnimationCurve> _gearCurve = new List<AnimationCurve>();
    public List<AnimationCurve> GearCurve
    {
        get { return _gearCurve; }
    }
    [SerializeField,Header("AIのギア速度")]
    private List<float> _aiGearSpeeds = new List<float>();
    public List<float> AIGearSpeeds
    {
        get { return _aiGearSpeeds; }
    }
    [SerializeField, Header("曲がりやすさ")]
    private float _curveAddValue = default;
    public float CurveAddValue
    {
        get { return _curveAddValue; }
        set { _curveAddValue = value; }
    }
    [SerializeField,Header("ギアチェンジのクールタイム")]
    private float _gearChangeCoolTime = default;
    public float GearChangeCoolTime
    {
        get { return _gearChangeCoolTime; }
    }
    [SerializeField,Header("どれくらい惰性で動かすかの値")]
    private float _attenuationRate = default;
    public float AttemiationRate
    {
        get { return _attenuationRate; }
    }
}
