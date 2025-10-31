using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private WhoTheHead _head = default;

    private void Awake()
    {
        _head = GetComponent<WhoTheHead>();
        _head.Initialize();
    }

    private void FixedUpdate()
    {
        _head.Run();
    }
}
