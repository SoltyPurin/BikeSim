using UnityEngine;
public class SportsBIke : BaseBike
{
    [SerializeField,Header("AI制御か？")]
    private bool _isAIControll = false;
    private void Awake()
    {
        //インジェクションテーブルを使うとここのboolもいらなくなる
        //コンストラクタインジェクション
        //Dependency Injection←基本的な考えはこれ
        if (_isAIControll)
        {
            _gearSpeeds = new float[] { 0.4f, 0.0f, 0.8f, 1.4f, 2.0f, 2.6f, 3.2f };
        }
        else
        {
            _gearSpeeds = new float[] { 0.6f, 0.0f, 1.2f, 1.8f, 2.4f, 3.0f, 3.6f };
        }
        _gearChangeCoolTime = 0.5f;
        _handringAdditionValue = 0.3f;
        _attenuationRate = 0.6f;
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

}
