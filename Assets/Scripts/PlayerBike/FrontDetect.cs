using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetect : MonoBehaviour
{
    [SerializeField] private BaseBike _baseBike;
    [SerializeField] private float _detectionDistance = 1.0f;
    private RaycastHit _hit;
    private void FixedUpdate()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = transform.forward;

        if(Physics.Raycast(rayOrigin, direction, out _hit, _detectionDistance))
        {
            _baseBike.EngineStop();
            Debug.DrawRay(rayOrigin, direction * _detectionDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayOrigin, direction * _detectionDistance, Color.green);

        }

    }
}
