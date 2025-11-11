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
    [SerializeField, Header("敵の最大数")]
    private int _enemyMaxCount = 10;
    [SerializeField, Header("敵の数のテキスト")]
    private Text _enemyCountText = default;

    private int _enemyCount = 1;

    private void Start()
    {
        PlayerPrefs.SetInt("EnemyCount", _enemyCount);
        Debug.Log("最初の敵の数は" + PlayerPrefs.GetInt("EnemyCount"));
    }
    public void CountPlus()
    {
        _enemyCount++;
        _enemyCount = Mathf.Clamp(_enemyCount, 1, _enemyMaxCount);
        _enemyCountText.text = _enemyCount.ToString();
    }

    public void CountMinus()
    {
        _enemyCount--;
        _enemyCount = Mathf.Clamp(_enemyCount, 1, _enemyMaxCount);
        _enemyCountText.text = _enemyCount.ToString();
    }

    public void PressReturnButton()
    {
        PlayerPrefs.SetInt("EnemyCount",_enemyCount);
        _optionParentObj.SetActive(false);
        _bikeSelectParentObj.SetActive(false);
        _firstButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectButton);
    }
}
