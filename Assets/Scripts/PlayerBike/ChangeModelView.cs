using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModelView : MonoBehaviour
{
    [SerializeField, Header("バイクのモデルの親オブジェクト")]
    private GameObject _bikeModelParent = default;
    [SerializeField, Header("ライダーのメッシュレンダラー")]
    private SkinnedMeshRenderer _skinMesh = default;
    public void ShadowOnly()
    {
        _skinMesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        Transform children = _bikeModelParent.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            MeshRenderer mesh = ob.gameObject.GetComponent<MeshRenderer>();
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }

    public void ShowModel()
    {
        _skinMesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        Transform children = _bikeModelParent.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            MeshRenderer mesh = ob.gameObject.GetComponent<MeshRenderer>();
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
