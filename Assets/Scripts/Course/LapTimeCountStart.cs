using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapTimeCountStart : MonoBehaviour
{
    private float _lapTime = 0.0f;
    private List<float> _lapTimeList = new List<float>();
    public List<float> LapTimeList
    {
        get {  return _lapTimeList; }
    }
    private int _currentLapCount = 0;
    private int _prevLapCount = 0;
    private bool _canAddTimeToList = true;
    [SerializeField] private UITimer _timer =default;
    [SerializeField, Header("プレイヤーがラップのラインに触れた回数")]
    private int _playerTouchLineCount = 0;
    [SerializeField, Header("何回触れたらゴール")]
    private int _endTouchValue = 3;

    [SerializeField, Header("お疲れ様でしたのテキスト")]
    private GameObject _endText = default;

    private bool _isMoveingResult = false;

    private void Start()
    {

        _endText.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _currentLapCount++;
            _playerTouchLineCount++;
        }
        if (_playerTouchLineCount >= _endTouchValue && !_isMoveingResult)
        {
            StartCoroutine(MoveToResult());
            _endText.SetActive(true);
            _isMoveingResult = true;
        }

    }

    private void FixedUpdate()
    {
        if ( _isMoveingResult)
        {
            return; //リザルト移行中であればカウントはしない
        }
            TimerCountUp();
    }

    private IEnumerator MoveToResult()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Result",LoadSceneMode.Additive);
    }

    private void TimerCountUp()
    {
        if (_currentLapCount != _prevLapCount)
        {
            _lapTime += Time.deltaTime;
            _canAddTimeToList = true;
            _timer.DisplayNumberOfLaps(_lapTime);
        }
        else
        {
            if (_canAddTimeToList)
            {
                AddTimeToList(_lapTime);
                PrevLapCountPlus(); 
            }
        }

    }

    public void PrevLapCountPlus()
    {
        _prevLapCount = _currentLapCount;
    }

    private void AddTimeToList(float time)
    {
        if (_canAddTimeToList)
        {
            _lapTimeList.Add(time);
            _canAddTimeToList=false;
            if(_prevLapCount != 0 && _prevLapCount < _lapTimeList.Count)
            {
                _timer.PrevLapTime(_lapTimeList[_prevLapCount]);
            }
        }
    }
}
