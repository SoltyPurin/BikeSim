using UnityEngine;
using UnityEngine.InputSystem;

public class Clutch : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _leftTrigger = default;
    [SerializeField] private bool _isAIControl = false;

    [SerializeField] BaseBike _baseBike = default;
    public float LeftTrigger
    {
        get { return _leftTrigger; }
    }

    private void Awake()
    {
      _gamePad = Gamepad.current;
    }

    private void FixedUpdate()
    {
        if (_isAIControl)
        {
            //AIÇ…ÇÊÇÈìÆçÏíÜÇÕÇ»Ç…Ç‡ÇµÇ»Ç¢
            return;
        }
        _leftTrigger = 1.0f - _gamePad.leftTrigger.ReadValue();
        _baseBike.UpdateClutchValue(LeftTrigger);

    }
}
