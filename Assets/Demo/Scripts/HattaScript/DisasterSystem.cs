using System.Collections;
using UnityEngine;

public class DisasterSystem : MonoBehaviour
{
    public static DisasterSystem Instance { get; private set; } // シングルトンのインスタンス

    public Vector3 startPoint; // スタート地点
    public Vector3 destPoint; // 目的地

    private bool disasterTrig = false;
    private float evacTimer = 180f; // 3分(180秒)のタイマー

    private void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ゲームオブジェクトがシーン間で破棄されないようにする
            UnityEngine.Debug.Log("DisasterSystem インスタンスが設定されました");
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄
        }
    }

    void Start()
    {
        // ゲームループのコルーチンを開始
        StartCoroutine(GameLoop());
    }

    // ゲームループの管理
    IEnumerator GameLoop()
    {
        while (true)
        {
            // 準備フェーズの開始
            yield return StartCoroutine(Prep());

            // 災害発生フェーズ
            yield return StartCoroutine(Disaster());

            // 避難フェーズの開始
            yield return StartCoroutine(Evac());
        }
    }

    // 準備フェーズの処理
    IEnumerator Prep()
    {
        UnityEngine.Debug.Log("準備フェーズ開始");

        // 災害フラグのリセット
        disasterTrig = false;

        // 準備フェーズのアニメーションを開始
        yield return StartCoroutine(PanelUI.Instance.PrepAnim());

        // ランダムな時間待機 (3分から4分の間)
        float randomTime = UnityEngine.Random.Range(10f, 24f); // 変更要必須
        yield return new WaitForSeconds(randomTime);

        // 災害発生をトリガー
        TriggerDisaster();
    }

    // 災害発生をトリガー
    void TriggerDisaster()
    {
        if (!disasterTrig)
        {
            disasterTrig = true;
            UnityEngine.Debug.Log("災害発生");

            // プレイヤーの動きを停止
            Player.Instance.enabled = false;

            // 災害効果をトリガー
            DisasterEffect.Instance.TriggerDisaster();
        }
    }

    // 災害発生フェーズの処理
    IEnumerator Disaster()
    {
        UnityEngine.Debug.Log("災害フェーズ開始");

        // 災害発生エフェクトの持続時間待機
        yield return new WaitForSeconds(DisasterEffect.Instance.duration);

        // 災害効果を停止
        DisasterEffect.Instance.StopDisaster();

        // プレイヤーの動きを再開
        Player.Instance.enabled = true;

        // 準備フェーズの終了処理
        EndPrep();
    }

    // 準備フェーズの終了処理
    void EndPrep()
    {
        UnityEngine.Debug.Log("準備フェーズ終了");
        // 準備フェーズを無効化
        if (PanelUI.Instance.prepPanel != null)
        {
            PanelUI.Instance.prepPanel.gameObject.SetActive(false);
        }
    }

    // 避難フェーズの処理
    IEnumerator Evac()
    {
        UnityEngine.Debug.Log("避難フェーズ開始");

        // 避難フェーズのアニメーションを開始
        yield return StartCoroutine(PanelUI.Instance.EvacAnim());

        float timer = evacTimer;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            // プレイヤーが目的地に到達したかチェック
            if (Vector3.Distance(Player.Instance.transform.position, destPoint) < 1f)
            {
                ReachDest();
                yield break; // コルーチンを終了
            }

            yield return null;
        }

        // タイマーが切れた場合
        EndEvac();
    }

    // 避難フェーズの終了処理（タイムアウト）
    void EndEvac()
    {
        UnityEngine.Debug.Log("避難フェーズ終了 - タイムアウト");

        // プレイヤーオブジェクトを消す
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
    }

    // プレイヤーが目的地に到達した時の処理
    void ReachDest()
    {
        UnityEngine.Debug.Log("プレイヤーが目的地に到達");

        // プレイヤーをスタート地点に戻す
        Player.Instance.transform.position = startPoint;
    }
}
