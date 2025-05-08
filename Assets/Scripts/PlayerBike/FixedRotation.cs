using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    private const float RIGHTROTATIONLIMIT = -90.0f;
    private const float LEFTROTATIONLIMIT = 90.0f;
    [SerializeField] private BaseBike _baseBike;

    private void FixedUpdate()
    {
        RotationFix();
    }

    private void RotationFix()
    {
        float zRotation = NormalizeAngle(transform.eulerAngles.z);

        if (zRotation >= 60f || zRotation <= -60f)
        {
            _baseBike.EngineStop();
            Vector3 fixedEuler = transform.eulerAngles;
            fixedEuler.z = 0f;
            transform.eulerAngles = fixedEuler;
        }
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    //private void RotationFix()
    //{
    //    Vector3 angle = transform.eulerAngles;
    //    //‰E‚É“|‚ê‚é
    //    if(transform.eulerAngles.z <= RIGHTROTATIONLIMIT)
    //    {
    //        _baseBike.EngineStop();
    //        transform.eulerAngles = new Vector3(angle.x,angle.y,0);
    //    }
    //    else if(transform.eulerAngles.z >= LEFTROTATIONLIMIT)
    //    {
    //        _baseBike.EngineStop();
    //        transform.eulerAngles = new Vector3(angle.x, angle.y, 0);

    //    }
    //}
}
