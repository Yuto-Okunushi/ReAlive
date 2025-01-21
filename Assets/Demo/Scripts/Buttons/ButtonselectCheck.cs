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
        SceneController.LoadNextScene("StartScene");
    }

    public void EndButtonCheck()
    {
        Debug.Log("�G���h�{�^�����I������܂���");
        Application.Quit();
    }

    public void GoNextScen2()       // TimeLine�ŃV�[���J�ڂ������邽�߂̃��\�b�h
    {
        SceneController.LoadNextScene("stage_3");
    }
    public void GoNextScene3()       // TimeLine�ŃV�[���J�ڂ������邽�߂̃��\�b�h
    {
        SceneController.LoadNextScene("stage_4");
    }
    public void GoNextScene4()       // TimeLine�ŃV�[���J�ڂ������邽�߂̃��\�b�h
    {
        SceneController.LoadNextScene("stage_5");
    }

    public void GoGameOverScene()
    {
        //�v���C���[�������ʒu�ɖ߂����\�b�h
        playerScripts.ResetPlayerPostion();
        // �v���C���[�̃X�e�[�^�X�������ɖ߂��āA�ŏ��̒ʒm���΂��V�X�e��
        playerStatus.ResetPlayerStates();
    }
}
