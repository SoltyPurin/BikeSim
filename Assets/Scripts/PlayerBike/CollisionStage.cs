using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStage : MonoBehaviour
{
    int _layerMask = default;
    private BaseBike _baseBike = default;

    private void Awake()
    {
        _baseBike = GetComponent<BaseBike>();
        _layerMask = LayerMask.GetMask("ObstacleOnly");
        Debug.Log("取得するレイヤーマスクは" + _layerMask);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("接触したオブジェクトのレイヤーは" + collision.gameObject.layer);
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 0)
        {
            Debug.Log("車体が壁に接触");
            _baseBike.EngineStop();
        }
    }
}
