using UnityEngine;
using UnityEngine.UI;

public class BikeUIManager : MonoBehaviour
{
    [SerializeField] private Text _clutchValueText = default;
    [SerializeField] private Text _gearNumberText = default;

    [SerializeField] private Clutch _clutch = default;
    [SerializeField] private BaseBike _baseBike = default;

    private string[] _gearNames = { "1", "N", "2", "3", "4", "5", "6" };
    private void FixedUpdate()
    {
        _clutchValueText.text = _clutch.LeftTrigger.ToString("F1");
        _gearNumberText.text = _gearNames[_baseBike.CurrentGearIndex];
    }
}
