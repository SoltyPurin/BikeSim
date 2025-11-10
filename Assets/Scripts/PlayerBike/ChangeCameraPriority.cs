using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCameraPriority : MonoBehaviour
{
    [SerializeField, Header("優先度を変えるカメラ")]
    private CinemachineVirtualCamera _camera = default;
    [SerializeField, Header("視点変更のアクションの名前")]
    private string _changePersonName = "ChangePerson";

    private PlayerInput _input = default;
    private InputAction _changeView;
    private ChangeModelView _model = default;

    private int _highPriority = 10;
    private int _lowPriority = 0;
    private bool _isFirstPerson = true;


    private void Start()
    {
        if(_input == null)
        {
            _input = GetComponent<PlayerInput>();
        }
        _changeView = _input.actions.FindAction(_changePersonName);
        _model = GetComponent<ChangeModelView>();
        _model.ShadowOnly();
    }

    private void Update()
    {
        if (_changeView.WasPressedThisFrame())
        {
            ChangeCamera();
        }

    }
    private void ChangeCamera()
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
