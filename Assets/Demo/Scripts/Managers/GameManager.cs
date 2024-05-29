using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //==タイムアタック関係=========================================================
    public int[] taTotalScore = new int[4];     //Perfect Great Goodの空箱作成
    public float taSumScore;                      //スコアの合計値の変数
    //=============================================================================


    //=トレーニングシステム関係====================================================
    public float trSecondSumTime;   //プレイ時間(秒)
    public int trMinuteSumTime;   //プレイ時間(分)
    public int trHourSumTIme;   //プレイ時間(時間)
    public float trAverage;         //平均値
    public int trCount;             //回答数
    public int trPlayCount;         //プレイ回数

    

    //=============================================================================

    //=タイムアタックトレーニング共通==============================================
    public int sumCombo;
    //=============================================================================

    public string nowScene;     //現在どのシーンにいるか

    public bool isSceneLoading = false;   //現在シーン遷移してるか。

    

    public bool isGame = false; //現在何らかのモードのゲーム中かどうか

    public bool isBoosting1;        //コンボの待機をしているかどうか3コンボの時
    public bool isBoosting2;        //コンボの待機をしているかどうか５コンボの時

    [HideInInspector]
    public string[] KatakanaArray =
    {
        "ア", "イ", "ウ", "エ", "オ",
        "カ", "キ", "ク", "ケ", "コ",
        "サ", "シ", "ス", "セ", "ソ",
        "タ", "チ", "ツ", "テ", "ト",
        "ナ", "ニ", "ヌ", "ネ", "ノ",
        "ハ", "ヒ", "フ", "ヘ", "ホ",
        "マ", "ミ", "ム", "メ", "モ",
        "ヤ", "ユ", "ヨ",
        "ラ", "リ", "ル", "レ", "ロ",
        "ワ", "ヰ", "ヱ", "ヲ",
        "ン", "ー",
        "ガ", "ギ", "グ", "ゲ", "ゴ",
        "ザ", "ジ", "ズ", "ゼ", "ゾ",
        "ダ", "ヂ", "ヅ", "デ", "ド",
        "バ", "ビ", "ブ", "ベ", "ボ",
        "ヴ",
        "パ", "ピ", "プ", "ペ", "ポ",
        "ァ", "ィ", "ゥ", "ェ", "ォ",
        "ャ", "ュ", "ョ", "ヮ", "ッ"
    };  //カタカナ配列

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

    static public int GetTaTotalScore(int n)
    {
        //error回避
        if (n >= 0 && n < instance.taTotalScore.Length)
        {
            return instance.taTotalScore[n];
        }

        //予想される配列のlengthより大きい数を参照しようとしたら
        else
        {
            return -1;
            //のちのー１以下の処理を書く
        }
    }

    static public float GetTaSumScore()
    {
        return instance.taSumScore;
    }

    static public float GetTrSecendSumTime()
    {
        return instance.trSecondSumTime;
    }

    static public int GetTrMinuteSumTime()
    {
        return instance.trMinuteSumTime;
    }

    static public int GetTrHourSumTime()
    {
        return instance.trHourSumTIme;
    }

    static public float GetTrAnswerAverage()
    {
        return instance.trAverage;
    }

    static public int GetTrAnswerCount()
    {
        return instance.trCount;
    }

    static public int GetTrPlayCount()
    {
        return instance.trPlayCount;
    }

    static public int GetTaTlCombo()
    {
        return instance.sumCombo;
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


    static public string getKatakanaArray(int n)
    {
        //error回避
        if (n >= 0 && n < instance.KatakanaArray.Length)
        {
            return instance.KatakanaArray[n];
        }

        //予想される配列のlengthより大きい数を参照しようとしたら
        else
        {
            return null;
        }
    }

    static public int getKatakanaArrayLength()
    {
        return instance.KatakanaArray.Length;
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

    static public void SetTaTotalScore(int n, int value)
    {
        if(n >= 0 && n < instance.taTotalScore.Length)
        {
            instance.taTotalScore[n] = value;       //n番目の箱にvalueを代入している
        }
        else
        {
            return;             //例外処理
        }
    }

    static public void SetTaTlsumCombo(int value)
    {
        instance.sumCombo = value;
    }

    static public void SetTaSumScore(float value)
    {
        instance.taSumScore = value;
    }

    static public void SetTrSecondSumTime(float value)
    {
        instance.trSecondSumTime = value;
    }

    static public void SetTrMinuteSumTime(int value)
    {
        instance.trMinuteSumTime = value;
    }

    static public void SetTrHourSumTIme(int value)
    {
        instance.trHourSumTIme = value;
    }
    
    static public void SetAnswerAverage(float value)
    {
        instance.trAverage = value;
    }

    static public void SetAnswerCount(int value)
    {
        instance.trCount = value;
    }

    static public void SetPlayCount(int value)
    {
        instance.trPlayCount = value;
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

    

}
