using UnityEngine;

public class Suspension : MonoBehaviour
{
    private RaycastHit _raycastHit;
    private int _layerMask;
    private float _distance = 0.1f;
    private bool _isGrounded;

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
        _isGrounded = Physics.Raycast(rayOrigin, rayDirection, out _raycastHit, _distance, _layerMask);
        Debug.DrawRay(rayOrigin, rayDirection * _distance, Color.red);
        if (_isPlayer)
        {
            Debug.Log("’n–Ê”»’è‚Í" + _isGrounded);

        }

        if (!_isGrounded)
        {
            float fallSpeed = 1.5f;
            Vector3 newPosition = transform.position;
            newPosition.y -= fallSpeed * Time.deltaTime;
            transform.position = newPosition;

        }
    }
}
