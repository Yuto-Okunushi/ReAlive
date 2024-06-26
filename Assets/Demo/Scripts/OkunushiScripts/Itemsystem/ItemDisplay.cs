using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Text Item;
    [SerializeField] Text ItemDetails;
    [SerializeField] GameObject ItemSpriteObject;  // Imageコンポーネントを持つGameObject

    private Image itemImage;  // Imageコンポーネントへの参照

    private void Start()
    {
        // ItemSpriteObjectからImageコンポーネントを取得
        if (ItemSpriteObject != null)
        {
            itemImage = ItemSpriteObject.GetComponent<Image>();
            if (itemImage == null)
            {
                Debug.LogError("ItemSpriteObject does not have an Image component!");
            }
        }
        else
        {
            Debug.LogError("ItemSpriteObject is not assigned!");
        }
    }

    private void Update()
    {
        // GameManagerのItemNameを取得してテキストに表示する
        string itemName = GameManager.getItemName();
        Item.text = itemName;

        // GameManagerのItemDetailsNameを取得してテキストに表示する
        string itemDetails = GameManager.getItemDetailsName();
        ItemDetails.text = itemDetails;

        // GameManagerのItemImageを取得してImageコンポーネントに表示する
        Sprite itemSprite = GameManager.getItemImage();
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
        }
    }
}
