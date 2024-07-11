using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{
    //--買い物で生成するアイテム-------------------------
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject inventory;
    //---------------------------------------------------

    //--買う買わないを表示させるパネル-------------------
    [SerializeField] GameObject Openpanel;
    //---------------------------------------------------

    //text
    [SerializeField] Text ItemText;

    //==アイテムデータ==============================================================
    [SerializeField] ItemData[] itemData;
    //==============================================================================

    private int TotalItemcounts = 1;
    private int selectedItemIndex;

    // アイテムを選択してパネルを開くメソッド
    public void PaneOpenSpwam(int index)
    {
        selectedItemIndex = index;
        Openpanel.SetActive(true);
        ItemText.text = $"{itemData[index].itemName} 値段${itemData[index].price} を買いますか？";
    }

    // パネルを閉じるメソッド
    public void ClosePanel()
    {
        Openpanel.SetActive(false);
    }

    // はいボタンを押したときにアイテムをスポーンさせるメソッド
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

    // アイテムのパネルを開くためのメソッド
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
