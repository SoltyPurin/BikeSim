using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompareWhitchSidePlayer : MonoBehaviour
{
    //次のウェイポイントととの距離を測る
    //そしてそのウェイポイントとの距離がどちらが短いかで判断する
    private GameObject _playerObj = default;
    public void Initialize(GameObject playerObj)
    {
        _playerObj = playerObj;
    }
    /// <summary>
    /// 現在プレイヤーより前にいるかどうか
    /// </summary>
    /// <returns>プレイヤーの前にいるかどうか</returns>
    public bool IsCurrentFront(int plIndex,int enIndex,bool isPlayerSecondRound,bool isEnemySecondRound)
    {
        bool isFront = false;
        if(isPlayerSecondRound && !isEnemySecondRound)
        {
            Debug.Log("プレイヤーの方が2週目");
            isFront = true;
        }else if(!isPlayerSecondRound && isEnemySecondRound)
        {
            Debug.Log("敵の方が2週目");
            isFront = false;
        }
        else
        {
            Debug.Log("同じ周回");
            isFront = enIndex >= plIndex;
        }

        return isFront;
    }
}
