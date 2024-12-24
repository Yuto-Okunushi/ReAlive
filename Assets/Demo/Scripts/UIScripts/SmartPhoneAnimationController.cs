using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneAnimationController : MonoBehaviour
{

    Animator animator;      //�A�j���[�V����
    int OpenSmartPhone = 0;     //�{�^���������ꂽ�񐔂𒲂ׂ�

    //��b�����ǂ���
    bool isTalking = false;


    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�̃R���|�[�l���g���擾����
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTalking = GameManager.GetIsTalking();

        if(!isTalking)
        {
            if (OpenSmartPhone == 0)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SmartPhoneOpneAnimation();
                }
            }
            else if (OpenSmartPhone == 1)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SmartPhoneCloseAnimation();
                }
            }
        }
        
    }

    //�X�}�z�p�l����\������A�j���[�V����
    public void SmartPhoneOpneAnimation()
    {
        //�A�j���[�^�[��bool��ύX����
        animator.SetBool("IsOpend", true);
        OpenSmartPhone++;
    }

    public void SmartPhoneCloseAnimation()
    {
        //�A�j���[�^�[��bool��ύX����
        animator.SetBool("IsOpend", false);
        OpenSmartPhone--;
    }
}
