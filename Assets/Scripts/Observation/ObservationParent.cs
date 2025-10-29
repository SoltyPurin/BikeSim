using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationParent : MonoBehaviour
{
    private GameObject _player = default;
    private ObservationPlayerNearWayPoint _wayPoint = default;
    private ObservationPlayerSpeed _speed = default;

    private readonly string PLAYER_TAG = "Player";

    private void Awake()
    {
        _wayPoint = GetComponent<ObservationPlayerNearWayPoint>();
        _speed = GetComponent<ObservationPlayerSpeed>();
        _player = GameObject.FindWithTag(PLAYER_TAG);
        _wayPoint.Initialize(_player);
        _speed.Initialize(_player);
    }

    private void FixedUpdate()
    {
        _wayPoint.Run();
        _speed.Run();
    }
}
