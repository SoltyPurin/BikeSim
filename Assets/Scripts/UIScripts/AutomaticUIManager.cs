using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticUIManager : BikeUIManager
{
    [SerializeField, Header("ギアナンバーの代わりに表示する文字")]
    private string _text = "Automatic";
    private void Start()
    {
        _gearNumberText.text = _text;

    }
    public override void UpdateGearText(int gearNumber)
    {
        _gearNumberText.text = _text;
    }
}
