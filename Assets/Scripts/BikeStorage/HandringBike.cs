using UnityEngine;

public class HandringBike : BaseBike
{
    //インジェクション
    //デコレータで変える(interfaceみたいなもの)
    //
    [SerializeField, Header("AI制御か？")]
    private bool _isAIControll = false;

    private void Awake()
    {
        if (_isAIControll)
        {
            for (int i = 0; i < _status.AIGearSpeeds.Count; i++)
            {
                _gearSpeeds.Add(_status.AIGearSpeeds[i]);
                Debug.Log("AIの" + i + "速のスピードは" + _gearSpeeds[i]);
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
        //_gearChangeCoolTime = 0.3f;
        //_attenuationRate = 0.99f; //ここで書き換えるのはマジックナンバー。インジェクションテーブルの別のとこから持ってきて代入する
        _gearChangeCoolTime = _status.GearChangeCoolTime;
        _attenuationRate = _status.AttemiationRate;
        //ていうかもしかしてScriptableObjectってのに変えた方がいいのかも
        //とりあえずマジックナンバーはマズいのでなんとかしろ
    }
    //セットアップみたいな関数を書いてそこの引数にハンドリングできるバイクの定数とかを書いたものをつける。そこの定数の引数を使って代入
    //これをやると一つのスクリプトで全てのバイク

    private void FixedUpdate()
    {
        MoveForward();
    }

}
