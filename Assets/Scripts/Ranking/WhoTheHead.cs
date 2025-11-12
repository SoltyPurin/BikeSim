using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhoTheHead : MonoBehaviour
{
    //バグが多発するので要修正
    [SerializeField, Header("プレイヤーのウェイポイント監視者")]
    private ObservationPlayerNearWayPoint _playerNearWayPoint = default;
    private List<GameObject> _bikers = new List<GameObject>();
    private GameObject _playerObj = default;

    [SerializeField, Header("順位を書き出すテキスト")]
    private Text _rankingText = default;

    private readonly string PLAYER_TAG = "Player";
    private readonly string ENEMY_TAG = "Enemy";

    //自分がどこの順位にいるかを表示するだけでいい
    public void Initialize()
    {
        GameObject[] bike = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        foreach (GameObject enBikeObj in bike)
        {
            _bikers.Add(enBikeObj);
        }
        _playerObj = GameObject.FindWithTag(PLAYER_TAG);

        //GameObject player = GameObject.FindWithTag("Player");
        //_bikers.Add(player);
    }

    public void Run()
    {
        //プレイヤーに一番近いウェイポイントを起点として考える。
        //そこから一番遠い敵の目指しているウェイポイントを取得
        //そのインデックスがプレイヤーウェイポイントより小さい(後ろにいる)場合は
        //プレイヤーウェイポイントとのDistanceをとって順位付け
        //もしインデックスがプレイヤーウェイポイントより大きい(前にいる)場合は
        //そのウェイポイントから全員のDistanceを測って一番値が小さいやつが先頭
        Vector3 playerNearPos = _playerNearWayPoint.NearPosition;
        float farDistance = Vector3.Distance(playerNearPos, _bikers[0].transform.position);
        float nearDistance = Vector3.Distance(playerNearPos, _bikers[0].transform.position);
        GameObject mostFarEnemy = _bikers[0].gameObject;
        GameObject mostNearEnemy = _bikers[0].gameObject;
        //一番遠い敵の場所を探索する
        for(int i =0; i< _bikers.Count; i++)
        {
            float tmpDistance = Vector3.Distance(playerNearPos,_bikers[i].transform.position);
            if(tmpDistance > farDistance)
            {
                farDistance = tmpDistance;
                mostFarEnemy = _bikers[i].gameObject;
            }
        }
        for(int i = 0; i< _bikers.Count; i++)
        {
            float tmpDistance = Vector3.Distance(playerNearPos, _bikers[i].transform.position);
            if(nearDistance > tmpDistance)
            {
                nearDistance = tmpDistance;
                mostNearEnemy = _bikers[i].gameObject;
            }
        }
        AIBikeController nearController = mostNearEnemy.GetComponent<AIBikeController>();
        AIBikeController farController = mostFarEnemy.GetComponent<AIBikeController>();
        int nearIndex = nearController.CurrentWaypointIndex;
        int farIndex = farController.CurrentWaypointIndex;
        //Debug.Log("近い敵の目指してるポイントは" + nearIndex);
        //Debug.Log("遠い敵の目指してるポイントは" + farIndex);
        if(farIndex < nearIndex)
        {
            //Debug.Log("プレイヤーが先頭付近にいるよ");
            PlayerNearHead(playerNearPos,mostNearEnemy);
        }
        else
        {
            //Debug.Log("プレイヤーは後ろの方にいるよ");
            PlayerFarHeader(playerNearPos,farController);
        }
    }

    private void PlayerFarHeader(Vector3 playerNearPos, AIBikeController farControler)
    {
        Vector3 mostFarPoint = farControler.WayPoints[farControler.CurrentWaypointIndex].transform.position;
        float playerDistance = Vector3.Distance(mostFarPoint, _playerObj.transform.position);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        int frontEnemyCount = enemys.Length + 1;
        for(int i = 0; i < enemys.Length; i++)
        {
            float tmpDistance = Vector3.Distance(mostFarPoint, enemys[i].transform.position);
            if(tmpDistance >= playerDistance)
            {
                frontEnemyCount--;
            }
        }

        _rankingText.text = "Rank:" + frontEnemyCount;
    }
    private void PlayerNearHead(Vector3 playerNearPos,GameObject mostNearEnemy)
    {
        float playerDistance = Vector3.Distance(playerNearPos, _playerObj.transform.position);
        float enDistance = Vector3.Distance(playerNearPos, mostNearEnemy.transform.position);
        if (playerDistance < enDistance)
        {
            _rankingText.text = "Rank:" + 1;
        }
        else
        {
            _rankingText.text = "Rank:" + 2;
        }

    }
}
