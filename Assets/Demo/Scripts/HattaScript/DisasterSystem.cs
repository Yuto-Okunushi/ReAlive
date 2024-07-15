using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のために追加

public class DisasterSystem : MonoBehaviour
{
    public static DisasterSystem Instance { get; private set; } // シングルトンのインスタンス

    public Vector3 startPoint; // スタート地点
    public GameObject[] objectsToActivate; // ランダムで設定するオブジェクト群

    public GameObject activeObject; // アクティブになったオブジェクト
    public bool IsEvacPhase { get; private set; } // 避難フェーズが開始されたかどうか

    private bool disasterTrig = false; // 災害発生のトリガーフラグ
    private float evacTimer = 180f; // 3分(180秒)のタイマー
    private bool restartGameLoop = false; // ゲームループ再開のフラグ
    private Vector3 destPoint; // 目的地の位置

    private void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ゲームオブジェクトがシーン間で破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は破棄
        }
    }

    private void Start()
    {
        // 全てのオブジェクトを非アクティブにする
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // ゲームループのコルーチンを開始
        StartCoroutine(GameLoop());
    }

    // ゲームループの管理
    private IEnumerator GameLoop()
    {
        while (true)
        {
            // 準備フェーズの前に少し待機
            yield return new WaitForSeconds(5f);

            // 準備フェーズの開始
            yield return StartCoroutine(Prep());

            // 災害発生フェーズ
            yield return StartCoroutine(Disaster());

            // 避難フェーズの開始
            yield return StartCoroutine(Evac());

            // ゲームループ再開待ち
            yield return new WaitUntil(() => restartGameLoop);
            restartGameLoop = false;
        }
    }

    // 準備フェーズの処理
    private IEnumerator Prep()
    {
        // 災害フラグのリセット
        disasterTrig = false;

        // 準備フェーズのアニメーションを開始
        yield return StartCoroutine(PanelUI.Instance.PrepAnim());

        // ランダムな時間待機 (3分から4分の間)
        float randomTime = UnityEngine.Random.Range(1f, 10f);
        yield return new WaitForSeconds(randomTime);

        // 災害発生をトリガー
        TriggerDisaster();
    }

    // 災害発生をトリガー
    private void TriggerDisaster()
    {
        if (!disasterTrig)
        {
            disasterTrig = true;

            // プレイヤーの動きを停止
            Player.Instance.enabled = false;

            // 災害効果をトリガー
            DisasterEffect.Instance.TriggerDisaster();
        }
    }

    // 災害発生フェーズの処理
    private IEnumerator Disaster()
    {
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
    private void EndPrep()
    {
        // 準備フェーズを無効化
        if (PanelUI.Instance.prepPanel != null)
        {
            PanelUI.Instance.prepPanel.gameObject.SetActive(false);
        }
    }

    // 避難フェーズの処理
    private IEnumerator Evac()
    {
        // 避難フェーズのアニメーションを開始
        yield return StartCoroutine(PanelUI.Instance.EvacAnim());

        // 少し待機
        yield return new WaitForSeconds(1f);

        // オブジェクトからランダムに1つをアクティブにし、その位置を目的地として設定
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
        int index = UnityEngine.Random.Range(0, objectsToActivate.Length);
        activeObject = objectsToActivate[index];
        activeObject.SetActive(true);
        destPoint = activeObject.transform.position;

        UnityEngine.Debug.Log($"アクティブなオブジェクト: {activeObject.name}");

        // ナビゲーションパネルの表示
        if (index == 0)
        {
            StartCoroutine(PanelUI.Instance.NaviPanel1Anim());
        }
        else if (index == 1)
        {
            StartCoroutine(PanelUI.Instance.NaviPanel2Anim());
        }

        IsEvacPhase = true;

        float timer = evacTimer;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            yield return null;
        }

        // タイマーが切れた場合
        EndEvac();
    }

    // 避難フェーズの終了処理（タイムアウト）
    private void EndEvac()
    {
        UnityEngine.Debug.Log("避難フェーズ終了 - タイムアウト");

        // プレイヤーオブジェクトを消す
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }

        IsEvacPhase = false;
        restartGameLoop = true;
    }

    // プレイヤーが目的地に到達した時の処理
    public void ReachDest()
    {
        UnityEngine.Debug.Log("ReachDest メソッドが呼び出されました");

        // Demo_EndingSceneに移動
        SceneManager.LoadScene("Demo_Ending");
    }
}
