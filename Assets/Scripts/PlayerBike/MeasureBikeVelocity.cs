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
        // deltaTimeが0の場合は何もしない
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        Vector3 delta = transform.position - _prevPos;
        _velocity = delta.magnitude / Time.deltaTime;
        _prevPos = transform.position;

        Debug.Log(_velocity);

        //ギアの変更を検知。その後ギア変更前の速度とギア変更後の初速を比較してしきい値を超えてたらエンスト

    }
}
