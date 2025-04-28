using UnityEngine;
using UnityEngine.UI;

public class MTBikeForward : MonoBehaviour
{
    //MTのバイクはクラッチを握らないと勝手に前に進む
    //ギアごとに最高速度が決まってる
    //左から順に1,N,2,3,4,5,6速
    //1速は1~30,2速は20~50,3速は40~70,4速は60~100,5速は80~130,6速は100~180
    //大体1.0fで50km
    private float[] _gearSpeeds = { 0.4f,0.0f, 0.8f, 1.2f, 1.8f, 2.4f, 3.0f };
    private readonly string[] GearNames = { "1", "N", "2", "3", "4", "5", "6" };
    private float _prevSpeedValue = 0.0f;
    [SerializeField] private Text _initGearText = default;

    //無駄な処理を走らせないためにこっちからギアチェンをしたときだけ情報を送る。ただしやりとりはここだけ
    //設計の面から見るとかなりグレーだが無駄な処理を走らせないためには仕方ない
    [SerializeField] private EngineStop _engineStop = default;

    [SerializeField]private float _attenuationRate = 0.6f;
    private const float ORIGINATTENUATIONVALUE = 0.6f;

    [SerializeField] private Clutch _clutchScript;
    [SerializeField] private MeasureBikeVelocity _bikeVelocity;
    private float _clutchValue = 0.0f;

    private bool _isFirst = true;

    private int _gearIndex = 1;

    public int GearIndex
    {
        get { return _gearIndex; }
    }
    private const int MAXGEARINDEX = 6;
    private const int MINGEARINDEX = 0;
    private const int NEUTRALGEARINDEX = 1;

    private void FixedUpdate()
    {
        AutoMoveForward();
    }

    private void AutoMoveForward()
    {
        _clutchValue = _clutchScript.LeftTrigger;
        switch (_gearIndex)
        {
            case NEUTRALGEARINDEX:
                if (_isFirst)
                {
                    transform.Translate(0, 0, _gearSpeeds[_gearIndex] * _clutchValue);
                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[0] * _attenuationRate);
                    _attenuationRate *= 0.98f;
                }
                break;

            default:
                if (_clutchValue <= 0.2f)
                {
                    transform.Translate(0, 0, _gearSpeeds[_gearIndex] * _attenuationRate);
                    _attenuationRate *= 0.98f;

                }
                else
                {
                    transform.Translate(0, 0, _gearSpeeds[_gearIndex] * _clutchValue);
                    _attenuationRate = ORIGINATTENUATIONVALUE;
                }
                _isFirst = false;

                break;
        }
        _initGearText.text = GearNames[_gearIndex];
    }

    public void UpGear()
    {
        if(_gearIndex < MAXGEARINDEX)
        {
            _gearIndex++;
        }
    }

    public void DownGear()
    {
        if(_gearIndex > MINGEARINDEX)
        {
            _prevSpeedValue = _gearSpeeds[_gearIndex];
            _gearIndex--;
        }
    }

    public void EngineStop()
    {
        _gearIndex = 1;
        //transform.Translate(0, 0, _gearSpeeds[_gearIndex] * 0);
    }
}
