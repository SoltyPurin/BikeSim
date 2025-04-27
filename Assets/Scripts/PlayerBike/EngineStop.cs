using UnityEngine;

public class EngineStop : MonoBehaviour
{
    [SerializeField] private Crach _crach = default;
    [SerializeField] private MTBikeForward _bikeForward = default;
    [SerializeField] private MeasureBikeVelocity _bikeVelocity = default;

    //クラッチ入れる前のギアを保存、クラッチの値が一定以上になったときに前のギアと比較、ギア差が

    private float _prevCrachValue = 0.0f;
    private float _initCrachValue = 0.0f;
    private float _tolerance = 0.7f;
    private const float GETVALUECOOLTIME = 0.2f;
    private float _initCoolTime = 0.0f;
    private int _initGear = 1;
    private int _prevGear = 1;
    private const float NUMBEROFGEARSYOUCANCHANGEATONCE = 2;
    private float _upperGearDetection = 0.1f;
    private bool _canChangeGear = true;
    private void FixedUpdate()
    {
        _initCoolTime += Time.deltaTime;
        if (_initCoolTime >= GETVALUECOOLTIME)
        {
            Debug.Log("エンスト判定");
            CrachOver();
            _initCoolTime = 0;
        }
        if(_crach.LeftTrigger >= _upperGearDetection )
        {
            SuddenStart();
            _canChangeGear = false;
        }
    }

    private void CrachOver()
    {
        _initCrachValue = _crach.LeftTrigger;
        if(_initCrachValue-_prevCrachValue > _tolerance)
        {
            _bikeForward.EngineStop();
        }
        _prevCrachValue = _crach.LeftTrigger;
    }

    public void GearChange(int gear)
    {
        _initGear = gear;
    }
    private void SuddenStart()
    {
        if(_initGear-_prevGear >= NUMBEROFGEARSYOUCANCHANGEATONCE)
        {
            _bikeForward.EngineStop();
            _initGear = 1;
        }
        else
        {
            _prevGear = _initGear;
        }
    }
}
