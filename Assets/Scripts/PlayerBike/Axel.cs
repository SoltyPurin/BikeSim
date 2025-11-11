using UnityEngine;
using UnityEngine.InputSystem;

public class Axel : MonoBehaviour
{
    #region Serializeïœêî
    [SerializeField] private bool _isAIControl = false;

    [SerializeField] BaseBike _baseBike = default;
    #endregion

    #region ïœêî
    private float _rightTriggerValue = default;
    private InputMap _inputMap = default;
    private bool _canStart = false;
    #endregion

    private void Start()
    {
        _inputMap = new InputMap();
        _inputMap.Enable();
    }

    public void PermissionStart()
    {
        _canStart = true;
    }

    private void Update()
    {
        if (!_canStart)
        {
            return;
        }
        _rightTriggerValue = _inputMap.Engine.Axel.ReadValue<float>();
        _baseBike.UpdateAxelValue(_rightTriggerValue);
    }

}
