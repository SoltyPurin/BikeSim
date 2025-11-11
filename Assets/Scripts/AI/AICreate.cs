using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AICreate : MonoBehaviour
{
    [SerializeField, Header("AIのオブジェクト")]
    private GameObject _aiBike = default;
    [SerializeField, Header("AIの初期化を行うスクリプト")]
    private AIBikeInitializer _initializer = default;
    private GameObject _getObject = default;
    private OptionButtonScript _optionButtonScript = default;
    private readonly string TITLEMANAGERTAG = "TitleManager";
    [SerializeField, Header("バイクをスポーンさせる場所のリスト")]
    private List<Transform> _spawnPos = new List<Transform>();
    private Quaternion _spawnRota = Quaternion.Euler(0,90,0);   

    private void Awake()
    {
        //_getObject = GameObject.FindWithTag(TITLEMANAGERTAG);
        //if (_getObject != null)
        //{
            //_optionButtonScript = _getObject.GetComponent<OptionButtonScript>();
            int enemyCount = PlayerPrefs.GetInt("EnemyCount");
            Debug.Log("AIの数は" + enemyCount);
            CreatAIProtocol(enemyCount);
        //}
    }

    private void CreatAIProtocol(int value)
    {
        for(int i = 0; i<value; i++)
        {
            GameObject obj= Instantiate(_aiBike, _spawnPos[i].position, _spawnRota);
            _initializer.ListAdder(obj);
        }
    }
}