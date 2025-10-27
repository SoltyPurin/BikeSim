using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGearChange : MonoBehaviour
{
    private BaseBike _base = default;
    private BikeStatus _status = default;
    private Rigidbody _rigidBody = default;

    [SerializeField, Header("ギアチェンジのクールタイム")]
    private float _gearChangeCoolTime = 5.0f;
    private bool _isUnlockCoolTime = true;
    private void Start()
    {
        _base = GetComponent<BaseBike>();
        _status = _base.Status;
        _rigidBody = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// 現在の速度を加味してギアアップが可能か判断する
    /// </summary>
    /// <param name="maxSpeed">現在のギアの最大速度を渡す</param>
    /// <returns>ギアアップが可能かを返す</returns>
    public bool MesureSpeed(float maxSpeed)
    {
        if (!_isUnlockCoolTime)
        {
            return false;
        }
        bool canGearUp = false;
        float gearConnectValue = maxSpeed* _status.SuccessGearChangeRatio;
        if (CalcCurrentBikeSpeed() >= gearConnectValue)
        {
            canGearUp = true;
            _isUnlockCoolTime = false;
            DoSomethingWaitGearChangeCoolTime();
        }
        return canGearUp;
    }


    /// <summary>
    /// 現在のバイクの速度を計測する
    /// </summary>
    /// <returns>現在の速度</returns>
        private float CalcCurrentBikeSpeed()
    {
        float speed = (float)Mathf.Sqrt(Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.z, 2));
        return speed;
    }

    private async UniTask DoSomethingWaitGearChangeCoolTime()
    {
        await UniTask.WaitForSeconds(_gearChangeCoolTime);
        _isUnlockCoolTime = true;
        Debug.Log("クールタイム明けました");
    }
}
