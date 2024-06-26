using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Text Item;
    [SerializeField] Text ItemDetails;
    [SerializeField] GameObject ItemSpriteObject;  // Image�R���|�[�l���g������GameObject

    private Image itemImage;  // Image�R���|�[�l���g�ւ̎Q��

    private void Start()
    {
        // ItemSpriteObject����Image�R���|�[�l���g���擾
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
        // GameManager��ItemName���擾���ăe�L�X�g�ɕ\������
        string itemName = GameManager.getItemName();
        Item.text = itemName;

        // GameManager��ItemDetailsName���擾���ăe�L�X�g�ɕ\������
        string itemDetails = GameManager.getItemDetailsName();
        ItemDetails.text = itemDetails;

        // GameManager��ItemImage���擾����Image�R���|�[�l���g�ɕ\������
        Sprite itemSprite = GameManager.getItemImage();
        if (itemImage != null)
        {
            itemImage.sprite = itemSprite;
        }
    }
}
