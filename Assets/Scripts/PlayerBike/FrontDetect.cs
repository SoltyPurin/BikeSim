using UnityEngine;

public class FrontDetect : MonoBehaviour
{
    [SerializeField] private BaseBike _baseBike;
    [SerializeField] private float _detectionDistance = 2.0f;
    [SerializeField]private Vector3 _halfSize = new Vector3(1.5f, 0.1f, 1f);
    private RaycastHit _hit;
    private int layerMask = default;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("ObstacleOnly");
        Debug.Log(layerMask);
    }
    private void FixedUpdate()
    {
        //Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        //Vector3 direction = transform.forward;

        //if(Physics.Raycast(rayOrigin, direction, out _hit, _detectionDistance,layerMask))
        //{
        //    _baseBike.EngineStop();
        //    Debug.DrawRay(rayOrigin, direction * _detectionDistance, Color.red);
        //}
        //else
        //{
        //    Debug.DrawRay(rayOrigin, direction * _detectionDistance, Color.green);

        //}
        DetectForward();
    }

    private void DetectForward()
    {
        Vector3 center = transform.position ;
        Vector3 direction = transform.forward;
        bool isHit = Physics.BoxCast(center, _halfSize,direction,out _hit, Quaternion.identity, _detectionDistance,layerMask);

        if (isHit)
        {
            Debug.Log("壁に激突");
            _baseBike.EngineStop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // 中心座標（バイクの前方1m）
        Vector3 center = transform.position;

        // 回転（必要ならtransform.rotationにする）
        Quaternion rotation = transform.rotation;

        // Boxを描画
        Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.identity, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _halfSize);
    }
}
