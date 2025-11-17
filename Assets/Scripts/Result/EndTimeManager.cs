using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

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
        //Debug.Log("実行してるオブジェクトは" + this.gameObject.name);
        _lapCountObject = GameObject.FindWithTag(LAPOBJECTTAGNAME);
        _lapScript = _lapCountObject.GetComponent<LapTimeCountStart>();
        _lapTime = new List<float>(_lapScript.LapTimeList);

        //0番目にはなにも入ってないので1から書く
        int firstMinuts = ReturnMinuts(_lapTime[1]);
        float firstSeconds = ReturnSeconds(_lapTime[1]);
        _firstLapText.text = "Lap1 Time :  " + firstMinuts+ ":" + firstSeconds.ToString("F1");
        int secondMinuts = ReturnMinuts(_lapTime[2]);
        float secondSeconds = ReturnSeconds(_lapTime[2]);
        _secondLapText.text = "Lap2 Time :  "+secondMinuts + ":" + secondSeconds.ToString("F1");

        float totalTime = _lapTime[1] + _lapTime[2];

        int totalMinuts = ReturnMinuts(totalTime);
        float totalSeconds = ReturnSeconds(totalTime);

        _totalTimeText.text = "Total Time :  " + totalMinuts+ ":" + totalSeconds.ToString("F1");
        
        StartCoroutine(HonpenUnload());
    }

    private int ReturnMinuts(float time)
    {
        int intergerTime = Mathf.FloorToInt(time);
        int minuts = intergerTime / 60;
        return minuts;
    }

    private float ReturnSeconds(float time)
    {
        float seconds = time % 60;
        return seconds;
    }
    private IEnumerator HonpenUnload()
    {
        AsyncOperation op= SceneManager.UnloadSceneAsync("Honpen");
        yield return op;
        Debug.Log("本編削除");
        yield return Resources.UnloadUnusedAssets();
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
