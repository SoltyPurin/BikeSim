using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCameraPriority : MonoBehaviour
{
    [SerializeField, Header("優先度を変えるカメラ")]
    private CinemachineVirtualCamera _camera = default;
    [SerializeField,Header("インプットシステム")]
    private InputAction _action;

    private ChangeModelView _model = default;

    private int _highPriority = 10;
    private int _lowPriority = 0;
    private bool _isFirstPerson = true;

    private void Start()
    {
        _action.performed += ChangeCamera;
        _action?.Enable();
        _model = GetComponent<ChangeModelView>();
        _model.ShadowOnly();
    }

    public void ChangeCamera(InputAction.CallbackContext context)
    {
        Debug.Log("視点変更");
        switch (_isFirstPerson)
        {
            case true:
                _camera.Priority = _lowPriority;
                _isFirstPerson = false;
                _model.ShowModel();
                break;

            case false:
                _camera.Priority = _highPriority;
                _isFirstPerson = true;
                _model.ShadowOnly();
                break;
        }
    }
}
