using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; } // シングルトンのインスタンス

    public float maxHydration = 100f; // 最大水分
    public float maxStress = 100f; // 最大ストレス

    private float currHyd; // 現在の水分
    private float currStress; // 現在のストレス

    public float hydRatePerUnit = 0.1f; // 移動距離1ユニットあたりの水分減少率
    public float stressRatePerUnit = 0.1f; // 移動距離1ユニットあたりのストレス減少率

    private Player player; // プレイヤーの参照
    private Vector3 lastPos; // プレイヤーの最後の位置

    void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ゲームオブジェクトがシーン間で破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合はこのオブジェクトを破棄
        }
    }

    void Start()
    {
        player = Player.Instance; // プレイヤーのインスタンスを取得
        currHyd = maxHydration; // 水分を最大値に設定
        currStress = maxStress; // ストレスを最大値に設定
        lastPos = player.transform.position; // 最初の位置を設定
        UnityEngine.Debug.Log("初期化完了。現在の水分量: " + currHyd + ", 現在のストレス量: " + currStress); // 初期状態をログに出力
    }

    void Update()
    {
        UpdateStatus(); // ステータスの更新
    }

    // プレイヤーの位置に基づいてステータスを更新
    void UpdateStatus()
    {
        float distance = Vector3.Distance(player.transform.position, lastPos); // 移動距離を計算
        UnityEngine.Debug.Log("移動距離: " + distance); // デバッグログに移動距離を出力
        if (distance > 0)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // プレイヤーが走っているかどうかを確認
            UnityEngine.Debug.Log("走っている: " + isRunning); // デバッグログにプレイヤーが走っているかどうかを出力
            DecreaseHyd(distance, isRunning); // 移動距離に応じて水分を減少
            DecreaseStress(distance); // 移動距離に応じてストレスを減少
            lastPos = player.transform.position; // 最後の位置を更新
        }
    }

    // 水分を減少させ、速度を調整
    void DecreaseHyd(float distance, bool isRunning)
    {
        float rate = isRunning ? hydRatePerUnit * 2 : hydRatePerUnit; // 走っている場合は減少率を倍にする
        currHyd -= distance * rate;
        UnityEngine.Debug.Log("現在の水分量: " + currHyd); // デバッグログに現在の水分を出力
        if (currHyd <= 0)
        {
            currHyd = 0;
            PlayerDie(); // 水分が0になったらプレイヤーが死ぬ
        }
        else
        {
            float speedFactor = Mathf.Clamp01(currHyd / maxHydration); // 水分に応じて速度を調整
            player.walkSpeed = player.baseWalkSpeed * speedFactor;
            player.runSpeed = player.baseRunSpeed * speedFactor;
        }
    }

    // ストレスを減少させ、画面のぼやけを調整
    void DecreaseStress(float distance)
    {
        currStress -= distance * stressRatePerUnit;
        UnityEngine.Debug.Log("現在のストレス量: " + currStress); // デバッグログに現在のストレスを出力
        if (currStress <= 0)
        {
            currStress = 0;
            PlayerDie(); // ストレスが0になったらプレイヤーが死ぬ
        }
        else
        {
            float blurFactor = 1 - Mathf.Clamp01(currStress / maxStress); // ストレスに応じてぼやけを調整
            ApplyBlur(blurFactor);
        }
    }

    // プレイヤーが死ぬ処理
    void PlayerDie()
    {
        player.gameObject.SetActive(false); // プレイヤーを非表示にする
        UnityEngine.Debug.Log("プレイヤーが死亡しました"); // デバッグログにプレイヤーの死亡を出力
        // 他のゲームオーバー処理
    }

    // ぼやけ効果を適用
    void ApplyBlur(float intensity)
    {
        // ここにぼやけ効果を適用するコードを追加
        // intensity を使用してぼやけ効果を調整します
    }
}
