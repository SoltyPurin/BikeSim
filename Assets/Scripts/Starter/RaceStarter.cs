using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaceStarter : MonoBehaviour
{
    [SerializeField, Header("スタート時間を表示するテキスト")]
    private Text _countDownText = default;
    [SerializeField, Header("スタートするときの文言")]
    private string _startText = "GO!";
    [SerializeField, Header("AIをスタートさせるスクリプト")]
    private AIBikeInitializer _aiInitializer = default;

    private Axel _axel = default;
    private float _countDownTime = 3;
    private float _deActiveTime = 1;
    private bool _canCountDown = true;

    private async void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        _axel = player.GetComponent<Axel>();
        _countDownText.text = _countDownTime.ToString();
        await UniTask.WaitForSeconds(_countDownTime);
        _canCountDown = false;
        RaceStart();
    }

    private void FixedUpdate()
    {
        if (!_canCountDown)
        {
            return;
        }
        _countDownTime -= Time.fixedDeltaTime;
        if(_countDownTime >= 0)
        {
            _countDownText.text = Mathf.Ceil(_countDownTime).ToString("F0");
        }
        else
        {
            _countDownText.text = _startText;
        }
    }

    private async void RaceStart()
    {
        _countDownText.text = _startText;
        _aiInitializer.Initialize();
        _axel.PermissionStart();
        await UniTask.WaitForSeconds(_deActiveTime);
        _countDownText.enabled = false;
    }
}
