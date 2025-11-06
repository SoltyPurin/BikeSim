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
    #endregion

    private void Start()
    {
        _inputMap = new InputMap();
        _inputMap.Enable();

    }

    private void Update()
    {
        _rightTriggerValue = _inputMap.Engine.Axel.ReadValue<float>();
        float outPutValue = Mathf.Clamp(_rightTriggerValue * 100, 0.1f, 100);
        Debug.Log(outPutValue);
        _baseBike.UpdateAxelValue(outPutValue);
    }

}
