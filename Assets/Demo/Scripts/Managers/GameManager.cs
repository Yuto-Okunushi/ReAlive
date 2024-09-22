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



    static public string GetNowScene()
    {
        return instance.nowScene;
    }

    static public bool GetIsSceneLoading()
    {
        return instance.isSceneLoading;
    }

    static public bool GetIsGame()
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

    static public int GetItemTotal()
    {
        return instance.Totalitem;
    }

    static public string getItemName()
    {
        return instance.ItemName;
    }

    static public string getItemDetailsName()
    {
        return instance.ItemDetailsName;
    }

    static public Sprite getItemImage()
    {
        return instance.ItemImage;
    }

    static public int GetPlayerMony()
    {
        return instance.Playerhavemony;
    }

    static public ItemData GetItemDate(ItemData value)
    {
        return instance.ShopItemDate = value;
    }

    static public SignDate GetSignDate()
    {
        return instance.Signdate;
    }

    static public float GetPlayerHp()
    {
        return instance.PlayerHp;
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






    //===SETTER==========================================================================================================
    static public float SetVolumeBGM(float value)
    {
        if (value > 0)
        {
            return instance.volumeBGM = value;
        }
        else
        {
            return instance.volumeBGM = 0;
        }
    }

    static public float SetVolumeSE(float value)
    {
        if (value > 0)
        {
            return instance.volumeSE = value;
        }
        else
        {
            return instance.volumeSE = 0;
        }
    }

    static public float SetVolumeVOICE(float value)
    {
        if (value > 0)
        {
            return instance.volumeVOICE = value;
        }
        else
        {
            return instance.volumeVOICE = 0;
        }
    }

    static public void SetDurationCrossBGM(float value)
    {
        instance.durationCrossBGM = value;
    }



    static public void SetNowScene(string name)
    {
        instance.nowScene = name;
    }

    static public void SetIsSceneLoading(bool tf)
    {
        instance.isSceneLoading = tf;
    }

    static public void SetIsGame(bool tf)
    {
        instance.isGame = tf;
    }

    static public void SetBoostingReset1(bool tf)        ////�R���{�ɂ��^�C��������bool�ϐ��R�R���{�̏ꍇ�̊֐�
    {
        instance.isBoosting1 = tf;
    }

    static public void SetBoostingReset2(bool tf)        ////�R���{�ɂ��^�C��������bool�ϐ�5�R���{�̏ꍇ�̊֐�
    {
        instance.isBoosting2 = tf;
    }

    static public void SetTotalItem(int value)
    {
        instance.Totalitem = value;
    }

    static public void SetItemName(string value)
    {
        instance.ItemName = value;
    }

    static public void SetItemDetailsName(string value)
    {
        instance.ItemDetailsName = value;
    }

    static public void SetItemImage(Sprite value)
    {
        instance.ItemImage = value;
    }



    static public void SetPlayerMony(int value)
    {
        instance.Playerhavemony = value;
    }

    static public void SetItemDate(ItemData itemData)
    {
        instance.ShopItemDate = itemData;
    }

    static public void SetSignDate(SignDate signDate)
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

    static public void SendPlusStress(float value)
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

    static public void SendPulusHydration(float value)
    {
        instance.PlayerHydration += value;
        PlayerStatus.Instance.currHyd = instance.PlayerHydration; // �l��PlayerStatus�ɔ��f

        if(PlayerStatus.Instance.currHyd > 100)
        {
            instance.PlayerHydration = 100;
            
        }
    }
}
