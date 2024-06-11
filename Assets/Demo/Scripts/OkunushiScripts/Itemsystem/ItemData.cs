using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        // �A�C�e���̖��O
    public int price;              // �A�C�e���̋��z
    public int hp;                 // �A�C�e����HP
    public int hydration;          // �A�C�e���̐���
    public int food;               // �A�C�e���̐H��
    public Sprite itemImage;       // �A�C�e���̃C���[�W�摜
}
