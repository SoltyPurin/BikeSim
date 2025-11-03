using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOptionButton : MonoBehaviour
{
    [SerializeField, Header("最初に表示されてるボタン達の親")]
    private GameObject _firstButtons = default;
    [SerializeField, Header("オプション画面に表示するオブジェクト達の親")]
    private GameObject _optionObjParent = default;
    [SerializeField,Header("オプション画面で最初に選択するボタン")]
    private GameObject _optionFirstSelectButton = default;

    public void PressTheButton()
    {
        _firstButtons.SetActive(false);
        _optionObjParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_optionFirstSelectButton);
    }
}
