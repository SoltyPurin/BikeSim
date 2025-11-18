using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonProtocols : MonoBehaviour
{
    private PauseButton _pauseButton = default;
    [SerializeField, Header("メニュー画面のボタン")]
    private GameObject _pauseMenu = default;
    [SerializeField, Header("オプション画面の親オブジェクト")]
    private GameObject _optionMenu = default;
    [SerializeField, Header("オプション画面で最初に選択されるボタン")]
    private GameObject _optionFirstSelectButton = default;
    private void Start()
    {
        _pauseButton = GetComponent<PauseButton>();
    }
    public void TapResume()
    {
        _pauseButton.TimeStart();
    }
    public void TapOptionMenu()
    {
        _pauseMenu.SetActive(false);
        _optionMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_optionFirstSelectButton);
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
