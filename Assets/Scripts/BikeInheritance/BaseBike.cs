using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class BaseBike : MonoBehaviour
{
    [SerializeField, Header("ステータスを設定するScriptableObject")]
    protected BikeStatus _status;

    //デコレーター、分けた方がいい。重すぎる
    //神クラスを作ってそれの関係性を明らかにしてそれを分割
    protected List<float> _gearSpeeds = new List<float>(); //必ず速度を子クラスで設定する
    protected int _currentGearIndex = 0;
    public int CurrentGearIndex
    {
        get { return _currentGearIndex; }
    }
    private int _viewGearIndex = 0;
    protected float _gearChangeCoolTime;
    public float GearChangeCoolTime
    {
        get { return _gearChangeCoolTime; }
    }
    protected float _decelerationMultiplication = 0.98f; //_attenuationRateに乗算する値
    protected float _attenuationRate ; //惰性で動かすために速度に乗算する値、子クラスで書き換える
    protected float _clutchValue;
    protected float _axelValue;
    protected string[] _gearNames = new string[] { "N", "1", "2", "3", "4", "5", "6" };
    protected const int NEUTRALGEARINDEX = 0;
    protected bool _isFirst = true;
    protected const float ORIGINATTENUATIONVALUE = 0.6f;
    protected float _axelEngageThreshold = 0.2f; //ベタ押し検知

    protected Rigidbody _rigidBody = default;
    private BikeUIManager _uiManager = default;

    public virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _uiManager = GetComponent<BikeUIManager>();
    }

    /// <summary>
    /// ギアを上げる
    /// </summary>
    public virtual void UpGear()
    {
        if(_currentGearIndex < _gearSpeeds.Count -1)
        {
            //_currentGearIndex++;
            float gearConnectValue = _status.GearMaxSpeeds[_currentGearIndex] * _status.SuccessGearChangeRatio;
            Debug.Log("ギアチェンジに必要な速度は" + gearConnectValue);
            Debug.Log("現在の速度は" + CalcCurrentBikeSpeed());
            if (CalcCurrentBikeSpeed() >= gearConnectValue)
            {
                Debug.Log("ギアチェンジ成功！");
                _currentGearIndex++;
                _viewGearIndex = _currentGearIndex;
                _currentGearIndex = Mathf.Clamp(_currentGearIndex, 0, 6);
                _uiManager.UpdateGearText(_currentGearIndex);
            }
            else
            {
                _viewGearIndex++;
                _viewGearIndex = Mathf.Clamp(_viewGearIndex, 0, 6);
                Debug.Log("ギアチェンジ失敗");
                _uiManager.UpdateGearText(_viewGearIndex);
            }
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
            _viewGearIndex = _currentGearIndex;
            _uiManager.UpdateGearText(_viewGearIndex);
        }
    }

    /// <summary>
    /// 前進
    /// </summary>
    public virtual void MoveForward()
    {
        //Debug.Log(_rigidBody.velocity.magnitude);
        //_clutchValue = Mathf.Lerp(_clutchValue, _targetClutchValue, Time.deltaTime * _gearSpeeds[_currentGearIndex]);
        Vector3 force = transform.forward;
        switch (_currentGearIndex)
        {
            case NEUTRALGEARINDEX:
                NeutralProtocol(force);
                break;

            default:
                ElseGearProtocol(force);
                break;
        }
        
    }

    /// <summary>
    /// ニュートラル状態の時の挙動
    /// </summary>
    /// <param name="force">力を加える向き</param>
    private void NeutralProtocol(Vector3 force)
    {
        if (_isFirst)
        {
            force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
            _rigidBody.AddForce(force);
        }
        else
        {
            force = (transform.forward * _gearSpeeds[0] * _attenuationRate * _axelValue);
            _rigidBody.AddForce(force);
            _attenuationRate *= _decelerationMultiplication;
        }

    }

    /// <summary>
    /// ニュートラル以外のギアの時の挙動
    /// </summary>
    /// <param name="force">力を加える向き</param>
    private void ElseGearProtocol(Vector3 force)
    {
        if (_axelValue <= _axelEngageThreshold)
        {
            force = (transform.forward * _gearSpeeds[_currentGearIndex] * _attenuationRate);
            _rigidBody.AddForce(force);
            _attenuationRate *= _decelerationMultiplication;

        }
        else
        {
            float speed = CalcCurrentBikeSpeed();
            float maxSpeed = _status.GearMaxSpeeds[_currentGearIndex];
            float speedNormalized = Mathf.Clamp(speed/maxSpeed, 0.1f, 1f);
            //x軸が速度のy軸が速度の上がりやすさ
            float initCurve = _status.GearCurve[_currentGearIndex].Evaluate(speedNormalized);
            force = (transform.forward * _gearSpeeds[_currentGearIndex] * _axelValue);
            _rigidBody.AddForce(force);
            //Debug.Log("加える力は"+force);
            if (speed >= _status.GearMaxSpeeds[_currentGearIndex])
            {
                _rigidBody.velocity = new Vector3(
                    _rigidBody.velocity.x / (speed / _status.GearMaxSpeeds[_currentGearIndex]),
                    _rigidBody.velocity.y,
                    _rigidBody.velocity.z / (speed / _status.GearMaxSpeeds[_currentGearIndex])
                    );
            }
            _attenuationRate = ORIGINATTENUATIONVALUE;
        }
        _isFirst = false;
    }

    /// <summary>
    /// 現在のスピードを計算するメソッド
    /// </summary>
    /// <returns>現在の速度</returns>
    private float CalcCurrentBikeSpeed()
    {
        float speed = (float)Mathf.Sqrt(Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.z, 2));
        return speed;
    }

    /// <summary>
    /// エンスト、邪魔ゴミ
    /// </summary>
    public virtual void EngineStop()
    {
        //_currentGearIndex = 0;
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
    public  void UpdateAxelValue(float value)
    {
        _axelValue = value;
    }

    /// <summary>
    /// ブレーキをかけるメソッド
    /// </summary>
    public  void BrakeProtocol()
    {
        _axelValue = -1;
    }

}
