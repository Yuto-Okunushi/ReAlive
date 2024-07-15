using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    public float maxHydration = 100f; // 最大水分量
    public float maxStress = 100f;    // 最大ストレス量

    private float currHyd;    // 現在の水分量
    private float currStress; // 現在のストレス量

    public float hydRatePerUnit = 0.005f;    // 水分減少率（移動距離1ユニットあたり）
    public float stressRatePerUnit = 0.005f; // ストレス減少率（移動距離1ユニットあたり）

    private Player player;     // プレイヤーの参照
    private Vector3 lastPos;   // プレイヤーの最後の位置

    private PostProcessVolume ppVolume; // ポストプロセスボリュームの参照
    private List<PostProcessEffectSettings> effects = new List<PostProcessEffectSettings>(); // 効果リスト

    public UnityEngine.UI.Image hydrationGauge; // 水分ゲージのImage
    public UnityEngine.UI.Image stressGauge;    // ストレスゲージのImage

    void Awake()
    {
        // シングルトンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 初期化
        player = Player.Instance;
        currHyd = maxHydration;
        currStress = maxStress;
        lastPos = player.transform.position;

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
        float distance = Vector3.Distance(player.transform.position, lastPos); // 移動距離の計算
        if (distance > 0)
        {
            // 移動があった場合のみ更新
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // プレイヤーが走っているかを確認
            ReduceHydration(distance, isRunning); // 水分を減少
            ReduceStress(distance);               // ストレスを減少
            lastPos = player.transform.position;  // 最後の位置を更新
        }
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
            Die(); // プレイヤーを死亡させる
        }
        else
        {
            // ストレス量に応じて効果を適用
            float effectFactor = 0.3f * (1 - Mathf.Clamp01(currStress / maxStress)); // 効果をさらに弱める
            ApplyEffects(effectFactor);
        }
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
                dof.focusDistance.value = Mathf.Lerp(10f, 7f, intensity); // 効果をさらに弱める
                dof.aperture.value = Mathf.Lerp(5.6f, 8f, intensity); // 効果をさらに弱める
            }
            else if (effect is ColorGrading cg)
            {
                // Color Grading（暗さ効果）の適用
                cg.postExposure.value = Mathf.Lerp(0f, -3f, intensity); // 効果をさらに弱める
                cg.contrast.value = Mathf.Lerp(0f, 30f, intensity); // 効果をさらに弱める
            }
            else if (effect is Vignette vg)
            {
                // Vignette（周囲暗さ効果）の適用
                vg.intensity.value = Mathf.Lerp(0f, 0.3f, intensity); // 効果をさらに弱める
                vg.smoothness.value = Mathf.Lerp(0.2f, 0.25f, intensity); // 効果をさらに弱める
            }
        }
    }

    // 水分ゲージを更新
    void UpdateHydrationGauge()
    {
        hydrationGauge.fillAmount = currHyd / maxHydration;
    }

    // ストレスゲージを更新
    void UpdateStressGauge()
    {
        stressGauge.fillAmount = currStress / maxStress;
    }
}
