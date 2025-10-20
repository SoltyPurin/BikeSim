using UnityEngine;
public class SportsBike : BaseBike
{
    [SerializeField,Header("AI制御か？")]
    private bool _isAIControll = false;

    private void Awake()
    {
        if (_status != null)
        {
            Debug.Log("ステータス読み込み可能");
        }
        else
        {
            Debug.Log("(ステータスが)ないです");
        }
        //コンストラクタインジェクション
        //Dependency Injection←基本的な考えはこれ
        if (_isAIControll)
        {
            for (int i = 0; i < _status.AIGearSpeeds.Count; i++)
            {
                _gearSpeeds.Add(_status.AIGearSpeeds[i]);
                Debug.Log("AIの"+i + "速のスピードは" + _gearSpeeds[i]);
            }

        }
        else
        {
            for (int i = 0; i < _status.GearSpeeds.Count; i++)
            {
                    _gearSpeeds.Add(_status.GearSpeeds[i]);
                    Debug.Log(i + "速のスピードは" + _gearSpeeds[i]);
            }
        }
        //_gearChangeCoolTime = 0.5f;
        //_attenuationRate = 0.6f;
        _gearChangeCoolTime = _status.GearChangeCoolTime;
        _attenuationRate = _status.AttemiationRate;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

}
