using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Crach : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _leftTrigger = default;
    [SerializeField]private Text _crachText = default;

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

        _crachText.text = "ƒNƒ‰ƒbƒ`:" + _leftTrigger.ToString("F1");
    }
}
