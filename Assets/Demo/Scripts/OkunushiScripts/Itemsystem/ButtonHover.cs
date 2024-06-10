using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject canvas;
   

    // OnPointerEnterメソッドをイベントトリガーから呼び出せるようにpublicにする
    public void OnPointerEnter()
    {
        GameObject newimage = Instantiate(image, canvas.transform);
    }
}
