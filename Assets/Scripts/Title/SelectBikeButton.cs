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
        _bikeNumber = AUTOMATIC;
        EnterHonpen();
    }

    public void Clutch()
    {
        _bikeNumber = CLUTCH;
        EnterHonpen();
    }

    public void Handring()
    {
        _bikeNumber = HANDRING;
        EnterHonpen();
    }

    public void Speed()
    {
        _bikeNumber = SPEED;
        EnterHonpen();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void EnterHonpen()
    {
        SceneManager.LoadScene(HONPENNAME,LoadSceneMode.Additive);
    }
}
