using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemArticle : MonoBehaviour
{
    [SerializeField] Text ShopTextArticle;
    [SerializeField] ItemData ItemData;

    // Update is called once per frame
    void Update()
    {
        ShopTextArticle.text = $"{ItemData.itemName}�𔃂��܂����H\n ���z{ItemData.price}";
    }

    // Method to update ItemData
    public void UpdateItemData(ItemData newItemData)
    {
        ItemData = newItemData;
    }
}
