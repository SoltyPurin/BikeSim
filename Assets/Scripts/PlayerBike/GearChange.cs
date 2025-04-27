using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChange : MonoBehaviour
{
    //ギアはコントローラーの前進と後進で変更する
    //ギアの順番は1→N(初期値)→2...という感じで6が最大
    //Nは0とする
    [SerializeField] private MTBikeForward _mtBikeForward = default;
    [SerializeField] private Crach _crachScript = default;
    private float _crachValue = 0f;
    private float _upThreshold = 0.5f; //誤入力を防ぐためにしきい値を設定する
    private float _downThreshold = -0.5f;
    private const float GearChangeCoolTime = 0.5f;
    private float _initCoolTime = 0.0f;
    private bool _canChangeGear = true;

    private void FixedUpdate()
    {
        ChangeGears();
    }

    private void ChangeGears()
    {
        _crachValue = _crachScript.LeftTrigger;
        if (Input.GetAxis("Vertical") > _upThreshold && _canChangeGear && _crachValue <= 0.0f)
        {
            _mtBikeForward.UpGear();
            _canChangeGear = false;
        }
        else if (Input.GetAxis("Vertical") < _downThreshold && _canChangeGear && _crachValue <= 0.0f)
        {
            _mtBikeForward.DownGear();
            _canChangeGear = false;
        }
        bool canCoolTimeCount = _initCoolTime < GearChangeCoolTime;
        if (!_canChangeGear && canCoolTimeCount)
        {
            _initCoolTime += Time.deltaTime;
        }
        else if (!canCoolTimeCount)
        {
            _initCoolTime = 0.0f;
            _canChangeGear = true;
        }

    }
}
