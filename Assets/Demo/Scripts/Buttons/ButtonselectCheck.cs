using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonselectCheck : MonoBehaviour
{

    public void StartButtonCheck()
    {
        Debug.Log("�X�^�[�g�{�^�����I������܂���");
        SceneController.LoadNextScene("Demo_Scenes");
    }

    public void EndButtonCheck()
    {
        Debug.Log("�G���h�{�^�����I������܂���");
        Application.Quit();
    }
}
