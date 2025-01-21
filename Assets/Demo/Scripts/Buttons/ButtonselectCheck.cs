using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonselectCheck : MonoBehaviour
{
    // Player�X�N���v�g���Q��
    [SerializeField] Player playerScripts;
    // PlayerStatus�X�N���v�g�Q��
    [SerializeField] PlayerStatus playerStatus;
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
        // �v���C���[�̃X�e�[�^�X�������ɖ߂��āA�ŏ��̒ʒm���΂��V�X�e��
        playerStatus.ResetPlayerStates();
    }
}
