using UnityEngine;

public class Suspension : MonoBehaviour
{
    private RaycastHit _raycastHit;
    private int _layerMask;
    private float _distance = 0.5f;
    private void FixedUpdate()
    {
        if(!Physics.Raycast(transform.position, Vector3.down, _distance, _layerMask))
        {

        }

       
    }
}
