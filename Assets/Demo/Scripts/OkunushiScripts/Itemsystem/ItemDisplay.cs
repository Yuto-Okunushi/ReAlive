using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ItemData itemData;      // ScriptableObjectを参照

    public Text itemNameText;
    public Text priceText;
    public Text hpText;
    public Text hydrationText;
    public Text foodText;
    public Image itemImage;

    void Start()
    {
        if (itemData != null)
        {
            DisplayItemData();
        }
    }

    void DisplayItemData()
    {
        itemNameText.text = "名前: " + itemData.itemName;
        priceText.text = "金額: " + itemData.price.ToString();
        hpText.text = "HP: " + itemData.hp.ToString();
        hydrationText.text = "水分: " + itemData.hydration.ToString();
        foodText.text = "食料: " + itemData.food.ToString();
        itemImage.sprite = itemData.itemImage;
    }
}
