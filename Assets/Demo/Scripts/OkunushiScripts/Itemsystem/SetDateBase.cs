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

    

    public void SendDate()
    {
        if (itemImage != null)
        {
            GameManager.SetItemImage(itemImage);  // �擾����Image�R���|�[�l���g�̃X�v���C�g��ݒ�
        }
        GameManager.SetItemName(itemDescription);
        GameManager.SetItemDetailsName(itemDetails);
    }
}
