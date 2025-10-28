using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ObservationPlayerNearWayPoint : MonoBehaviour
{
    [SerializeField, Header("ウェイポイントが登録されてる親オブジェクト")]
    private Transform _wayPointParent = default;

    private GameObject _player = default;
    private List<Vector3> _wayPointPos = new List<Vector3>();
    private int _listCount = 0;
    private float _mostNearDistance = 0f;
    private int _mostPlayerNearPointIndex = 0;
    public int MostPlayerNearPointIndex
    {
        get { return  _mostPlayerNearPointIndex; }
    }

    private readonly string PLAYER_TAG = "Player";
    private void Awake()
    {
        _player = GameObject.FindWithTag(PLAYER_TAG);
        _listCount = _wayPointParent.childCount;
        for(int i =0; i < _listCount; i++)
        {
            _wayPointPos.Add(_wayPointParent.GetChild(i).position);
        }
        float distance = Vector3.Distance(_player.transform.position, _wayPointPos[0]);
        _mostNearDistance = distance;
        _mostPlayerNearPointIndex = 0;
    }

    private void Update()
    {
        _mostNearDistance = Mathf.Infinity;
        for (int i = 0; i < _listCount; i++)
        {
            float distance = Vector3.Distance(_player.transform.position, _wayPointPos[i]);
            if (distance < _mostNearDistance)
            {
                _mostNearDistance = distance;
                _mostPlayerNearPointIndex = i;
            }
        }
        //Debug.Log("プレイヤーに一番近い座標インデックスは" + _mostPlayerNearPointIndex);
    }
}
