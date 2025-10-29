using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private Text _lapText;
    [SerializeField] private Text _timeText;
    private int _lapCount = 0;

    public void DisplayNumberOfLaps(float time)
    {
        _timeText.text =  "Time:" + time.ToString("F3");
    }

    public void PrevLapTime(float time)
    {
        Debug.Log("èëÇ´çûÇ›Ç‹ÇµÇΩ");
        _lapCount++;
        _lapText.text += "Lap" + _lapCount + ":" + time.ToString("F3") + "\n";
    }
}
