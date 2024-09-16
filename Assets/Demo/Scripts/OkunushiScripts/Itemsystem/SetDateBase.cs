using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDateBase : MonoBehaviour
{
    [SerializeField] private ItemData itemDatabase;

    public string itemDescription;
    public string itemDetails;
    public Sprite itemImage;  // Image�R���|�[�l���g�̕ϐ���錾

    public float playerHydration;       //�����f�[�^���Q�[���}�l�[�W���[�Ɏ󂯓n��
    public float playerStress;       //�X�g���X�f�[�^���Q�[���}�l�[�W���[�Ɏ󂯓n��

    public GameObject YesNopanel;       //�g�����ŏI�I�Ȕ��f��������p�l���\���A�A�^�b�`�o���Ȃ����

    



    public void SendDate()
    {
        if (itemImage != null)
        {
            GameManager.SetItemImage(itemImage);  // �擾����Image�R���|�[�l���g�̃X�v���C�g��ݒ�
        }
        GameManager.SetItemName(itemDescription);
        GameManager.SetItemDetailsName(itemDetails);
    }

    public void SendHydration()
    {
        Debug.Log("�{�^����������܂���");
        GameManager.SetPlayerHydration(playerHydration);        //�A�C�e���g�p�ɂ�鐅���f�[�^�̎󂯓n��
        GameManager.SetPlayerStress(playerStress);              //�A�C�e���g�p�ɂ��X�g���X�f�[�^�̎󂯓n��
    }

    

    
}
