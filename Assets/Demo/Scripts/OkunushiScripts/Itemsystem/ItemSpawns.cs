using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{

    //--買い物で生成するアイテム-------------------------
    [SerializeField] GameObject Item1;
    [SerializeField] GameObject Item2;
    [SerializeField] GameObject Item3;
    [SerializeField] GameObject Item4;
    [SerializeField] GameObject Item5;
    [SerializeField] GameObject inventory;
    //---------------------------------------------------

    //--インベントリでアイテムスロットに物を表示させるアイテムと親オブジェクト------
    [SerializeField] GameObject DisplayItem1;
    //[SerializeField] GameObject Itemslot;
    //---------------------------------------------------------------

    private int TotalItemcouts = 0;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Displayitem();
    }

    public void Spawn1()
    {
        // Item1をinventoryの子オブジェクトとして生成
        GameObject newItem1 = Instantiate(Item1, inventory.transform);
        TotalItemcouts++;
    }

    public void Spwan2()
    {
        GameObject newItem2 = Instantiate(Item2, inventory.transform);
        TotalItemcouts++;

    }

    public void Spwan3()
    {
        GameObject newItem3 = Instantiate(Item3, inventory.transform);
        TotalItemcouts++;

    }

    public void Spwan4()
    {
        GameObject newItem4 = Instantiate(Item4, inventory.transform);
        TotalItemcouts++;

    }

    public void Spwan5()
    {
        GameObject newItem5 = Instantiate(Item5, inventory.transform);
        TotalItemcouts++;
        Debug.Log(TotalItemcouts);

    }

    public void Displayitem()
    {
        GameManager.SetTotalItem(TotalItemcouts);
    }

}
