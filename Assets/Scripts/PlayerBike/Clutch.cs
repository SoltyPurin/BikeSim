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
            //AIによる動作中はなにもしない
            return;
        }
        _leftTrigger = 1.0f - _gamePad.leftTrigger.ReadValue();
        _baseBike.UpdateClutchValue(LeftTrigger);

    }

    /// <summary>
    /// クラッチの値をセットする
    /// </summary>
    /// <param name="value">左トリガーの押し込み具合</param>
    public void SetClutchValue(float value)
    {
        _leftTrigger = value;
    }
}
