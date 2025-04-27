using UnityEngine;
using UnityEngine.InputSystem;

public class Crach : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _leftTrigger = default;

    public float LeftTrigger
    {
        get { return _leftTrigger; }
    }

    private void Start()
    {
       if(_gamePad == null)
        {
            return;
        } 
    }

    private void FixedUpdate()
    {
        _gamePad = Gamepad.current;

        _leftTrigger =  1.0f -_gamePad.leftTrigger.ReadValue();

    }
}
