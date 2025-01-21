using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ステージ2専用

public class PlayerCollision_2 : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;
    [SerializeField] private GameObject SignPanel;
    [SerializeField] private float displayDuration = 3.0f; // パネルを表示する時間（秒）
    [SerializeField] TimeLineControll_3 TimeLineControll_3;
    [SerializeField] private UnityEngine.UI.Text signCollectionText; // UI テキスト

    public bool isOpend = false;        // 何かしらの他キャンバスが開かれているか
    public bool isTimeline = false;     // タイムラインの再生中か
    public bool isTalking = false;      // 会話中かどうか

    private bool[] collectedSigns;      // 標識収集状態を追跡
    private int totalSigns;             // 標識の総数
    private int collectedCount = 0;     // 収集済みの標識数

    private void Start()
    {
        SignPanel.SetActive(false);
        totalSigns = signDate.Length;
        collectedSigns = new bool[totalSigns];
        UpdateSignCollectionUI(); // 初期状態を更新

        // 地震タイマーを初期停止
        TimeLineControll_3.StopEarthquakeTimer();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign1")
        {
            Debug.Log("標識1にぶつかりました");
            SendSignDate(0);
            CollectSign(0);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign2")
        {
            Debug.Log("標識2にぶつかりました");
            SendSignDate(1);
            CollectSign(1);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign3")
        {
            Debug.Log("標識3にぶつかりました");
            SendSignDate(2);
            CollectSign(2);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign4")
        {
            Debug.Log("標識4にぶつかりました");
            SendSignDate(3);
            CollectSign(3);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "GameOverObject1")
        {
            isTimeline = true;
            GameManager.SetIsTimeline(isTimeline);
            // タイムライン再生メソッドを呼び出す
            TimeLineControll_3.FallRockTimeLine();
        }
        else if (other.gameObject.tag == "Goal1")     // ゴール1のオブジェクトに当たっている時
        {
            TimeLineControll_3.StageClear();
        }
        else if (other.gameObject.tag == "Goal2")    // ゴール2のオブジェクトに当たっている時
        {
            TimeLineControll_3.StageClear();
        }
    }

    private void CollectSign(int index)
    {
        if (index >= 0 && index < collectedSigns.Length && !collectedSigns[index])
        {
            collectedSigns[index] = true; // 標識を収集済みに設定
            collectedCount++; // 収集数を更新
            UpdateSignCollectionUI(); // UI を更新

            SendSignDate(index);
            StartCoroutine(DisplaySignPanel());

            // すべての標識が収集されたかチェック
            if (AllSignsCollected())
            {
                StartCoroutine(DelayedEarthquake());
            }
        }
    }

    // 地震を遅らせて発生させるコルーチン
    private IEnumerator DelayedEarthquake()
    {
        float delayTime = 1.0f; // 遅延時間（秒）
        yield return new WaitForSeconds(delayTime); // 指定秒数待機

        TimeLineControll_3.StopEarthquakeTimer(); // 地震タイマーを停止
        TimeLineControll_3.EarthquakeTimeline();  // 地震を発生
    }

    private bool AllSignsCollected()
    {
        return collectedCount == totalSigns;
    }

    private void UpdateSignCollectionUI()
    {
        if (signCollectionText != null)
        {
            signCollectionText.text = $"{collectedCount} / {totalSigns}"; // UI テキストを更新
        }
    }

    private void SendSignDate(int index)
    {
        if (index >= 0 && index < signDate.Length)
        {
            GameManager.SetSignDate(signDate[index]);
        }
        else
        {
            Debug.LogError("Invalid index for signDate array");
        }
    }

    private IEnumerator DisplaySignPanel()
    {
        SignPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        SignPanel.SetActive(false);
    }
}
