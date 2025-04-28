using UnityEngine;

public class MeasureBikeVelocity : MonoBehaviour
{
    private Vector3 _prevPos = default;

    private float _velocity = 0;

    public float Velocity
    {
        get {  return _velocity; }
    }

    private void Awake()
    {
        _prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        MeasureVelocity();
    }

    private void MeasureVelocity()
    {
        // deltaTimeÇ™0ÇÃèÍçáÇÕâΩÇ‡ÇµÇ»Ç¢
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        Vector3 delta = transform.position - _prevPos;
        _velocity = delta.magnitude / Time.deltaTime;
        _prevPos = transform.position;

    }
}
