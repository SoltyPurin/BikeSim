using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeSpawn : MonoBehaviour
{
    private GameObject _bikeButtonObject = default;
    private SelectBikeButton _selectBikeButton = default;
    private readonly string TITLEMANAGERTAG = "TitleManager";
    private int _bikeIndex = default;
    [SerializeField]private GameObject[] _bikeObjectArray = new GameObject[4];
    private Quaternion _spawnRotation = Quaternion.Euler(0, -90, 0);
    private void Start()
    {
        _bikeButtonObject = GameObject.FindWithTag(TITLEMANAGERTAG);
        _selectBikeButton = _bikeButtonObject.GetComponent<SelectBikeButton>();
        _bikeIndex = _selectBikeButton.BikeNumber;
        SceneManager.UnloadSceneAsync("Title");
        Instantiate(_bikeObjectArray[_bikeIndex],transform.position,_spawnRotation);
    }
}
