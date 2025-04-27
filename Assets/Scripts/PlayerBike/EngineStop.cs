using UnityEngine;

public class EngineStop : MonoBehaviour
{
    [SerializeField] private Crach _crach = default;
    [SerializeField] private MTBikeForward _bikeForward = default;

    private float _prevCrachValue = 0.0f;
    private float _initCrachValue = 0.0f;
    private float _tolerance = 0.7f;
    private const float GETVALUECOOLTIME = 0.2f;
    private float _initCoolTime = 0.0f;

    private void FixedUpdate()
    {
        _initCoolTime += Time.deltaTime;
        if (_initCoolTime >= GETVALUECOOLTIME)
        {
            Debug.Log("ƒGƒ“ƒXƒg”»’è");
            CrachOver();
            _initCoolTime = 0;
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
        Debug.Log(_prevCrachValue - _initCrachValue);
    }
}
