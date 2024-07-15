using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu( menuName = "ScriptableObjects/Create SignDate", fileName ="SignDatefile")]

public class SignDate : ScriptableObject
{
    public string signName;        //標識の名前
    public int singNumber;         //標識の登録番号
    public string signDetaile;      //標識の詳細設定
    public Sprite itemImage;       //標識のイメージ画像
}
