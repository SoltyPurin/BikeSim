using System.Collections.Generic;
using UnityEngine;

public class LapTimeCountStart : MonoBehaviour
{
    private float _lapTime = 0.0f;
    private List<float> _lapTimeList = new List<float>();
    private int _currentLapCount = 0;
    private int _prevLapCount = 0;
    private bool _canAddTimeToList = true;
    [SerializeField] private UITimer _timer =default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentLapCount++;
        }
    }

    private void FixedUpdate()
    {
        TimerCountUp();
    }

    private void TimerCountUp()
    {
        Debug.Log("currentÇÕ" + _currentLapCount + "prevÇÕ" + _prevLapCount);
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
                PrevLapCountPlus(); // Å© Ç±ÇÍÇÇ±Ç±Ç…à⁄ìÆÅI
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
