using UnityEngine;

public class Suspension : MonoBehaviour
{
    private RaycastHit _raycastHit;
    private int _layerMask;
    [SerializeField,Header("レイの長さ")]
    private float _distance = 1f;
    private bool _isGrounded;
    [SerializeField, Header("中心から見て横のレイのスタート地点")]
    private float _besideRayPos = 0.5f;

    [SerializeField, Header("下げる値")]
    private float _downValue = 0.1f;
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            return;
        }

        Vector3 curPos = transform.position;
        curPos.y -= _downValue;
        transform.position = curPos;
    }
    /// <summary>
    /// 左右どちらかが浮いてるか確認するメソッド
    /// </summary>
    /// <returns>浮いてるかどうか</returns>
    private bool IsGrounded()
    {
        bool isGrounded = false;
        Vector3 rayDirection = Vector3.down;
        Vector3 underRay = transform.position + rayDirection * _besideRayPos;
        if (Physics.Raycast(underRay, rayDirection, out _raycastHit, _distance, _layerMask))
        {
            isGrounded = true;
            Debug.Log("触れてる");
        }
        else
        {
            Debug.Log("触れてない");
        }
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Vector3 rayDirection = Vector3.down;
        Vector3 underRay = transform.position + rayDirection * _besideRayPos;


        Debug.DrawRay(underRay, rayDirection * _distance, Color.red);
    }
}
