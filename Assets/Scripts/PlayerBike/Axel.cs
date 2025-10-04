using UnityEngine;
using UnityEngine.InputSystem;

public class Axel : MonoBehaviour
{
    private Gamepad _gamePad;
    private float _rightTrigger = default;
    [SerializeField] private bool _isAIControl = false;

    [SerializeField] BaseBike _baseBike = default;

    public float RightTrigger
    {
        get { return _rightTrigger; }
    }

    private void Awake()
    {
        _gamePad = Gamepad.current;
    }

    private void Update()
    {
        _rightTrigger = _gamePad.rightTrigger.ReadValue();
        //Debug.Log(_rightTrigger);
        float outPutValue = Mathf.Clamp(_rightTrigger * 100, 0.1f, 100);
        _baseBike.UpdateAxelValue(outPutValue);
    }

}
