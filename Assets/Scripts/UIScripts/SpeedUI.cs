using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    [SerializeField, Header("スピードメーターのテキスト")]
    private Text _speedText = default;

    public void UpdateSpeedText(float speed)
    {
        Debug.Log(speed);
        _speedText.text = speed.ToString();
    }
}
