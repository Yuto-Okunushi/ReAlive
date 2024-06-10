using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    public float maxHydration = 100f; // 最大水分量
    public float maxStress = 100f;    // 最大ストレス量

    private float currHyd;    // 現在の水分量
    private float currStress; // 現在のストレス量

    public float hydRatePerUnit = 0.1f;    // 水分減少率（移動距離1ユニットあたり）
    public float stressRatePerUnit = 0.1f; // ストレス減少率（移動距離1ユニットあたり）

    private Player player;     // プレイヤーの参照
    private Vector3 lastPos;   // プレイヤーの最後の位置

    private PostProcessVolume ppVolume; // ポストプロセスボリュームの参照
    private List<PostProcessEffectSettings> effects = new List<PostProcessEffectSettings>(); // 効果リスト

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
        UnityEngine.Debug.Log("初期化完了。現在の水分量: " + currHyd + ", 現在のストレス量: " + currStress);

        // ポストプロセス効果の取得
        ppVolume = FindObjectOfType<PostProcessVolume>();
        if (ppVolume != null && ppVolume.profile != null)
        {
            foreach (var setting in ppVolume.profile.settings)
            {
                effects.Add(setting);
            }
            UnityEngine.Debug.Log("PostProcess設定を取得しました。");
        }
        else
        {
            UnityEngine.Debug.LogWarning("Post Process VolumeまたはProfileが見つかりません。");
        }
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
        UnityEngine.Debug.Log("移動距離: " + distance);
        if (distance > 0)
        {
            // 移動があった場合のみ更新
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // プレイヤーが走っているかを確認
            UnityEngine.Debug.Log("走っている: " + isRunning);
            ReduceHydration(distance, isRunning); // 水分を減少
            ReduceStress(distance);               // ストレスを減少
            lastPos = player.transform.position;  // 最後の位置を更新
        }
    }

    // 水分を減少させ、移動速度を調整
    void ReduceHydration(float distance, bool isRunning)
    {
        float rate = isRunning ? hydRatePerUnit * 2 : hydRatePerUnit; // 走っている場合は減少率を倍にする
        currHyd -= distance * rate; // 水分量を減少
        UnityEngine.Debug.Log("現在の水分量: " + currHyd);
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
        UnityEngine.Debug.Log("現在のストレス量: " + currStress);
        if (currStress <= 0)
        {
            // ストレスが0になった場合の処理
            currStress = 0;
            Die(); // プレイヤーを死亡させる
        }
        else
        {
            // ストレス量に応じて効果を適用
            float effectFactor = 1 - Mathf.Clamp01(currStress / maxStress);
            ApplyEffects(effectFactor);
        }
    }

    // プレイヤーを死亡させる処理
    void Die()
    {
        player.gameObject.SetActive(false);
        UnityEngine.Debug.Log("プレイヤーが死亡しました");
    }

    // ポストプロセス効果を適用
    void ApplyEffects(float intensity)
    {
        foreach (var effect in effects)
        {
            if (effect is DepthOfField dof)
            {
                // Depth of Field（ぼやけ効果）の適用
                dof.focusDistance.value = Mathf.Lerp(10f, 5f, intensity);
                dof.aperture.value = Mathf.Lerp(5.6f, 16f, intensity);
                UnityEngine.Debug.Log("ぼやけ効果適用: " + intensity);
            }
            else if (effect is ColorGrading cg)
            {
                // Color Grading（暗さ効果）の適用
                cg.postExposure.value = Mathf.Lerp(0f, -5f, intensity);
                cg.contrast.value = Mathf.Lerp(0f, 50f, intensity);
                UnityEngine.Debug.Log("画面の暗さ適用: " + intensity);
            }
            else if (effect is Vignette vg)
            {
                // Vignette（周囲暗さ効果）の適用
                vg.intensity.value = Mathf.Lerp(0f, 0.5f, intensity);
                vg.smoothness.value = Mathf.Lerp(0.2f, 0.5f, intensity);
                UnityEngine.Debug.Log("ビネット効果適用: " + intensity);
            }
        }
    }
}
