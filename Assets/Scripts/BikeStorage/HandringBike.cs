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
            _gearSpeeds = new float[] { 0.0f,3.2f,3.5f, 3.7f, 5.3f, 5.9f,6.6f }; //ここも定数化する
        }
        else
        {
            _gearSpeeds = new float[] { 0.0f, 0.2f, 0.6f, 0.8f, 1.4f, 2.0f, 2.6f }; //ここも定数化する
        }
        _gearChangeCoolTime = 0.3f;
        _attenuationRate = 0.99f; //ここで書き換えるのはマジックナンバー。インジェクションテーブルの別のとこから持ってきて代入する
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
