using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject canvas;
   

    // OnPointerEnter���\�b�h���C�x���g�g���K�[����Ăяo����悤��public�ɂ���
    public void OnPointerEnter()
    {
        GameObject newimage = Instantiate(image, canvas.transform);
    }
}
