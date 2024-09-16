using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDateBase : MonoBehaviour
{
    [SerializeField] private ItemData itemDatabase;

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

    public void SendHydration()
    {
        Debug.Log("ボタンが押されました");
        GameManager.SetPlayerHydration(playerHydration);        //アイテム使用による水分データの受け渡し
        GameManager.SetPlayerStress(playerStress);              //アイテム使用によるストレスデータの受け渡し
    }

    

    
}
