using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    public static PanelUI Instance { get; private set; } // シングルトンのインスタンス

    public Transform prepPanel; // 準備フェーズのパネル
    public Transform evacPanel; // 避難フェーズのパネル
    public Transform naviPanel_1; // ナビゲーションパネル1
    public Transform naviPanel_2; // ナビゲーションパネル2

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

        // DOTweenの初期化
        DOTween.Init();
    }

    private void Start()
    {
        // パネルの初期回転角度を設定
        InitializePanel(prepPanel);
        InitializePanel(evacPanel);
        InitializePanel(naviPanel_1);
        InitializePanel(naviPanel_2);
    }

    private void InitializePanel(Transform panel)
    {
        if (panel != null)
        {
            panel.rotation = Quaternion.Euler(90, 0, 0);
            panel.gameObject.SetActive(false); // 初期状態ではパネルを非表示にする
        }
    }

    public IEnumerator PrepAnim()
    {
        if (prepPanel == null) yield break;

        // パネルを表示
        prepPanel.gameObject.SetActive(true);

        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        // パネルを0.7秒かけて回転させる（90度から0度へ）
        yield return prepPanel.DORotate(Vector3.zero, 0.7f).WaitForCompletion();

        // 1秒待機
        yield return new WaitForSeconds(1f);

        // パネルを0.7秒かけて回転させる（0度から90度へ戻す）
        yield return prepPanel.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // パネルを非表示にする
        prepPanel.gameObject.SetActive(false);
    }

    public IEnumerator EvacAnim()
    {
        if (evacPanel == null) yield break;

        // パネルを表示
        evacPanel.gameObject.SetActive(true);

        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        // パネルを0.7秒かけて回転させる（90度から0度へ）
        yield return evacPanel.DORotate(Vector3.zero, 0.7f).WaitForCompletion();

        // 1秒待機
        yield return new WaitForSeconds(1f);

        // パネルを0.7秒かけて回転させる（0度から90度へ戻す）
        yield return evacPanel.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // パネルを非表示にする
        evacPanel.gameObject.SetActive(false);
    }

    public IEnumerator NaviPanel1Anim()
    {
        if (naviPanel_1 == null) yield break;

        // パネルを表示
        naviPanel_1.gameObject.SetActive(true);

        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        // パネルを0.7秒かけて回転させる（90度から0度へ）
        yield return naviPanel_1.DORotate(Vector3.zero, 0.7f).WaitForCompletion();

        // 1秒待機
        yield return new WaitForSeconds(1f);

        // パネルを0.7秒かけて回転させる（0度から90度へ戻す）
        yield return naviPanel_1.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // パネルを非表示にする
        naviPanel_1.gameObject.SetActive(false);
    }

    public IEnumerator NaviPanel2Anim()
    {
        if (naviPanel_2 == null) yield break;

        // パネルを表示
        naviPanel_2.gameObject.SetActive(true);

        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        // パネルを0.7秒かけて回転させる（90度から0度へ）
        yield return naviPanel_2.DORotate(Vector3.zero, 0.7f).WaitForCompletion();

        // 1秒待機
        yield return new WaitForSeconds(1f);

        // パネルを0.7秒かけて回転させる（0度から90度へ戻す）
        yield return naviPanel_2.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // パネルを非表示にする
        naviPanel_2.gameObject.SetActive(false);
    }
}
