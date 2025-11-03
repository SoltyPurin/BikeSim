using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerDetect : MonoBehaviour
{
    [SerializeField, Header("表示するスプライト")]
    private Image _viewSprite = default;

    private ViewCurveSprite _viewSpriteScript = default;

    private void Start()
    {
        _viewSpriteScript = transform.parent.GetComponent<ViewCurveSprite>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
