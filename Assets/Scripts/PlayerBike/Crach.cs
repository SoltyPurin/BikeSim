using UnityEngine;
using UnityEngine.InputSystem;

public class Crach : MonoBehaviour
{
    private Gamepad _gamePad;

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

        float lt = _gamePad.leftTrigger.ReadValue();

        Debug.Log("現在のクラッチの値は" +  lt);
    }
}
