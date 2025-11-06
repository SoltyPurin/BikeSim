using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVChange : MonoBehaviour
{
    [SerializeField, Header("最大視野角")]
    private float _maxFOV = 120;
    [SerializeField, Header("速度に掛け算して視野が広がるのを制限する値")]
    private float _speedMultiplier = 0.5f;
    [SerializeField,Header("一人称カメラ")]
    private CinemachineVirtualCamera _fpsCamera = default;
    [SerializeField,Header("三人称カメラ")]
    private CinemachineVirtualCamera _tpsCamera = default;
    [SerializeField, Header("リジッドボディ")]
    private Rigidbody _rigidBody = default;

    private BaseBike _baseBike = default;

    private float _firstFOV = 60;

    private void Start()
    {
        _baseBike = GetComponent<BaseBike>();
        _firstFOV = _fpsCamera.m_Lens.FieldOfView;
    }

    private void LateUpdate()
    {
        float fovValue = (CalcSpeed() * _speedMultiplier) + _firstFOV;
        fovValue *= 10;
        fovValue = Mathf.Floor(fovValue);
        fovValue /= 10;
        _fpsCamera.m_Lens.FieldOfView = fovValue;
        _tpsCamera.m_Lens.FieldOfView = fovValue;
    }

    private float CalcSpeed()
    {
        float speed = (float)Mathf.Sqrt(Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.z, 2));
        return speed;
    }
}
