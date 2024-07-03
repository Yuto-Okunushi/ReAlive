using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;        // アイテムの名前
    public int price;              // アイテムの金額
    public int hp;                 // アイテムのHP
    public int hydration;          // アイテムの水分
    public int food;               // アイテムの食料
    private GameObject itemSpawns;       //スポーンさせるアイテム
    public Sprite itemImage;       // アイテムのイメージ画像

    public string ItemName { get => itemName; set => itemName = value; }
    public int Price { get => price; set => price = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Hydration { get => hydration; set => hydration = value; }
    public int Food { get => food; set => food = value; }
    public GameObject ItemSpawns { get => itemSpawns; set => itemSpawns = value; }
}
