using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;      //�ÓI�C���X�^���X��

    public delegate void Delegete(string nextSceneName);    //���\�b�h�������Ɏ������邽�߂̕ϐ����f���Q�[�g�^
    [SerializeField] private GameObject sceneCanvas;        //�V�[���R���g���[���̎q�I�u�W�F�N�g�A�V�[���L�����o�X�̃Q�[���I�u�W�F�N�g�^�Ƃ��Ă̎擾
    private List<Animator> scAnimator = new List<Animator>();       //�V�[���R���g���[���̃A�j���[�^�[�����X�g�Ƃ��Ď擾

    private float time = 0;     //�V�[�����[�h���̎���

    private void Awake()            //start���O�ɃV�[���R���g���[���v���t�@�u���V���O���g����
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



    //====================================================================================//

    private void Start()
    {
        GameManager.SetNowScene(SceneManager.GetActiveScene().name);        //���݂̃V�[�������Q�[���}�l�[�W���ɃZ�b�g
    }

    private void Update()
    {
        if (GameManager.GetIsSceneLoading())        //�V�[�����[�h���Ȃ�time�𑝉�
        {
            time += Time.deltaTime;
        }
        else if (!GameManager.GetIsSceneLoading())      //�V�[�����[�h�I��莟��time��������
        {
            time = 0;
        }
    }

    //====================================================================================//

    static public void LoadNextScene(string nextSceneName)      //���̃V�[�����Ăяo�����\�b�h
    {
        GameManager.SetNowScene(nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    //======================================================================================//

    static public void SceneTransitionAnimation(GameObject parent)      //�V�[���g�����W�V�����A�j���[�V�����@�������I�u�W�F�N�g�̐e�I�u�W�F�N�g��������
    {

        if (instance.scAnimator != null && instance.scAnimator.Count > 0)       //�z��O�ւ̃A�N�Z�X�𐧌�����G���[�������
        {
            instance.scAnimator.Clear();
        }
        Animator[] localAnimator;
        localAnimator = parent.GetComponentsInChildren<Animator>();     //�e�I�u�W�F�N�g�̎q�I�u�W�F�N�g�̃A�j���[�^�[�R���|�[�l���g�����ׂĎ擾
        instance.scAnimator.AddRange(localAnimator);                    //�����o�ϐ��ɍĒ�`
        for (int i = 0; i < localAnimator.Length; i++)                  //�擾�������̃I�u�W�F�N�g�R���|�[�l���g�񐔕��J��Ԃ�
        {
            instance.scAnimator[i].SetTrigger("Transition");
        }
    }

    static public void SceneTransitionAnimation(GameObject parent, string transitionName)       //��Ɠ������\�b�h�I�[�o�[���[�h���Ă܂��@�������ɃA�j���[�V�����̃g���K�[
    {

        if (instance.scAnimator != null && instance.scAnimator.Count > 0)
        {
            instance.scAnimator.Clear();
        }
        Animator[] localAnimator;
        localAnimator = parent.GetComponentsInChildren<Animator>();
        instance.scAnimator.AddRange(localAnimator);
        for (int i = 0; i < localAnimator.Length; i++)
        {
            instance.scAnimator[i].SetTrigger(transitionName);
        }
    }
}