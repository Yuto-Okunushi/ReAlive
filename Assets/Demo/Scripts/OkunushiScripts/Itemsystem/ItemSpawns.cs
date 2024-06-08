using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{
    [SerializeField] GameObject Item1;
    [SerializeField] GameObject Item2;
    [SerializeField] GameObject Item3;
    [SerializeField] GameObject Item4;
    [SerializeField] GameObject Item5;
    [SerializeField] GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn1()
    {
        // Item1をinventoryの子オブジェクトとして生成
        GameObject newItem1 = Instantiate(Item1, inventory.transform);
    }

    public void Spwan2()
    {
        GameObject newItem2 = Instantiate(Item2, inventory.transform);
    }

    public void Spwan3()
    {
        GameObject newItem3 = Instantiate(Item3, inventory.transform);
    }

    public void Spwan4()
    {
        GameObject newItem4 = Instantiate(Item4, inventory.transform);
    }

    public void Spwan5()
    {
        GameObject newItem5 = Instantiate(Item5, inventory.transform);
    }

}
