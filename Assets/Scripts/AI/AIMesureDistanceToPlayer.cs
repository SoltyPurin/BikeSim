using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMesureDistanceToPlayer : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";
    private GameObject _playerObj = default;

    public void Initialize()
    {
        _playerObj = GameObject.FindWithTag(PLAYER_TAG);
    }

    public bool MesureDistance(float compareDistance)
    {
        bool isFarAway = false;
        float distance = Vector3.Distance(_playerObj.transform.position,this.transform.position);

        if(distance > compareDistance)
        {
            isFarAway = true;
        }
        return isFarAway;
    }
}
