using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{
    //--�������Ő�������A�C�e��-------------------------
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject inventory;
    //---------------------------------------------------

    //--��������Ȃ���\��������p�l��-------------------
    [SerializeField] GameObject Openpanel;
    //---------------------------------------------------

    //text
    [SerializeField] Text ItemText;

    //==�A�C�e���f�[�^==============================================================
    [SerializeField] ItemData[] itemData;
    //==============================================================================

    private int TotalItemcounts = 1;
    private int selectedItemIndex;

    // �A�C�e����I�����ăp�l�����J�����\�b�h
    public void PaneOpenSpwam(int index)
    {
        selectedItemIndex = index;
        Openpanel.SetActive(true);
        ItemText.text = $"{itemData[index].itemName} �l�i${itemData[index].price} �𔃂��܂����H";
    }

    // �p�l������郁�\�b�h
    public void ClosePanel()
    {
        Openpanel.SetActive(false);
    }

    // �͂��{�^�����������Ƃ��ɃA�C�e�����X�|�[�������郁�\�b�h
    public void SpawnSelectedItem()
    {
        if (TotalItemcounts <= 5)
        {
            GameObject newItem = Instantiate(items[selectedItemIndex], inventory.transform);
            TotalItemcounts++;
            GameManager.SetTotalItem(TotalItemcounts);
        }
        ClosePanel();
    }

    // �A�C�e���̃p�l�����J�����߂̃��\�b�h
    public void OnClickItem1()
    {
        PaneOpenSpwam(0);
    }

    public void OnClickItem2()
    {
        PaneOpenSpwam(1);
    }

    public void OnClickItem3()
    {
        PaneOpenSpwam(2);
    }

    public void OnClickItem4()
    {
        PaneOpenSpwam(3);
    }

    public void OnClickItem5()
    {
        PaneOpenSpwam(4);
    }
}
