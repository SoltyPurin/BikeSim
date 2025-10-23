using UnityEngine;

public class Suspension : MonoBehaviour
{
    private RaycastHit _raycastHit;
    private int _layerMask;
    private float _distance = 0.1f;
    private bool _isGrounded;
    [SerializeField, Header("中心から見て横のレイのスタート地点")]
    private float _besideRayPos = 0.5f;

    [SerializeField] private bool _isPlayer = false;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Ground");
    }
    public bool IsGrounded
    {
        get { return _isGrounded; }
    }
    private void FixedUpdate()
    {
        Vector3 rayOrigin = transform.position + Vector3.down * 0.5f;
        Vector3 rayDirection = Vector3.down;
        Debug.DrawRay(rayOrigin, rayDirection * _distance, Color.red);

        if (!_isGrounded)
        {
            float fallSpeed = 1.5f;
            Vector3 newPosition = transform.position;
            newPosition.y -= fallSpeed * Time.deltaTime;
            transform.position = newPosition;

        }
    }
    /// <summary>
    /// 左右どちらかが浮いてるか確認するメソッド
    /// </summary>
    /// <returns>0だったら地面ピッタリ、1は左が浮いてる、2は右が浮いてる</returns>
    private int ReturnLeftOrRightFloat()
    {
        int leftOrRight = 0;
        Vector3 rayDirection = Vector3.down;
        Vector3 leftRay = transform.position + Vector3.left * _besideRayPos;
        Vector3 rightRay = transform.position + Vector3.right * _besideRayPos;
        if(Physics.Raycast(leftRay,rayDirection,out _raycastHit, _distance, _layerMask))
        {
            Debug.Log("左側が浮いてる");
            leftOrRight = 1;
        }else if(Physics.Raycast(rightRay, rayDirection, out _raycastHit, _distance, _layerMask))
        {
            Debug.Log("右側が浮いてる");
            leftOrRight = 2;
        }
        return leftOrRight;
    }
}
