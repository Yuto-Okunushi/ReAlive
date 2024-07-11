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
}
