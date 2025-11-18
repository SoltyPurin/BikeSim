using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面")]
    private GameObject _pauseMenu = default;
    [SerializeField, Header("最初に選択されるボタン")]
    private GameObject _firstSelectedButton = default;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TimeStopProtocol();
        }
    }

    private void TimeStopProtocol()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton);

    }

    public void TimeStart()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

}
