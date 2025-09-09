using UnityEngine;

public class BaseBike : MonoBehaviour
{
    //デコレーター、分けた方がいい。重すぎる
    //神クラスを作ってそれの関係性を明らかにしてそれを分割
    protected float[] _gearSpeeds; //必ず速度を子クラスで設定する
    protected int _currentGearIndex = 1;
    public int CurrentGearIndex
    {
        get { return _currentGearIndex; }
    }
    protected float _gearChangeCoolTime;
    public float GearChangeCoolTime
    {
        get { return _gearChangeCoolTime; }
    }
    protected float _handringAdditionValue;
    public float HandringAdditionValue
    {
        get { return _handringAdditionValue; }
    }
    protected float _decelerationMultiplication = 0.98f; //_attenuationRateに乗算する値
    protected float _attenuationRate ; //惰性で動かすために速度に乗算する値、子クラスで書き換える
    protected float _clutchValue;
    protected float _axelValue;
    protected float _maxSpeed;
    protected string[] _gearNames = new string[] { "1", "N", "2", "3", "4", "5", "6" };
    protected const int NEUTRALGEARINDEX = 1;
    protected bool _isFirst = true;
    protected const float ORIGINATTENUATIONVALUE = 0.6f;
    protected float _clutchEngageThreshold = 0.2f; //クラッチベタ押し検知
    protected float _gearChangeTorelance = 0.7f; //しっかり半クラにしないとエンストするための変数
    private float _targetClutchValue = 0;

    /// <summary>
    /// ギアを上げる
    /// </summary>
    public virtual void UpGear()
    {
        if(_currentGearIndex < _gearSpeeds.Length -1)
        {
            _currentGearIndex++;
        }
    }

    /// <summary>
    /// ギアを下げる
    /// </summary>
    public virtual void DownGear()
    {
        if(_currentGearIndex > 0)
        {
            _currentGearIndex--;
        }
    }

    /// <summary>
    /// 前進
    /// </summary>
    public virtual void MoveForward()
    {
        _clutchValue = Mathf.Lerp(_clutchValue,_targetClutchValue,Time.deltaTime * _gearSpeeds[_currentGearIndex]);
        Debug.Log(_clutchValue);
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue);
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;
                }
                break;

            default:
                if (_clutchValue <= _clutchEngageThreshold)
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _attenuationRate *= _decelerationMultiplication;

                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[_currentGearIndex] * _clutchValue);
                    _attenuationRate = ORIGINATTENUATIONVALUE;
                }
                _isFirst = false;
                break;
        }

    }

    /// <summary>
    /// エンスト
    /// </summary>
    public virtual void EngineStop()
    {
        _currentGearIndex = 1;
    }


    /// <summary>
    /// クラッチの値を変更
    /// </summary>
    /// <param name="value">左トリガーの値</param>
    public virtual void UpdateClutchValue(float value)
    {
        _targetClutchValue = value;
    }
    /// <summary>
    /// アクセルを吹かす
    /// </summary>
    /// <param name="value">右トリガーの値</param>
    public virtual void UpdateAxelValue(float value)
    {
        _axelValue = value;
    }

}
