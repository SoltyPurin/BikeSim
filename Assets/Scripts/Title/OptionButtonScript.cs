using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionButtonScript : MonoBehaviour
{
    [SerializeField, Header("最初の画面で表示されるボタンの親")]
    private GameObject _firstButtons = default;
    [SerializeField, Header("オプション画面の親オブジェ")]
    private GameObject _optionParentObj = default;
    [SerializeField, Header("バイク選択画面の親オブジェクト")]
    private GameObject _bikeSelectParentObj = default;
    [SerializeField,Header("最初の画面に戻った時に最初に選択するボタン")]
    private GameObject _firstSelectButton = default;
    [SerializeField, Header("時間帯を表示するテキスト")]
    private Text _enemyCountText = default;
    [SerializeField,Header("時間帯")]
    private List<string>_timeZones = new List<string>();

    private int _currentTimeZoneIndex = 0;
    private void Start()
    {
        _enemyCountText.text = _timeZones[_currentTimeZoneIndex];
        PlayerPrefs.SetInt("TimeZone", _currentTimeZoneIndex);
    }
    public void CountPlus()
    {
        if(_currentTimeZoneIndex >= _timeZones.Count - 1)
        {
            _currentTimeZoneIndex = 0;
        }
        else
        {
            _currentTimeZoneIndex++;
        }
        _enemyCountText.text = _timeZones[_currentTimeZoneIndex];
        PlayerPrefs.SetInt("TimeZone",_currentTimeZoneIndex);
    }

    public void CountMinus()
    {
        if(_currentTimeZoneIndex <= 0)
        {
            _currentTimeZoneIndex = _timeZones.Count - 1;
        }
        else
        {
            _currentTimeZoneIndex--;
        }
        _enemyCountText.text = _timeZones[_currentTimeZoneIndex];
        PlayerPrefs.SetInt("TimeZone", _currentTimeZoneIndex);
    }

    public void PressReturnButton()
    {
        _optionParentObj.SetActive(false);
        _bikeSelectParentObj.SetActive(false);
        _firstButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectButton);
    }
}
