using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGearChange : MonoBehaviour
{
    private BaseBike _base = default;
    private BikeStatus _status = default;
    private Rigidbody _rigidBody = default;
    private void Start()
    {
        _base = GetComponent<BaseBike>();
        _status = _base.Status;
        _rigidBody = GetComponent<Rigidbody>();
    }
    public bool MesureSpeed(float maxSpeed)
    {
        bool canGearUp = false;
        float gearConnectValue = maxSpeed* _status.SuccessGearChangeRatio;
        Debug.Log("Œ»Ý‚Ì‘¬“x‚Í" + CalcCurrentBikeSpeed());
        if (CalcCurrentBikeSpeed() >= gearConnectValue)
        {
            canGearUp = true;
        }
        return canGearUp;
    }

        private float CalcCurrentBikeSpeed()
    {
        float speed = (float)Mathf.Sqrt(Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.z, 2));
        return speed;
    }

}
