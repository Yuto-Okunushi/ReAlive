using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    //�C���X�^���X�쐬
    public static GameManager instance = null;

    public float test;
    public float PlusTest;

    //�Q�[���}�l�[�W���������o�ϐ�

    //===���֌W====================================================================
    public float volumeBGM; //BGM�̉���
    public float volumeSE;  //SoundEffect�̉���
    public float volumeVOICE; //Voice�̉���
    public float durationCrossBGM;  //BGM���N���X�t�F�[�h������Ƃ��̊Ԃ̎���
  �@//=============================================================================
    
    public string nowScene;     //���݂ǂ̃V�[���ɂ��邩

    public bool isSceneLoading = false;   //���݃V�[���J�ڂ��Ă邩�B



    public bool isGame = false; //���݉��炩�̃��[�h�̃Q�[�������ǂ���

    public bool isBoosting1;        //�R���{�̑ҋ@�����Ă��邩�ǂ���3�R���{�̎�
    public bool isBoosting2;        //�R���{�̑ҋ@�����Ă��邩�ǂ����T�R���{�̎�

    public int Totalitem;

    public string ItemName;         //�A�C�e���̖��O��\��������
    public string ItemDetailsName;      //�A�C�e���̏ڍׂ�\��������
    public Sprite ItemImage;
    public ItemData ShopItemDate;        //�V���b�v�A�C�e���̃f�[�^�i�[
    public SignDate Signdate;           //�W���f�[�^�̊i�[
    public int Playerhavemony;          //�v���C���[�̏�����
    public float PlayerHp;              //�v���C���[��HP
    public float PlayerStress = 0;      //�v���C���[�̃X�g���X�l
    public float PlayerHydration = 0;       //�v���C���[�̐�����

    public float maxHydration = 100f; // �ő吅����
    public float maxStress = 100f;    // �ő�X�g���X��

    public bool isTimeline = false;     //�^�C�����C�������̃t���O

    public string TalkDate;         //CSV�̃g�[�N�f�[�^�󂯓n���ϐ�
    public string NameDate;         //CSV�̖��O�g�[�N�f�[�^�󂯓n���ϐ�

    public bool isTalking = false;      //��b�C�x���g�����̃t���O

    public bool isOpend = false;        //�C���x���g�����J����Ă��邩�̃t���O

    public bool isMapShow = true;       // �}�b�v���\���\���̃t���O

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //===METHOD==========================================================================================================

    static public IEnumerator DelaySceneTransition(float delayButtonAnim, float delaySceneTrans, AudioManager.SEType se, GameObject parent, string nextSceneName)
    {
        AudioManager.PlaySE(se);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(delayButtonAnim);
        SceneController.SceneTransitionAnimation(parent);
        yield return new WaitForSeconds(delaySceneTrans);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneController.LoadNextScene(nextSceneName);
    }

    static public IEnumerator DelaySceneTransition(float delayButtonAnim, float delaySceneTrans, AudioManager.SEType se, GameObject parent, string nextSceneName, string transitionName)
    {
        AudioManager.PlaySE(se);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(delayButtonAnim);
        SceneController.SceneTransitionAnimation(parent, transitionName);
        yield return new WaitForSeconds(delaySceneTrans);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneController.LoadNextScene(nextSceneName);
    }

    static public void SelectButton()
    {
        AudioManager.PlaySE(AudioManager.SEType.Cursor);
    }


    //===GETTER==========================================================================================================
    static public bool GetIsTalking()       //��b�C�x���g��
    {
        return instance.isTalking;
    }

    //===�g�p���Ă��Ȃ����\�b�h�A�G���[����ׂ̈ɏ����Ă��܂���=============================================================
    static public float GetVolumeBGM()      
    {
        return instance.volumeBGM;
    }

    static public float GetVolumeSE()
    {
        return instance.volumeSE;
    }

    static public float GetVolumeVOICE()
    {
        return instance.volumeVOICE;
    }

    static public float GetDurationCrossBGM()
    {
        return instance.durationCrossBGM;
    }
    //=�����܂ŃX���[���Ă�������==================================================================================================================


    static public string GetNowScene()      //���݂̃V�[��
    {
        return instance.nowScene;
    }

    static public bool GetIsSceneLoading()      //�V�[���̃��[�h����
    {
        return instance.isSceneLoading;
    }

    static public bool GetIsMapShow()      //�}�b�v���J���邩
    {
        return instance.isMapShow;
    }

    static public bool GetIsGame()          //���쒆���m�F
    {
        return instance.isGame;
    }

    static public bool GetBoostingReset1()      //�R���{�ɂ��^�C��������bool�ϐ��R�R���{�̏ꍇ�̊֐�
    {
        return instance.isBoosting1;
    }

    static public bool GetBoostingReset2()      //�R���{�ɂ��^�C��������bool�ϐ�5�R���{�̏ꍇ�̊֐�
    {
        return instance.isBoosting2;
    }

    static public int GetItemTotal()        //�A�C�e���������̎擾
    {
        return instance.Totalitem;
    }

    static public string getItemName()      //�A�C�e���̖��O���擾
    {
        return instance.ItemName;
    }

    static public string getItemDetailsName()       //�A�C�e���ڍ׏��̎擾
    {
        return instance.ItemDetailsName;
    }

    static public Sprite getItemImage()         //�A�C�e���摜�擾
    {
        return instance.ItemImage;
    }

    static public int GetPlayerMony()           //�v���C���[�̏������擾
    {
        return instance.Playerhavemony;
    }

    static public SignDate GetSignDate()        //�W���f�[�^�̎擾
    {
        return instance.Signdate;
    }

    static public float GetPlayerStress(float value)        //�X�g���X�󂯓n��
    {
        instance.PlayerStress = value;
        return instance.PlayerStress + instance.test;
    }

    static public float GetPlayerHydration(float value)     //�����󂯓n��
    {
        instance.PlayerHydration = value; // �n���ꂽ�l�� PlayerHydration �ɑ��
        return instance.PlayerHydration + instance.test;    // ���v�l��Ԃ�

    }

    static public bool GetTimelineflug()            //�^�C�����C�������擾
    {
        return instance.isTimeline;
    }

    static public string GetCSVTalk()               //CSV��b�f�[�^�擾
    {
        return instance.TalkDate;
    }

    static public string GetCSVCharactorName()      //CSV�f�[�^����L�����N�^�[���̎擾
    {
        return instance.NameDate;
    }

    static public bool GetIsOpend()                 //�C���x���g���������Ă��邩�̎擾
    {
        return instance.isOpend;
    }
    //===SETTER==========================================================================================================
    static public bool SetIsOpend(bool value)       // �C���x���g�����J����Ă��邩�ǂ���
    {
        return instance.isOpend = value;    
    }

    static public bool SetIsMapShow(bool value)       // �}�b�v���J����Ă��邩�ǂ���
    {
        return instance.isMapShow = value;
    }

    static public bool SetIsTalking(bool value)         //��b�C�x���g�����̔���
    {
        return instance.isTalking = value;
    }


    static public void SetNowScene(string name)         //���݂̃V�[��
    {
        instance.nowScene = name;
    }

    static public void SetIsTimeline(bool tf)           //�^�C�����C���Đ������m�F
    {
        instance.isTimeline = tf;
    }

    static public void SetTotalItem(int value)          //�A�C�e��������
    {
        instance.Totalitem = value;
    }

    static public void SetItemName(string value)        //�A�C�e����
    {
        instance.ItemName = value;
    }

    static public void SetItemDetailsName(string value)         //�A�C�f���ڍ׃f�[�^
    {
        instance.ItemDetailsName = value;
    }

    static public void SetItemImage(Sprite value)               //�A�C�e���C���[�W
    {
        instance.ItemImage = value;
    }



    static public void SetPlayerMony(int value)                 //�v���C���[�̏�����
    {
        instance.Playerhavemony = value;
    }

    static public void SetSignDate(SignDate signDate)           //�W���f�[�^
    {
        instance.Signdate = signDate;
    }

    static public void SetPlayerHp(float value)     //�v���C���[��HP�󂯓n��
    {
        instance.PlayerHp = value;
    }

    static public void SetPlayerStress(float value)     //�v���C���[�̃X�g���X�󂯓n��
    {
        instance.PlayerStress = value;
    }

    static public void SendPlusStress(float value)          //�X�g���X�l
    {
        instance.PlayerStress += value;
        PlayerStatus.Instance.currStress = instance.PlayerStress;

        if (PlayerStatus.Instance.currStress > 100)
        {
            instance.PlayerStress = 100;

        }
    }

    static public void SetPlayerHydration(float value)     //�v���C���[�̐����󂯓n��
    {
        instance.PlayerHydration = value;
    }

    static public void SendPulusHydration(float value)      //�v���C���[�����ʂ𑫂�
    {
        instance.PlayerHydration += value;
        PlayerStatus.Instance.currHyd = instance.PlayerHydration; // �l��PlayerStatus�ɔ��f

        if(PlayerStatus.Instance.currHyd > 100)
        {
            instance.PlayerHydration = 100;
            
        }
    }

    static public void SendCSVTalk(string talknumber)           //CSV�����b���e�𑗂�
    {
        instance.TalkDate = talknumber;
    }

    static public void SendCSVCharactorName(string NameNumber)      //CSV�̃L�����N�^�[�̖��O�𑗂�
    {
        instance.NameDate = NameNumber;
    }
}
