using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonselectCheck : MonoBehaviour
{
    // Player�X�N���v�g���Q��
    [SerializeField] Player playerScripts;
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
        //�v���C���[�������ʒu�ɖ߂����\�b�h
        playerScripts.ResetPlayerPostion();
        // Scene���ă��[�h
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneController.LoadNextScene("GameOverScene");
    }
}
