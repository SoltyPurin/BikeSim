using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBikeInitializer : MonoBehaviour
{
    [SerializeField,Header("テストする際にこのリストにぶち込んでおくこと")]
    private List<GameObject> _aiBikes = new List<GameObject>();
    [SerializeField, Header("何秒後にイニシャライズを行うか")]
    private int _initializeTime = 3;
    [SerializeField, Header("ランキングのスクリプト")]
    private RankingManager _rankingManager = default;
    [SerializeField, Header("監視者の親")]
    private ObservationParent _observationParent = default;

    private AIUpdater _updater = default;

    public void ListAdder(GameObject obj)
    {
        _aiBikes.Add(obj);
    }
    private async void Start()
    {
        await UniTask.WaitForSeconds(_initializeTime);
        _updater = GetComponent<AIUpdater>();
        foreach(GameObject ai in _aiBikes)
        {
            IAiInitializer initialize = ai.GetComponent<IAiInitializer>();
            initialize.Initialize();
            _updater.Initialize(ai);
        }
        _rankingManager.ScriptAwake();
        _observationParent.Initializer();
    }
}
