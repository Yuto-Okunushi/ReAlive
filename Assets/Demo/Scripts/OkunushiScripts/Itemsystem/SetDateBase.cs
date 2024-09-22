using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDateBase : MonoBehaviour
{
    [SerializeField] public ItemData itemDatabase;

    public string itemDescription;
    public string itemDetails;
    public Sprite itemImage;  // Imageコンポーネントの変数を宣言

    public float playerHydration;       //水分データをゲームマネージャーに受け渡す
    public float playerStress;       //ストレスデータをゲームマネージャーに受け渡す

    public GameObject YesNopanel;       //使うか最終的な判断をさせるパネル表示、アタッチ出来ないやつ


    public void SendDate()
    {
        if (itemImage != null)
        {
            GameManager.SetItemImage(itemImage);  // 取得したImageコンポーネントのスプライトを設定
        }
        GameManager.SetItemName(itemDescription);
        GameManager.SetItemDetailsName(itemDetails);
    }

    public void PlusHydrationStress()
    {
        GameManager.SendPulusHydration(playerHydration);        //アイテム使用による水分データの受け渡し
        GameManager.SendPlusStress(playerStress);               //アイテム使用によるストレス回復データの受け渡し
        Destroy(this.gameObject);                               //使用後にこのオブジェクトを削除する
    }

    

    

    
}
