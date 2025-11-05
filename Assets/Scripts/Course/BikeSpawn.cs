using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeSpawn : MonoBehaviour
{
    private GameObject _bikeButtonObject = default;
    private SelectBikeButton _selectBikeButton = default;
    private readonly string TITLEMANAGERTAG = "TitleManager";
    private int _bikeIndex = default;
    [SerializeField]private GameObject[] _bikeObjectArray = new GameObject[4];
    [SerializeField, Header("バイクをスポーンさせる場所")]
    private Vector3 _bikeSpawnPoint = default;

    [SerializeField,Header("スタート時のバイクの回転")]
    private Quaternion _quaternion = Quaternion.identity;
    private void Awake()
    {
        _bikeButtonObject = GameObject.FindWithTag(TITLEMANAGERTAG);
        if (_bikeButtonObject != null)
        {
            _selectBikeButton = _bikeButtonObject.GetComponent<SelectBikeButton>();
            _bikeIndex = _selectBikeButton.BikeNumber;
            SceneManager.UnloadSceneAsync("Title");
            Instantiate(_bikeObjectArray[_bikeIndex],_bikeSpawnPoint,_quaternion);

        }
        else
        {
            return;
        }

    }
}
