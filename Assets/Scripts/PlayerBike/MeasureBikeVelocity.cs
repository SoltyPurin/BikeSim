using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureBikeVelocity : MonoBehaviour
{
    private Vector3 _prevPos = default;

    private void Start()
    {
        _prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        Measure();
    }

    private void Measure()
    {
        // deltaTime‚ª0‚Ìê‡‚Í‰½‚à‚µ‚È‚¢
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        Vector3 delta = transform.position - _prevPos;
        float speed = delta.magnitude / Time.deltaTime;
        _prevPos = transform.position;
        Debug.Log("Œ»İ‚Ì‘¬“x‚Í" + speed);
    }
}
