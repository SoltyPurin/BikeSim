using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUpdater : MonoBehaviour
{
    [SerializeField, Header("プレイヤーとウェイポイントを監視するプログラム")]
    private ObservationPlayerNearWayPoint _observer = default;

    private List<IAIUpdater> _updaterList = new List<IAIUpdater>();

    public void Initialize(GameObject aiObj)
    {
        _updaterList.Add(aiObj.GetComponent<IAIUpdater>());
    }

    private void FixedUpdate()
    {
        foreach(IAIUpdater updater in _updaterList)
        {
            updater.InterfaceUpdate(_observer);
        }
    }
}
