using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    //インスタンス作成
    public static GameManager instance = null;

    //ゲームマネージャ内メンバ変数

    //===音関係====================================================================
    public float volumeBGM; //BGMの音量
    public float volumeSE;  //SoundEffectの音量
    public float volumeVOICE; //Voiceの音量
    public float durationCrossBGM;  //BGMをクロスフェードさせるときの間の時間
    //=============================================================================

    

    public string nowScene;     //現在どのシーンにいるか

    public bool isSceneLoading = false;   //現在シーン遷移してるか。

    

    public bool isGame = false; //現在何らかのモードのゲーム中かどうか

    public bool isBoosting1;        //コンボの待機をしているかどうか3コンボの時
    public bool isBoosting2;        //コンボの待機をしているかどうか５コンボの時

    public int Totalitem;

    public string ItemName;         //アイテムの名前を表示させる
    public string ItemDetailsName;      //アイテムの詳細を表示させる
    public Sprite ItemImage;
    public ItemData ShopItemDate;        //ショップアイテムのデータ格納
    public SignDate Signdate;           //標識データの格納
    public int Playerhavemony;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //===METHOD==========================================================================================================

    static public IEnumerator DelaySceneTransition(float delayButtonAnim, float delaySceneTrans, AudioManager.SEType se, GameObject parent, string nextSceneName)
    {
        AudioManager.PlaySE(se);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(delayButtonAnim);
        SceneController.SceneTransitionAnimation(parent);
        yield return new WaitForSeconds(delaySceneTrans);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneController.LoadNextScene(nextSceneName);
    }

    static public IEnumerator DelaySceneTransition(float delayButtonAnim, float delaySceneTrans, AudioManager.SEType se, GameObject parent, string nextSceneName, string transitionName)
    {
        AudioManager.PlaySE(se);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(delayButtonAnim);
        SceneController.SceneTransitionAnimation(parent,transitionName);
        yield return new WaitForSeconds(delaySceneTrans);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneController.LoadNextScene(nextSceneName);
    }

    static public void SelectButton()
    {
        AudioManager.PlaySE(AudioManager.SEType.Cursor);
    }


    //===GETTER==========================================================================================================
    static public float GetVolumeBGM()
    {
        return instance.volumeBGM;
    }

    static public float GetVolumeSE()
    {
        return instance.volumeSE;
    }

    static public float GetVolumeVOICE()
    {
        return instance.volumeVOICE;
    }

    static public float GetDurationCrossBGM()
    {
        return instance.durationCrossBGM;
    }

    

    static public string GetNowScene()
    {
        return instance.nowScene;
    }

    static public bool GetIsSceneLoading()
    {
        return instance.isSceneLoading;
    }

    static public bool GetIsGame()
    {
        return instance.isGame;
    }

    static public bool GetBoostingReset1()      //コンボによるタイム増加のbool変数３コンボの場合の関数
    {
        return instance.isBoosting1;
    }

    static public bool GetBoostingReset2()      //コンボによるタイム増加のbool変数5コンボの場合の関数
    {
        return instance.isBoosting2;
    }

    static public int GetItemTotal()
    {
        return instance.Totalitem;
    }

    static public string getItemName()
    {
        return instance.ItemName;
    }

    static public string getItemDetailsName()
    {
        return instance.ItemDetailsName;
    }

    static public Sprite getItemImage()
    {
        return instance.ItemImage;
    }

    static public int GetPlayerMony()
    {
        return instance.Playerhavemony;
    }

    static public ItemData GetItemDate(ItemData value)
    {
        return instance.ShopItemDate = value;
    }

    static public SignDate GetSignDate()
    {
        return instance.Signdate;
    }


    //===SETTER==========================================================================================================
    static public float SetVolumeBGM(float value)
    {
        if (value > 0)
        {
            return instance.volumeBGM = value;
        }
        else
        {
            return instance.volumeBGM = 0;
        }
    }

    static public float SetVolumeSE(float value)
    {
        if (value > 0)
        {
            return instance.volumeSE = value;
        }
        else
        {
            return instance.volumeSE = 0;
        }
    }

    static public float SetVolumeVOICE(float value)
    {
        if (value > 0)
        {
            return instance.volumeVOICE = value;
        }
        else
        {
            return instance.volumeVOICE = 0;
        }
    }

    static public void SetDurationCrossBGM(float value)
    {
        instance.durationCrossBGM = value;
    }

    

    static public void SetNowScene(string name)
    {
        instance.nowScene = name;
    }

    static public void SetIsSceneLoading(bool tf)
    {
        instance.isSceneLoading = tf;
    }

    static public void SetIsGame(bool tf)
    {
        instance.isGame = tf;
    }

    static public void SetBoostingReset1(bool tf)        ////コンボによるタイム増加のbool変数３コンボの場合の関数
    {
        instance.isBoosting1 = tf;
    }

    static public void SetBoostingReset2(bool tf)        ////コンボによるタイム増加のbool変数5コンボの場合の関数
    {
        instance.isBoosting2 = tf;
    }

    static public void SetTotalItem(int value)
    {
        instance.Totalitem = value;
    }

    static public void SetItemName(string value)
    {
        instance.ItemName = value;
    }

    static public void SetItemDetailsName(string value)
    {
        instance.ItemDetailsName = value;
    }

    static public void SetItemImage(Sprite value)
    {
        instance.ItemImage= value;
    }

    

    static public void SetPlayerMony(int value)
    {
        instance.Playerhavemony = value;
    }

    public static void SetItemDate(ItemData itemData)
    {
        instance.ShopItemDate = itemData;
    }

    public static void SetSignDate(SignDate signDate)
    {
        instance.Signdate = signDate;
    }
}
