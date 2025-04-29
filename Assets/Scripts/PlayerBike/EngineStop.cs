using System.Collections;
using UnityEngine;

public class EngineStop : MonoBehaviour
{
    [SerializeField] private Clutch _clutch = default;
    //[SerializeField] private MTBikeForward _bikeForward = default;
    [SerializeField] private BaseBike _baseBike = default;
    [SerializeField] private MeasureBikeVelocity _bikeVelocity = default;

    //クラッチ入れる前のギアを保存、クラッチの値が一定以上になったときに前のギアと比較、ギア差が

    private float _prevClutchValue = 0.0f;
    private float _initClutchValue = 0.0f;
    private float _tolerance = 0.7f;
    private const float GETVALUECOOLTIME = 0.2f;
    private float _initCoolTime = 0.0f;
    private float _prevVelocity = 0.0f;
    private float _afterGearChangeVelocity = 0.0f;
    [SerializeField]private float _permissionVelocity = 24.0f;
    private bool _canChangeVelocity = true;
    [SerializeField]private  float _canChangeGearTorelance = 2;
    [SerializeField] private float _callCheckVelocityAfterGearChange = 0.6f;
    private void FixedUpdate()
    {
        float nowClutchValue = _clutch.LeftTrigger;
        _initCoolTime += Time.deltaTime;
        if (_initCoolTime >= GETVALUECOOLTIME)
        {
            CrachOver(nowClutchValue);
            _initCoolTime = 0;
        }
        if(nowClutchValue <= 0 && _canChangeVelocity)
        {
            _canChangeVelocity = false;
            StartCoroutine(CheckVelocityAfterGearChange());
        }
    }

    private IEnumerator CheckVelocityAfterGearChange()
    {
        _prevVelocity = _bikeVelocity.Velocity;

        yield return new WaitForSeconds(_callCheckVelocityAfterGearChange);

        _afterGearChangeVelocity = _bikeVelocity.Velocity;
        if (Mathf.Abs(_afterGearChangeVelocity - _prevVelocity) > _permissionVelocity)
        {
            _baseBike.EngineStop();
        }
        _canChangeVelocity = true;

    }

    private void CrachOver(float clutch)
    {
        _initClutchValue = clutch;
        if(_initClutchValue-_prevClutchValue > _tolerance)
        {
            _baseBike.EngineStop();
        }
        _prevClutchValue = _clutch.LeftTrigger;
    }

}
