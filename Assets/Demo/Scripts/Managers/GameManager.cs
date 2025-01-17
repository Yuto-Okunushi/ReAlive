using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    //インスタンス作成
    public static GameManager instance = null;

    public float test;
    public float PlusTest;

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
    public int Playerhavemony;          //プレイヤーの所持金
    public float PlayerHp;              //プレイヤーのHP
    public float PlayerStress = 0;      //プレイヤーのストレス値
    public float PlayerHydration = 0;       //プレイヤーの水分量

    public float maxHydration = 100f; // 最大水分量
    public float maxStress = 100f;    // 最大ストレス量

    public bool isTimeline = false;     //タイムライン中かのフラグ

    public string TalkDate;         //CSVのトークデータ受け渡し変数
    public string NameDate;         //CSVの名前トークデータ受け渡し変数

    public bool isTalking = false;      //会話イベント中かのフラグ

    public bool isOpend = false;        //インベントリが開かれているかのフラグ

    public bool isMapShow = true;       // マップが表示可能かのフラグ

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
        SceneController.SceneTransitionAnimation(parent, transitionName);
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
    static public bool GetIsTalking()       //会話イベント中
    {
        return instance.isTalking;
    }

    //===使用していないメソッド、エラー回避の為に消していません=============================================================
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
    //=ここまでスルーしてください==================================================================================================================


    static public string GetNowScene()      //現在のシーン
    {
        return instance.nowScene;
    }

    static public bool GetIsSceneLoading()      //シーンのロード中か
    {
        return instance.isSceneLoading;
    }

    static public bool GetIsMapShow()      //マップが開けるか
    {
        return instance.isMapShow;
    }

    static public bool GetIsGame()          //操作中か確認
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

    static public int GetItemTotal()        //アイテム所持数の取得
    {
        return instance.Totalitem;
    }

    static public string getItemName()      //アイテムの名前を取得
    {
        return instance.ItemName;
    }

    static public string getItemDetailsName()       //アイテム詳細情報の取得
    {
        return instance.ItemDetailsName;
    }

    static public Sprite getItemImage()         //アイテム画像取得
    {
        return instance.ItemImage;
    }

    static public int GetPlayerMony()           //プレイヤーの所持金取得
    {
        return instance.Playerhavemony;
    }

    static public SignDate GetSignDate()        //標識データの取得
    {
        return instance.Signdate;
    }

    static public float GetPlayerStress(float value)        //ストレス受け渡し
    {
        instance.PlayerStress = value;
        return instance.PlayerStress + instance.test;
    }

    static public float GetPlayerHydration(float value)     //水分受け渡し
    {
        instance.PlayerHydration = value; // 渡された値を PlayerHydration に代入
        return instance.PlayerHydration + instance.test;    // 合計値を返す

    }

    static public bool GetTimelineflug()            //タイムライン中か取得
    {
        return instance.isTimeline;
    }

    static public string GetCSVTalk()               //CSV会話データ取得
    {
        return instance.TalkDate;
    }

    static public string GetCSVCharactorName()      //CSVデータからキャラクター名の取得
    {
        return instance.NameDate;
    }

    static public bool GetIsOpend()                 //インベントリがあいているかの取得
    {
        return instance.isOpend;
    }
    //===SETTER==========================================================================================================
    static public bool SetIsOpend(bool value)       // インベントリが開かれているかどうか
    {
        return instance.isOpend = value;    
    }

    static public bool SetIsMapShow(bool value)       // マップが開かれているかどうか
    {
        return instance.isMapShow = value;
    }

    static public bool SetIsTalking(bool value)         //会話イベント中かの判定
    {
        return instance.isTalking = value;
    }


    static public void SetNowScene(string name)         //現在のシーン
    {
        instance.nowScene = name;
    }

    static public void SetIsTimeline(bool tf)           //タイムライン再生中か確認
    {
        instance.isTimeline = tf;
    }

    static public void SetTotalItem(int value)          //アイテム所持数
    {
        instance.Totalitem = value;
    }

    static public void SetItemName(string value)        //アイテム名
    {
        instance.ItemName = value;
    }

    static public void SetItemDetailsName(string value)         //アイデム詳細データ
    {
        instance.ItemDetailsName = value;
    }

    static public void SetItemImage(Sprite value)               //アイテムイメージ
    {
        instance.ItemImage = value;
    }



    static public void SetPlayerMony(int value)                 //プレイヤーの所持金
    {
        instance.Playerhavemony = value;
    }

    static public void SetSignDate(SignDate signDate)           //標識データ
    {
        instance.Signdate = signDate;
    }

    static public void SetPlayerHp(float value)     //プレイヤーのHP受け渡し
    {
        instance.PlayerHp = value;
    }

    static public void SetPlayerStress(float value)     //プレイヤーのストレス受け渡し
    {
        instance.PlayerStress = value;
    }

    static public void SendPlusStress(float value)          //ストレス値
    {
        instance.PlayerStress += value;
        PlayerStatus.Instance.currStress = instance.PlayerStress;

        if (PlayerStatus.Instance.currStress > 100)
        {
            instance.PlayerStress = 100;

        }
    }

    static public void SetPlayerHydration(float value)     //プレイヤーの水分受け渡し
    {
        instance.PlayerHydration = value;
    }

    static public void SendPulusHydration(float value)      //プレイヤー水分量を足す
    {
        instance.PlayerHydration += value;
        PlayerStatus.Instance.currHyd = instance.PlayerHydration; // 値をPlayerStatusに反映

        if(PlayerStatus.Instance.currHyd > 100)
        {
            instance.PlayerHydration = 100;
            
        }
    }

    static public void SendCSVTalk(string talknumber)           //CSVから会話内容を送る
    {
        instance.TalkDate = talknumber;
    }

    static public void SendCSVCharactorName(string NameNumber)      //CSVのキャラクターの名前を送る
    {
        instance.NameDate = NameNumber;
    }
}
