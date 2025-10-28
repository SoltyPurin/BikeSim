using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBikeInitializer : MonoBehaviour
{
    [SerializeField, Header("AI‚ÌƒoƒCƒN‚ð“o˜^")]
    private List<GameObject> _aiBikes = new List<GameObject>();

    private AIUpdater _updater = default;
    private void Start()
    {
        _updater = GetComponent<AIUpdater>();
        foreach(GameObject ai in _aiBikes)
        {
            IAiInitializer initialize = ai.GetComponent<IAiInitializer>();
            initialize.Initialize();
            _updater.Initialize(ai);
        }
    }
}
