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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6 /*|| collision.gameObject.layer == 0*/)
        {
            Debug.Log("ƒGƒ“ƒXƒg");
            _baseBike.EngineStop();
        }
    }
}
