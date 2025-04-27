using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Clutch : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _leftTrigger = default;
    [SerializeField]private Text _clutchText = default;

    public float LeftTrigger
    {
        get { return _leftTrigger; }
    }

    private void Start()
    {
      _gamePad = Gamepad.current;
    }

    private void FixedUpdate()
    {
        _leftTrigger =  1.0f -_gamePad.leftTrigger.ReadValue();

        _clutchText.text = "ƒNƒ‰ƒbƒ`:" + _leftTrigger.ToString("F1");
    }
}
