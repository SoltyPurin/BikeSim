using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetectGearChangeCurve : MonoBehaviour
{
    [SerializeField, Header("Ç«ÇÍÇ≠ÇÁÇ¢ã»Ç™Ç¡ÇƒÇ¢ÇΩÇÁÉMÉAÇâ∫Ç∞ÇÈÇ©")]
    private float _curveThshould = 10f;

    private BaseBike _bike = default;

    public void Initialize()
    {
        _bike = GetComponent<BaseBike>();
    }

    public bool CheckCurve(Vector3 target)
    {
        bool isGearUp = false;
        Vector3 prev = transform.position;
        Vector3 diff = prev - target;
        Vector3 axis = Vector3.Cross(transform.forward, diff);
        float angle = Vector3.Angle(transform.forward, diff) * (axis.y <0 ? -1:1);

        if(angle > _curveThshould)
        {
            _bike.DownGear();
            if(_bike.CurrentGearIndex == 1)
            {
                _bike.DownGear();
            }
            Debug.Log("ÉMÉAâ∫Ç∞ÇÈ");
        }
        else
        {
            _bike.UpGear();
            if (_bike.CurrentGearIndex == 1)
            {
                _bike.UpGear();
            }
            isGearUp = true;
            Debug.Log("ÉMÉAè„Ç∞ÇÈ");
        }

        return isGearUp;
    }
}
