using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private WhoTheHead _head = default;

    private bool _isStarting = false;

    public void ScriptAwake()
    {
        _head = GetComponent<WhoTheHead>();
        _head.Initialize();
        _isStarting = true;
    }

    private void FixedUpdate()
    {
        if (!_isStarting)
        {
            return;
        }
        _head.Run();
    }
}
