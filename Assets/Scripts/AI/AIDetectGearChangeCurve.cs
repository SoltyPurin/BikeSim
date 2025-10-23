using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetectGearChangeCurve : MonoBehaviour
{
    [SerializeField, Header("どれくらい曲がっていたらギアを下げるか")]
    private float _curveThshould = 10f;

    private AIBikeController _controll = default;

    public void Initialize()
    {
        _controll = GetComponent<AIBikeController>();
    }

    /// <summary>
    /// カーブを確認してギアチェンジをするかどうかを返す。
    /// </summary>
    /// <param name="target">目的地</param>
    /// <returns>ギアを上げるかの値を返す。0が↑、1が↓,2が→</returns>
    public int CheckCurve(Vector3 target)
    {
        //返す値をintにして実際のギアチェンジは呼び出し元で行うようにする
        //0はギアあげる、1はギア下げる、2はそのままで
        int retValue = 2;
        Vector3 prev = transform.position;
        Vector3 diff = prev - target;
        Vector3 axis = Vector3.Cross(transform.forward, diff);
        float angle = Vector3.Angle(transform.forward, diff) * (axis.y <0 ? -1:1);

        if(angle > _curveThshould)
        {
            retValue = 1;
        }
        else if(angle < _curveThshould)
        {
            retValue = 0;
        }else if ((angle == _curveThshould))
        {
            retValue = 2;
        }

        return retValue;
    }
}
