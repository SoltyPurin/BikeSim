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
    public bool IsGrounded
    {
        get { return _isGrounded; }
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(rayOrigin, rayDirection * _distance, Color.red);
        float curZ = transform.rotation.z;

        switch (ReturnLeftOrRightFloat())
        {
            case 0:
                Debug.Log("浮いてない");
                break;

            case 1:
                //左が浮いてるためZプラス
                curZ += _downValue;
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, curZ);
                break;

            case 2:
                //右が浮いてるためZマイナス
                curZ -= _downValue;
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, curZ);

                break;

            default:
                break;
        }

        //if (!_isGrounded)
        //{
        //    float fallSpeed = 1.5f;
        //    Vector3 newPosition = transform.position;
        //    newPosition.y -= fallSpeed * Time.deltaTime;
        //    transform.position = newPosition;

        //}
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
        if (Physics.Raycast(leftRay,rayDirection,out _raycastHit, _distance, _layerMask))
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

    private void OnDrawGizmos()
    {
        Vector3 rayDirection = Vector3.down;
        Vector3 leftRay = transform.position + Vector3.left * _besideRayPos;
        Vector3 rightRay = transform.position + Vector3.right * _besideRayPos;

        Debug.DrawRay(leftRay, rayDirection * _distance, Color.red);
        Debug.DrawRay(rightRay, rayDirection * _distance, Color.red);
    }
}
