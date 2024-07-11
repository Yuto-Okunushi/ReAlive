using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawns : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject Openpanel;
    [SerializeField] Text ItemText;
    [SerializeField] ItemData[] itemData;

    private int TotalItemcounts = 1;
    private int selectedItemIndex;

    private void Update()
    {
        // 更新される所持金の表示
    }

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
        int currentMoney = GameManager.GetPlayerMony();
        if (TotalItemcounts <= 5 && currentMoney >= itemData[selectedItemIndex].price)
        {
            GameObject newItem = Instantiate(items[selectedItemIndex], inventory.transform);
            TotalItemcounts++;
            GameManager.SetTotalItem(TotalItemcounts);
            currentMoney -= itemData[selectedItemIndex].price;
            GameManager.SetPlayerMony(currentMoney);
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
