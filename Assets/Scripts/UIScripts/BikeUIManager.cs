using UnityEngine;
using UnityEngine.UI;

public class BikeUIManager : MonoBehaviour
{
    //[SerializeField] private Text _clutchValueText = default;
    protected Text _gearNumberText = default;

    [SerializeField] private BaseBike _baseBike = default;

    private string[] _gearNames = { "N", "1", "2", "3", "4", "5", "6" };

    private readonly string GEARNUMBERTEXT = "GearNumberText";

    private void Awake()
    {
        //_clutchValueText = GameObject.FindWithTag(CLUTHTEXTNAME).GetComponent<Text>();
        _gearNumberText = GameObject.FindWithTag(GEARNUMBERTEXT).GetComponent <Text>();
    }
    public virtual void UpdateGearText(int gearNumber)
    {
        _gearNumberText.text = _gearNames[gearNumber];
    }
}
