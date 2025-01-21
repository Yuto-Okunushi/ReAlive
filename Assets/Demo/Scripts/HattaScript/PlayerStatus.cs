using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance = null;

    public float maxHydration = 100f; // 最大水分量
    public float maxStress = 100f;    // 最大ストレス量

    public float currHyd = 0;    // 現在の水分量
    public float currStress = 0; // 現在のストレス量


    public float hydRatePerUnit = 0.005f;    // 水分減少率（移動距離1ユニットあたり）
    public float stressRatePerUnit = 0.005f; // ストレス減少率（移動距離1ユニットあたり）

    private Player player;     // プレイヤーの参照
    private Vector3 lastPos;   // プレイヤーの最後の位置

    private PostProcessVolume ppVolume; // ポストプロセスボリュームの参照
    private List<PostProcessEffectSettings> effects = new List<PostProcessEffectSettings>(); // 効果リスト

    public UnityEngine.UI.Image hydrationGauge; // 水分ゲージのImage
    public UnityEngine.UI.Image stressGauge;    // ストレスゲージのImage

    //==奥主が追加したやつ=================================================================
    public int playerinitialmony = 3000;        //プレイヤーの所持金
    public int playerHaveItem = 0;          //現在のプレイヤーアイテム所持数
    public int playerMaxHaveItem = 5;       //限界所持数
    [SerializeField] TalkEventController talkEventController;        // 会話システム参照
    [SerializeField] TimeLineControll timeLineControll;         // TimelineControll参照
    //=====================================================================================

    

    void Start()
    {
        // 初期化
        currHyd = maxHydration;
        currStress = maxStress;
        lastPos = player.transform.position;
        GameManager.SetPlayerMony(playerinitialmony);   //最初の所持金を受け渡す
        GameManager.SetPlayerHydration(currHyd);        //最初の水分量を受け渡す
        GameManager.SetPlayerStress(currStress);        //最初のストレス値を受け渡す
        GameManager.SetTotalItem(playerHaveItem);       //最初に所持数を受け渡す


        // ポストプロセス効果の取得
        ppVolume = FindObjectOfType<PostProcessVolume>();
        if (ppVolume != null && ppVolume.profile != null)
        {
            foreach (var setting in ppVolume.profile.settings)
            {
                effects.Add(setting);
            }
        }

        // UIの初期化
        UpdateHydrationGauge();
        UpdateStressGauge();

             
    }

    void Update()
    {
        // ステータス更新
        UpdateStats();
    }

    // プレイヤーの位置に基づいてステータスを更新
    void UpdateStats()
    {
        GameManager.GetPlayerHydration(currHyd);
        GameManager.GetPlayerStress(currStress);
        playerHaveItem = GameManager.GetItemTotal();
        float distance = Vector3.Distance(player.transform.position, lastPos); // 移動距離の計算
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // プレイヤーが走っているかを確認
        ReduceHydration(distance, isRunning); // 水分を減少
        ReduceStress(distance);               // ストレスを減少
        lastPos = player.transform.position;  // 最後の位置を更新
        playerinitialmony = GameManager.GetPlayerMony();        //ゲームマネージャーからデータの受け取り
    }

    // 水分を減少させ、移動速度を調整
    void ReduceHydration(float distance, bool isRunning)
    {
        float rate = isRunning ? hydRatePerUnit * 1.2f : hydRatePerUnit; // 走っている場合は減少率を1.2倍にする
        currHyd -= distance * rate; // 水分量を減少
        UpdateHydrationGauge(); // 水分ゲージを更新
        if (currHyd <= 0)
        {
            // 水分が0になった場合の処理
            currHyd = 0;
            Die(); // プレイヤーを死亡させる
        }
        else if(currHyd >= maxHydration)     //値を最大値以上にしないときの処理
        {
            currHyd = maxHydration;
        }
        else
        {
            // 水分量に応じて移動速度を調整
            float speedFactor = Mathf.Clamp01(currHyd / maxHydration);
            player.walkSpeed = player.baseWalkSpeed * speedFactor;
            player.runSpeed = player.baseRunSpeed * speedFactor;
        }
    }

    // ストレスを減少させ、視覚効果を適用
    void ReduceStress(float distance)
    {
        currStress -= distance * stressRatePerUnit; // ストレス量を減少
        UpdateStressGauge(); // ストレスゲージを更新
        if (currStress <= 0)
        {
            // ストレスが0になった場合の処理
            currStress = 0;
        }
        else if(currStress >= maxStress)        //最大値以上になった時の処理
        {
            currStress = maxStress;
        }
        // ストレス量に応じて効果を適用
        float effectFactor = 0.3f * (1 - Mathf.Clamp01(currStress / maxStress)); // 効果をさらに弱める
        ApplyEffects(effectFactor);
    }

    // プレイヤーを死亡させる処理
    void Die()
    {
        player.gameObject.SetActive(false);
    }

    // ポストプロセス効果を適用
    void ApplyEffects(float intensity)
    {
        foreach (var effect in effects)
        {
            if (effect is DepthOfField dof)
            {
                // Depth of Field（ぼやけ効果）の適用
                dof.focusDistance.value = Mathf.Lerp(10f, 2f, intensity); // 効果をさらに弱める
                dof.aperture.value = Mathf.Lerp(5.6f, 22f, intensity); // 効果をさらに弱める
            }
            else if (effect is ColorGrading cg)
            {
                // Color Grading（暗さ効果）の適用
                cg.postExposure.value = Mathf.Lerp(0f, -10f, intensity); // 効果をさらに弱める
                cg.contrast.value = Mathf.Lerp(0f, 100f, intensity); // 効果をさらに弱める
            }
            else if (effect is Vignette vg)
            {
                // Vignette（周囲暗さ効果）の適用
                vg.intensity.value = Mathf.Lerp(0f, 1f, intensity); // 効果をさらに弱める
                vg.smoothness.value = Mathf.Lerp(0.2f, 0.5f, intensity); // 効果をさらに弱める
            }
        }
    }

    // 水分ゲージを更新
    void UpdateHydrationGauge()
    {
        hydrationGauge.fillAmount = currHyd / maxHydration;
        GameManager.SetPlayerHydration(currHyd);
    }

    // ストレスゲージを更新
    void UpdateStressGauge()
    {
        stressGauge.fillAmount = currStress / maxStress;
        GameManager.SetPlayerStress(currStress);

    }

    //プレイヤー状態を初期値に戻すメソッド
    public void ResetPlayerStates()
    {
        Debug.Log("このスクリプトは実行されてるよ！");
        // 水分量
        currHyd = maxHydration;
        // ストレス値
        currStress = maxStress;
        // 所持金
        playerinitialmony = 3000;
        // 現在のプレイヤーアイテム所持数
        playerHaveItem = 0;
        // ゲームの最初に働通知が来るシステムを再生
        talkEventController.notice();
        //地震が起きる時間をリセット
        timeLineControll.EarthQuakeTimeReset();
    }
}
