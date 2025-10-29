using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    [SerializeField, Header("スピードメーターのテキスト")]
    private Text _speedText = default;

    public void UpdateSpeedText(float speed)
    {
        speed *= 2;
        _speedText.text = speed.ToString("F0");
    }
}
