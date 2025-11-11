using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeSpawn : MonoBehaviour
{
    private GameObject _bikeButtonObject = default;
    //private SelectBikeButton _selectBikeButton = default;
    private readonly string TITLEMANAGERTAG = "TitleManager";
    private int _bikeIndex = default;
    [SerializeField]private GameObject[] _bikeObjectArray = new GameObject[4];
    [SerializeField, Header("バイクをスポーンさせる場所")]
    private Transform _bikeSpawnPoint = default;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            return;
        }
        _bikeIndex = PlayerPrefs.GetInt("BikeNumber");
        Instantiate(_bikeObjectArray[_bikeIndex],_bikeSpawnPoint.position,_bikeSpawnPoint.rotation);
    }

}
