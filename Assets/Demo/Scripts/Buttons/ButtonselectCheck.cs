using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonselectCheck : MonoBehaviour
{

    public void StartButtonCheck()
    {
        Debug.Log("�X�^�[�g�{�^�����I������܂���");
        SceneController.LoadNextScene("��_DemoScene");
    }

    public void RestartButtonCheck()
    {
        Debug.Log("���X�^�[�g�{�^����������܂���");
        SceneController.LoadNextScene("��_DemoScene");
    }

    public void TitleButtonCheck()
    {
        Debug.Log("�^�C�g���{�^����������܂���");
        SceneController.LoadNextScene("Demo_Title");
    }

    public void EndButtonCheck()
    {
        Debug.Log("�G���h�{�^�����I������܂���");
        Application.Quit();
    }

    public void GoGameOverScene()
    {
        SceneController.LoadNextScene("GameOverScene");
    }
}
