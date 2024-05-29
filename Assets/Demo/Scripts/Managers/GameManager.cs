using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�C���X�^���X�쐬
    public static GameManager instance = null;

    //�Q�[���}�l�[�W���������o�ϐ�

    //===���֌W====================================================================
    public float volumeBGM; //BGM�̉���
    public float volumeSE;  //SoundEffect�̉���
    public float volumeVOICE; //Voice�̉���
    public float durationCrossBGM;  //BGM���N���X�t�F�[�h������Ƃ��̊Ԃ̎���
    //=============================================================================

    //==�^�C���A�^�b�N�֌W=========================================================
    public int[] taTotalScore = new int[4];     //Perfect Great Good�̋󔠍쐬
    public float taSumScore;                      //�X�R�A�̍��v�l�̕ϐ�
    //=============================================================================


    //=�g���[�j���O�V�X�e���֌W====================================================
    public float trSecondSumTime;   //�v���C����(�b)
    public int trMinuteSumTime;   //�v���C����(��)
    public int trHourSumTIme;   //�v���C����(����)
    public float trAverage;         //���ϒl
    public int trCount;             //�񓚐�
    public int trPlayCount;         //�v���C��

    

    //=============================================================================

    //=�^�C���A�^�b�N�g���[�j���O����==============================================
    public int sumCombo;
    //=============================================================================

    public string nowScene;     //���݂ǂ̃V�[���ɂ��邩

    public bool isSceneLoading = false;   //���݃V�[���J�ڂ��Ă邩�B

    

    public bool isGame = false; //���݉��炩�̃��[�h�̃Q�[�������ǂ���

    public bool isBoosting1;        //�R���{�̑ҋ@�����Ă��邩�ǂ���3�R���{�̎�
    public bool isBoosting2;        //�R���{�̑ҋ@�����Ă��邩�ǂ����T�R���{�̎�

    [HideInInspector]
    public string[] KatakanaArray =
    {
        "�A", "�C", "�E", "�G", "�I",
        "�J", "�L", "�N", "�P", "�R",
        "�T", "�V", "�X", "�Z", "�\",
        "�^", "�`", "�c", "�e", "�g",
        "�i", "�j", "�k", "�l", "�m",
        "�n", "�q", "�t", "�w", "�z",
        "�}", "�~", "��", "��", "��",
        "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��",
        "��", "�[",
        "�K", "�M", "�O", "�Q", "�S",
        "�U", "�W", "�Y", "�[", "�]",
        "�_", "�a", "�d", "�f", "�h",
        "�o", "�r", "�u", "�x", "�{",
        "��",
        "�p", "�s", "�v", "�y", "�|",
        "�@", "�B", "�D", "�F", "�H",
        "��", "��", "��", "��", "�b"
    };  //�J�^�J�i�z��

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
        SceneController.SceneTransitionAnimation(parent,transitionName);
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

    static public int GetTaTotalScore(int n)
    {
        //error���
        if (n >= 0 && n < instance.taTotalScore.Length)
        {
            return instance.taTotalScore[n];
        }

        //�\�z�����z���length���傫�������Q�Ƃ��悤�Ƃ�����
        else
        {
            return -1;
            //�̂��́[�P�ȉ��̏���������
        }
    }

    static public float GetTaSumScore()
    {
        return instance.taSumScore;
    }

    static public float GetTrSecendSumTime()
    {
        return instance.trSecondSumTime;
    }

    static public int GetTrMinuteSumTime()
    {
        return instance.trMinuteSumTime;
    }

    static public int GetTrHourSumTime()
    {
        return instance.trHourSumTIme;
    }

    static public float GetTrAnswerAverage()
    {
        return instance.trAverage;
    }

    static public int GetTrAnswerCount()
    {
        return instance.trCount;
    }

    static public int GetTrPlayCount()
    {
        return instance.trPlayCount;
    }

    static public int GetTaTlCombo()
    {
        return instance.sumCombo;
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


    static public string getKatakanaArray(int n)
    {
        //error���
        if (n >= 0 && n < instance.KatakanaArray.Length)
        {
            return instance.KatakanaArray[n];
        }

        //�\�z�����z���length���傫�������Q�Ƃ��悤�Ƃ�����
        else
        {
            return null;
        }
    }

    static public int getKatakanaArrayLength()
    {
        return instance.KatakanaArray.Length;
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

    static public void SetTaTotalScore(int n, int value)
    {
        if(n >= 0 && n < instance.taTotalScore.Length)
        {
            instance.taTotalScore[n] = value;       //n�Ԗڂ̔���value�������Ă���
        }
        else
        {
            return;             //��O����
        }
    }

    static public void SetTaTlsumCombo(int value)
    {
        instance.sumCombo = value;
    }

    static public void SetTaSumScore(float value)
    {
        instance.taSumScore = value;
    }

    static public void SetTrSecondSumTime(float value)
    {
        instance.trSecondSumTime = value;
    }

    static public void SetTrMinuteSumTime(int value)
    {
        instance.trMinuteSumTime = value;
    }

    static public void SetTrHourSumTIme(int value)
    {
        instance.trHourSumTIme = value;
    }
    
    static public void SetAnswerAverage(float value)
    {
        instance.trAverage = value;
    }

    static public void SetAnswerCount(int value)
    {
        instance.trCount = value;
    }

    static public void SetPlayCount(int value)
    {
        instance.trPlayCount = value;
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

    

}
