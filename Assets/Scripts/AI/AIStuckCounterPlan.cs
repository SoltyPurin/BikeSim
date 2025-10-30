using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStuckCounterPlan : MonoBehaviour
{
    [SerializeField, Header("どれくらいの誤差ならカウントを続けるか")]
    private float _countContinueThreshold = 10.0f;
    [SerializeField, Header("何秒スタックしたら強制排出するか")]
    private float _stuckTimeCount = 3;

    private Vector3 _prevPosition = default;
    private Vector3 _currentPosition = default;

    private void Update()
    {
        _prevPosition = transform.position;
        if(_prevPosition != _currentPosition)
        {
            Debug.Log("1f前と座標が違います");
        }
        _currentPosition = transform.position;
    }
}
