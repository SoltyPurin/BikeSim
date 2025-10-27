using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompareWhitchSidePlayer : MonoBehaviour
{
    //次のウェイポイントととの距離を測る
    //そしてそのウェイポイントとの距離がどちらが短いかで判断する
    [SerializeField, Header("ウェイポイントからプレイヤーが何m離れてたら返り値を強制的にfalseにするか")]
    private float _playerFrontM = 100f;
    private GameObject _playerObj = default;
    public void Initialize(GameObject playerObj)
    {
        _playerObj = playerObj;
    }
    /// <summary>
    /// 現在プレイヤーより前にいるかどうか
    /// </summary>
    /// <param name="currentWayPoint">現在のウェイポイント</param>
    /// <returns>プレイヤーの前にいるかどうか</returns>
    public bool IsCurrentFront(Vector3 currentWayPoint)
    {
        float enDistance = Vector3.Distance(currentWayPoint,this.transform.position);
        float plDistance = Vector3.Distance(currentWayPoint, _playerObj.transform.position);

        Debug.Log("敵のウェイポイントとの距離は" + enDistance);
        Debug.Log("プレイヤーのウェイポイントとの距離は" + plDistance);

        bool isFront = enDistance < plDistance;

        if(plDistance >= _playerFrontM)
        {
            isFront = false;
        }

        return isFront;
    }
}
