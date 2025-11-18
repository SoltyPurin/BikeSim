using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField, Header("ポーズメニュー")]
    private GameObject _pauseMenu = default;
    [SerializeField, Header("オプション画面")]
    private GameObject _optionMenu = default;
    [SerializeField, Header("ポーズメニューで最初に選択されるボタン")]
    private GameObject _pauseFirstSelectButton = default;
    [SerializeField, Header("時間帯")]
    private List<string> _timeZones = new List<string>();
    [SerializeField, Header("時間帯を表示するテキスト")]
    private Text _timeZoneText = default;
    [SerializeField, Header("時間帯を設定するスクリプト")]
    private FirstTimeZoneSet _timeSet = default;

    private int _currentTimeZoneIndex = 0;
    private void Start()
    {
        _timeZoneText.text = PlayerPrefs.GetString("TimeZone", "昼");
    }
    public void CountPlus()
    {
        if (_currentTimeZoneIndex >= _timeZones.Count - 1)
        {
            _currentTimeZoneIndex = 0;
        }
        else
        {
            _currentTimeZoneIndex++;
        }
        _timeZoneText.text = _timeZones[_currentTimeZoneIndex];
        PlayerPrefs.SetInt("TimeZone", _currentTimeZoneIndex);
        _timeSet.ChangeTimeZone(_currentTimeZoneIndex);
    }

    public void CountMinus()
    {
        if (_currentTimeZoneIndex <= 0)
        {
            _currentTimeZoneIndex = _timeZones.Count - 1;
        }
        else
        {
            _currentTimeZoneIndex--;
        }
        _timeZoneText.text = _timeZones[_currentTimeZoneIndex];
        PlayerPrefs.SetInt("TimeZone", _currentTimeZoneIndex);
        _timeSet.ChangeTimeZone(_currentTimeZoneIndex);
    }

    public void Return()
    {
        _optionMenu.SetActive(false);
        _pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_pauseFirstSelectButton);
    }
}
