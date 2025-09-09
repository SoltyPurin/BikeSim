using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndTimeManager : MonoBehaviour
{
    private GameObject _lapCountObject = default;
    private LapTimeCountStart _lapScript = default;
    private readonly string LAPOBJECTTAGNAME = "LapLine";
    [SerializeField, Header("一個目のラップのテキスト")]
    private Text _firstLapText = default;
    [SerializeField, Header("二個目のラップのテキスト")]
    private Text _secondLapText = default;
    [SerializeField,Header("合計の時間")]
    private Text _totalTimeText = default;

    private List<float> _lapTime = new List<float>();
    void Start()
    {
        _lapCountObject = GameObject.FindWithTag(LAPOBJECTTAGNAME);
        _lapScript = _lapCountObject.GetComponent<LapTimeCountStart>();
        _lapTime = new List<float>(_lapScript.LapTimeList);
        //0番目にはなにも入ってないので1から書く
        _firstLapText.text = "Lap1 Time :"+_lapTime[1].ToString();
        _secondLapText.text = "Lap2 Time :"+_lapTime[2].ToString();
        float firstValue = _lapTime[1];
        float secondValue = _lapTime[2];
        float totalTime = firstValue + secondValue;
        _totalTimeText.text = "Total Time :" +totalTime.ToString();
        for (int i = 0; i < _lapScript.LapTimeList.Count; i++)
        {
            Debug.Log($"Lap {i + 1}: {_lapTime[i]}");
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Title");
    }

}
