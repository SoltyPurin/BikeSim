using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeZoneSet : MonoBehaviour
{
    [SerializeField,Header("各時間帯ごとのDirectionalLightの回転")]
    private List<Vector3>_timeZoneDirectionalLightRotation = new List<Vector3>();
    [SerializeField, Header("ステージのDirectionalLight")]
    private GameObject _directionalLight = default;
    private void Awake()
    {
        int timeZoneint = PlayerPrefs.GetInt("TimeZone", 0);
        switch (timeZoneint)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;
        }

        Vector3 rota = _timeZoneDirectionalLightRotation[timeZoneint];
        _directionalLight.transform.rotation = Quaternion.Euler(rota);
    }
}
