using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    [SerializeField, Header("カメラが注視するオブジェクト")]
    private GameObject _lookAtObj = default;
   
    private Vector3 _lookPos = Vector3.zero;
    private Vector3 _firstPos = Vector3.zero;

    private readonly string HORIZONTAL = "RightHorizontal";
    private readonly string VERTICAL = "RightVertical";

    private void Start()
    {
        _firstPos = transform.localPosition;
    }

    private void MoveCameraLookObj(float hori,float ver)
    {
        //Debug.Log("右スティックの入力は" + hori + ver);
        Vector3 cameraPos = _lookAtObj.transform.localPosition;
        cameraPos.x = hori;
        cameraPos.y = ver;
        _lookAtObj.transform.localPosition = cameraPos;
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed)
        {
            MoveCameraLookObj(0, 0.35f);
            return;
        }

        // スティックの2軸入力取得
        Vector2 inputValue = context.ReadValue<Vector2>();
        
        MoveCameraLookObj(inputValue.x, inputValue.y);
    }

}
