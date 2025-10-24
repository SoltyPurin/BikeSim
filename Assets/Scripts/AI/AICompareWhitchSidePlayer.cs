using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICompareWhitchSidePlayer : MonoBehaviour
{
    private GameObject _playerObj = default;
    public void Initialize(GameObject playerObj)
    {
        _playerObj = playerObj;
    }
    public bool IsCurrentFront(float errorValue)
    {
        bool isFront = false;

        bool isTravelDirectionMatch = false;

        Vector3 currentRotation = this.gameObject.transform.rotation.eulerAngles;

        Vector3 playerRotation = _playerObj.transform.rotation.eulerAngles;

        float curY = currentRotation.y; 
        float playerY = playerRotation.y;
        //現在のY軸回転とプレイヤーのY軸回転を比較して
        //近似値であれば進行方向が合致している
        if (curY -  playerY < errorValue)
        {
            isTravelDirectionMatch = true;
        }

        if (!isTravelDirectionMatch)
        {
            return false;   
        }

        //その状態で進行方向を計測
        //進行方向がx,zがそれぞれプラス方面かどうかを判断して
        //前後の判定を行う


        return isFront;
    }
}
