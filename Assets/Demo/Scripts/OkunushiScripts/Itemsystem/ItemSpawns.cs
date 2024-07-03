using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{

    //--�������Ő�������A�C�e��-------------------------
    [SerializeField] GameObject Item1;
    [SerializeField] GameObject Item2;
    [SerializeField] GameObject Item3;
    [SerializeField] GameObject Item4;
    [SerializeField] GameObject Item5;
    [SerializeField] GameObject inventory;
    //---------------------------------------------------

    //--��������Ȃ���\��������p�l��-------------------
    [SerializeField] GameObject Openpanel;
    //---------------------------------------------------

    //--�C���x���g���ŃA�C�e���X���b�g�ɕ���\��������A�C�e���Ɛe�I�u�W�F�N�g------
    //[SerializeField] GameObject DisplayItem1;
    //[SerializeField] GameObject Itemslot;
    //---------------------------------------------------------------

    private int TotalItemcouts = 1;

    // Update is called once per frame
    
    public void PaneOpen()
    {
        Openpanel.SetActive(true);
    }

    public void ClosePanel()
    {
        Openpanel.SetActive(false);
    }

    public void Spawn1()
    {
        if(TotalItemcouts <= 5)
        {
            GameObject newItem1 = Instantiate(Item1, inventory.transform);
            TotalItemcouts++;
            GameManager.SetTotalItem(TotalItemcouts);
        }
        
    }

    public void Spwan2()
    {
        if (TotalItemcouts <= 5)
        {
            GameObject newItem2 = Instantiate(Item2, inventory.transform);
            TotalItemcouts++;
            GameManager.SetTotalItem(TotalItemcouts);
        }
        

    }

    public void Spwan3()
    {
        if (TotalItemcouts <= 5)
        {
            GameObject newItem3 = Instantiate(Item3, inventory.transform);
            TotalItemcouts++;
            GameManager.SetTotalItem(TotalItemcouts);
        }
        

    }

    public void Spwan4()
    {
        if (TotalItemcouts <= 5)
        {
            GameObject newItem4 = Instantiate(Item4, inventory.transform);
            TotalItemcouts++;
            GameManager.SetTotalItem(TotalItemcouts);

        }

    }

    public void Spwan5()
    {
        if (TotalItemcouts <= 5)
        {
            GameObject newItem5 = Instantiate(Item5, inventory.transform);
            TotalItemcouts++;
            GameManager.SetTotalItem(TotalItemcouts);
        }
        

    }

}
