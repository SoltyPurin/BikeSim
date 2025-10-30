using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AIStuckCounterPlan : MonoBehaviour
{
    [SerializeField, Header("状態をスタックと決定する時間")]
    private int _stuckTolleranceTime = 3;
    [SerializeField, Header("前回の地点との距離がどれくらい以内であればスタックと判定するか")]
    private float _stuckDecisiveValue = 3.0f;

    private AIReturnPrevPosition _returnScript = default;
    private float _currentCountTime = 0;
    private Vector3 _currentPosition = Vector3.zero;
    private Vector3 _prevPosition = Vector3.zero;
    //private CancellationToken _token = default;

    private void Start()
    {
        _returnScript = GetComponent<AIReturnPrevPosition>();
        _currentPosition = transform.position;
        _prevPosition = transform.position;
        //_token = this.GetCancellationTokenOnDestroy();
        ////↑ゲームオブジェクトがデストロイされたときにキャンセル発行
        ////トークンはシーン通して一つにするべき
        ///
    }
    private void FixedUpdate()
    {
        _currentPosition = transform.position;
        _currentCountTime += Time.fixedDeltaTime;
        if(_currentCountTime >= _stuckTolleranceTime)
        {
            //Debug.Log("チェックします");
            CheckStuck();
        }
    }

    private void CheckStuck()
    {
        float prevDistance = Vector3.Distance(_currentPosition, _prevPosition);
        if (prevDistance < _stuckDecisiveValue)
        {
            _returnScript.Jugemu();
        }
        _prevPosition = transform.position;
        _currentCountTime = 0;

    }

    //private async UniTask<bool> IsStuck(CancellationToken token)
    //{
    //    await UniTask.WaitForEndOfFrame
    //        try
    //    {
    //        //例外を監視しながら実行する

    //        await UniTask.WaitForSeconds(duration:_stuckTolleranceTime, cancellationToken:token);
    //    }
    //    catch
    //    {
    //        //例外が起きたらこっちに行く
    //        //ゲームが終了するときとかにこっちに行くらしい
    //        //
    //        throw new Exception();
    //        //↑例外を投げられる。意図的に1/0等の色々なエラーを出す
    //        return false;
    //    }
    //    await UniTask.Delay(TimeSpan.FromSeconds((_stuckTolleranceTime),token);
    //    bool isStucking = false;
    //    float distance = Vector3.Distance(_prevPosition, _currentPosition);
    //    if(distance < _stuckDecisiveValue)
    //    {
    //        isStucking=true;
    //    }
    //    _prevPosition = transform.position;
    //    return isStucking;

    //}
}
