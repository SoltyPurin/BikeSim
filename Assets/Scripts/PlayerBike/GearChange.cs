using UnityEngine;

public class GearChange : MonoBehaviour
{
    //ギアはコントローラーの前進と後進で変更する
    //ギアの順番は1→N(初期値)→2...という感じで6が最大
    //Nは0とする
    [SerializeField] private MTBikeForward _mtBikeForward = default;
    [SerializeField] private Clutch _clutchScript = default;
    private float _clutchValue = 0f;
    [SerializeField]private float _upThreshold = 0.5f; //誤入力を防ぐためにしきい値を設定する
    [SerializeField]private float _downThreshold = -0.5f;
    private const float GearChangeCoolTime = 0.5f;
    private float _initCoolTime = 0.0f;
    private bool _canChangeGear = true;

    private void FixedUpdate()
    {
        ChangeGears();
    }

    private void ChangeGears()
    {
        _clutchValue = _clutchScript.LeftTrigger;
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > _upThreshold && _canChangeGear && _clutchValue <= 0.0f)
        {
            _mtBikeForward.UpGear();
            _canChangeGear = false;
        }
        else if (verticalInput < _downThreshold && _canChangeGear && _clutchValue <= 0.0f)
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
