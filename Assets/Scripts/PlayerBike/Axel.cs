using UnityEngine;
using UnityEngine.InputSystem;

public class CAxel : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _rightTrigger = default;
    [SerializeField] private bool _isAIControl = false;

    [SerializeField] BaseBike _baseBike = default;

    [SerializeField] private bool _isAutomatic = false;
    public float RightTrigger
    {
        get { return _rightTrigger; }
    }

    private void Awake()
    {
        _gamePad = Gamepad.current;
    }

    private void FixedUpdate()
    {
        _rightTrigger = _gamePad.rightTrigger.ReadValue();
        if(_isAutomatic)
        {
            _baseBike.UpdateAxelValue(_rightTrigger);
            Debug.Log(_rightTrigger);

        }
    }

}
