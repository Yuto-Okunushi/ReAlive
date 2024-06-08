using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    private bool buttonSelected = false;
    public Button startButton; // �����I������{�^����Inspector�Őݒ�

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n���Ɉ�x�����{�^����I������
        SelectButton(startButton);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        // �{�^���I���͂����ōs��Ȃ�
    }

    // �{�^����I�����郁�\�b�h
    void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
