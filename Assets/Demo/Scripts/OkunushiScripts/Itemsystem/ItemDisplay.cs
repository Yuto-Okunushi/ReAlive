using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ItemData itemData;      // ScriptableObjectÇéQè∆

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
        itemNameText.text = "ñºëO: " + itemData.itemName;
        priceText.text = "ã‡äz: " + itemData.price.ToString();
        hpText.text = "HP: " + itemData.hp.ToString();
        hydrationText.text = "êÖï™: " + itemData.hydration.ToString();
        foodText.text = "êHóø: " + itemData.food.ToString();
        itemImage.sprite = itemData.itemImage;
    }
}
