using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBikeButton : MonoBehaviour
{
    private int _bikeNumber = default;
    public int BikeNumber
    {
        get {  return _bikeNumber; }
    }
    private const int AUTOMATIC = 0;
    private const int CLUTCH = 1;
    private const int HANDRING = 2;
    private const int SPEED = 3;

    private readonly string HONPENNAME = "Honpen";

    public void Automatic()
    {
        PlayerPrefs.SetInt("BikeNumber",AUTOMATIC);
        EnterHonpen();
    }

    public void Clutch()
    {
        PlayerPrefs.SetInt("BikeNumber", CLUTCH);
        EnterHonpen();
    }

    public void Handring()
    {
        PlayerPrefs.SetInt("BikeNumber", HANDRING);
        EnterHonpen();
    }

    public void Speed()
    {
        PlayerPrefs.SetInt("BikeNumber", SPEED);
        EnterHonpen();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void EnterHonpen()
    {
        //if (SceneManager.GetSceneByName(HONPENNAME).isLoaded)
        //{
        //    return;
        //}
        SceneManager.LoadSceneAsync(HONPENNAME);
    }
}
