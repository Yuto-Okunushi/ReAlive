using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    public static PanelUI Instance { get; private set; } // シングルトンのインスタンス

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
        transform.rotation = Quaternion.Euler(90, 0, 0);

        // パネルのアニメーションを開始
        StartCoroutine(PanelAnim());
    }

    public static IEnumerator PanelAnim()
    {
        // 0.5秒待機
        yield return new WaitForSeconds(0.5f);

        // パネルを0.7秒かけて回転させる（90度から0度へ）
        yield return Instance.transform.DORotate(new Vector3(0, 0, 0), 0.7f).WaitForCompletion();

        // 1秒待機
        yield return new WaitForSeconds(1f);

        // パネルを0.7秒かけて回転させる（0度から90度へ戻す）
        yield return Instance.transform.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();
    }
}
