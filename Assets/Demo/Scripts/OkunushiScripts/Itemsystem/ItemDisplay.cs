using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public ItemData itemData;      // ScriptableObject���Q��

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
        itemNameText.text = "���O: " + itemData.itemName;
        priceText.text = "���z: " + itemData.price.ToString();
        hpText.text = "HP: " + itemData.hp.ToString();
        hydrationText.text = "����: " + itemData.hydration.ToString();
        foodText.text = "�H��: " + itemData.food.ToString();
        itemImage.sprite = itemData.itemImage;
    }
}
