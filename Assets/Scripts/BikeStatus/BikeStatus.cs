using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bike_Status")]
public class BikeStatus : ScriptableObject
{
    [SerializeField,Header("各ギアの速度")]
    private  List<float> _gearSpeeds = new List<float>();
    [SerializeField, Header("曲がりやすさ")]
    private float _curveAddValue = default;
    [SerializeField,Header("ギアチェンジのクールタイム")]
    private float _gearChangeCoolTime = default;
    [SerializeField,Header("どれくらい惰性で動かすかの値")]
    private float _attenuationRate = default;

}
