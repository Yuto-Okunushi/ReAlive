using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Text Item;
    [SerializeField] Text ItemDetails;

    private void Update()
    {
        // GameManagerのItemNameを取得してテキストに表示する
        string itemName = GameManager.getItemName();
        Item.text = itemName;
        string itemDetails = GameManager.getItemDetailsName();
        ItemDetails.text = itemDetails;
    }
}
