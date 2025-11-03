using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCurveSprite : MonoBehaviour
{
    [SerializeField, Header("イメージ差し替え用のイメージ")]
    private Image _image = default;
    public void ShowingSprite(Image image)
    {
        _image.enabled = true;
        _image = image;
    }
}
