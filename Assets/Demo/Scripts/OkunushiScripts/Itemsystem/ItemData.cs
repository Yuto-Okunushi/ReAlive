using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        // �A�C�e���̖��O
    public int price;              // �A�C�e���̋��z
    public int hp;                 // �A�C�e����HP
    public int hydration;          // �A�C�e���̐���
    public int food;               // �A�C�e���̐H��
    private GameObject itemSpawns;       //�X�|�[��������A�C�e��
    public Sprite itemImage;       // �A�C�e���̃C���[�W�摜

    public string ItemName { get => itemName; set => itemName = value; }
    public int Price { get => price; set => price = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Hydration { get => hydration; set => hydration = value; }
    public int Food { get => food; set => food = value; }
    public GameObject ItemSpawns { get => itemSpawns; set => itemSpawns = value; }
}
