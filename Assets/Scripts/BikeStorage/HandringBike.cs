using UnityEngine;

public class HandringBike : BaseBike
{
    //インジェクション
    //デコレータで変える(interfaceみたいなもの)
    private void Awake()
    {
        _gearSpeeds = new float[] { 0.2f, 0.0f, 0.6f, 0.8f, 1.4f, 2.0f, 2.6f }; //ここも定数化する
        _gearChangeCoolTime = 0.3f;
        _handringAdditionValue = 0.8f;
        _attenuationRate = 0.8f; //ここで0.8で書き換えるのはマジックナンバー。インジェクションテーブルの別のとこから持ってきて代入する
    }
    //セットアップみたいな関数を書いてそこの引数にハンドリングできるバイクの定数とかを書いたものをつける。そこの定数の引数を使って代入
    //これをやると一つのスクリプトで全てのバイク
    private void FixedUpdate()
    {
        MoveForward();
    }
}
