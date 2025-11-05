using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectGameStartButton : MonoBehaviour
{
    [SerializeField, Header("最初に表示されてるボタン達の親")]
    private GameObject _firstButtons = default;
    [SerializeField,Header("バイクを選択するボタン達の親")]
    private GameObject _bikeSelectButton = default;
    [SerializeField,Header("最初に選択されるボタン")]
    private GameObject _firstSelectButton = default;

    public void PressTheButton()
    {
        _firstButtons.SetActive(false);
        _bikeSelectButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectButton);
    }
}
