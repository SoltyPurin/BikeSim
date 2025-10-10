using UnityEngine;

public class BaseBike : MonoBehaviour
{
    //デコレーター、分けた方がいい。重すぎる
    //神クラスを作ってそれの関係性を明らかにしてそれを分割
    protected float[] _gearSpeeds; //必ず速度を子クラスで設定する
    protected int _currentGearIndex = 0;
    public int CurrentGearIndex
    {
        get { return _currentGearIndex; }
    }
    protected float _gearChangeCoolTime;
    public float GearChangeCoolTime
    {
        get { return _gearChangeCoolTime; }
    }
    protected float _decelerationMultiplication = 0.98f; //_attenuationRateに乗算する値
    protected float _attenuationRate ; //惰性で動かすために速度に乗算する値、子クラスで書き換える
    protected float _clutchValue;
    protected float _axelValue;
    protected float _maxSpeed;
    protected string[] _gearNames = new string[] { "N", "1", "2", "3", "4", "5", "6" };
    protected const int NEUTRALGEARINDEX = 0;
    protected bool _isFirst = true;
    protected const float ORIGINATTENUATIONVALUE = 0.6f;
    protected float _axelEngageThreshold = 0.2f; //ベタ押し検知

    protected Rigidbody _rigidBody = default;

    public virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

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
        Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
        //_clutchValue = Mathf.Lerp(_clutchValue, _targetClutchValue, Time.deltaTime * _gearSpeeds[_currentGearIndex]);
        Vector3 force = transform.forward;
        Debug.Log("現在のギアは" + _currentGearIndex);
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    Debug.Log("初回N");
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
                    _rigidBody.AddForce(force);
                }
                else
                {
                    Debug.Log("次回N");
                    force = (transform.forward * _gearSpeeds[0] * _attenuationRate * _axelValue);
                    _rigidBody.AddForce(force);
                    _attenuationRate *= _decelerationMultiplication;
                }
                break;

            default:
                if (_axelValue <= _axelEngageThreshold)
                {
                    Debug.Log("アクセル離し");
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _attenuationRate);
                    _rigidBody.AddForce(force);
                    _attenuationRate *= _decelerationMultiplication;

                }
                else
                {
                    Debug.Log(_gearSpeeds[_currentGearIndex]);
                    force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
                    _rigidBody.AddForce(force);
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
        _currentGearIndex = 0;
    }


    /// <summary>
    /// 本作はクラッチを使わないバイクゲームになったのでこのメソッドは荼毘に付したよ
    /// </summary>
    /// <param name="value">左トリガーの値</param>
    public virtual void UpdateClutchValue(float value)
    {
        //_targetClutchValue = value;
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
