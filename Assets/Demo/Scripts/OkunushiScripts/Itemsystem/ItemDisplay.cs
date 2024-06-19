using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Text Item;
    [SerializeField] Text ItemDetails;

    private void Update()
    {
        // GameManager��ItemName���擾���ăe�L�X�g�ɕ\������
        string itemName = GameManager.getItemName();
        Item.text = itemName;
        string itemDetails = GameManager.getItemDetailsName();
        ItemDetails.text = itemDetails;
    }
}
