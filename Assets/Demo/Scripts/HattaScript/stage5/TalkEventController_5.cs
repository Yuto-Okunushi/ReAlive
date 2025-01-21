using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TalkEventController_5 : MonoBehaviour
{
    // 会話オブジェクト
    [SerializeField] GameObject FirstTalk1;
    [SerializeField] GameObject FirstTalk2;
    [SerializeField] GameObject FirstTalk3;
    [SerializeField] TimeLineControll_5 timeLineControll_5;     // タイムラインコントローラーのメソッドを呼び出すため

    // 通知音
    [SerializeField] AudioClip noticeClip;
    private AudioSource audioSource;

    // スマホUIのアニメーション
    [SerializeField] Animator StartSmartPhoneUI;

    // 会話イベント用UI
    [SerializeField] GameObject TalkEventPanel;
    [SerializeField] Text EventText;
    [SerializeField] Text CharactorNameText;
    [SerializeField] GameObject EndingCanvas;
    [SerializeField] Text EndingText;

    // 現在の会話内容とキャラクター名
    string NowTalk;
    string FirstName;
    public bool isTalking = false;

    // CSVのデータインデックス
    public int CharactorNameNumber = 2;
    public int TalkEventName = 5;

    // CSVデータ
    private TextAsset csvFile;
    private List<string[]> csvData = new List<string[]>();

    // 通知音のフラグと会話進行ステップ
    private bool isNoticeActive = false;
    private int talkStep = 0;
    private bool isCoroutineRunning = false;

    private void Start()
    {
        // AudioSourceの初期化
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = noticeClip;
        audioSource.loop = true;

        // CSVファイルの読み込み
        csvFile = Resources.Load("TalkEventContrnts") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
        }

        // ゲーム開始時に通知音と最初の会話を開始
        notice();
    }

    private void Update()
    {
        if (isNoticeActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
            {
                audioSource.Stop();
                StartSmartPhoneUI.SetBool("IsStarted", true);
                isNoticeActive = false;
                FirstTalkControll();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0) && isTalking)
        {
            TalkManager();
        }
        else if (Input.GetMouseButtonDown(0) && !isTalking && !isNoticeActive)
        {
            FirstTalkControll();
        }
    }

    public void Objectactivetrue()
    {
        isTalking = true;
        GameManager.SetIsTalking(isTalking);
        TalkEventPanel.SetActive(true);
    }

    public void Objectactivefalse()
    {
        TalkEventPanel.SetActive(false);
        isTalking = false;
        GameManager.SetIsTalking(isTalking);
    }

    public void SelectEvent1()
    {
        if (isTalking)
        {
            FirstName = csvData[CharactorNameNumber][0];
            NowTalk = csvData[TalkEventName][0];
            CharactorNameText.text = FirstName;
            EventText.text = NowTalk;
            GameManager.SendCSVTalk(NowTalk);
            GameManager.SendCSVCharactorName(FirstName);
        }
    }

    public void TalkManager()
    {
        if (TalkEventName + 1 < csvData.Count && csvData[TalkEventName + 1].Length > 0)
        {
            TalkEventName++;
            NowTalk = csvData[TalkEventName][0];
            EventText.text = NowTalk;
        }
        else
        {
            Objectactivefalse();
            timeLineControll_5.destination();     // 目的地のImageを表示するメソッド
        }
    }

    public void notice()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            isNoticeActive = true;
            talkStep = 0;       // 会話の進行状況を0に戻す
        }
    }

    public void FirstTalkControll()
    {
        if (isCoroutineRunning) return;
        talkStep++;
        switch (talkStep)
        {
            case 1:
                StartSmartPhoneUI.SetBool("IsStarted", true); // アニメーションを開始
                StartSmartPhoneUI.SetBool("IsClosed", false);

                break;
            case 2:
                FirstTalk1.SetActive(true);
                StartCoroutine(ActivateFirstTalk2());
                break;
            case 3:
                FirstTalk2.SetActive(true);
                break;
            case 4:
                FirstTalk3.SetActive(true);
                break;
            case 5:
                StartSmartPhoneUI.SetBool("IsStarted", false);
                StartSmartPhoneUI.SetBool("IsClosed", true);
                // もう一度一から会話を始められるようもとに戻す
                FirstTalk1.SetActive(false);
                FirstTalk2.SetActive(false);
                FirstTalk3.SetActive(false);
                break;
        }
    }

    private IEnumerator ActivateFirstTalk2()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(1f);
        isCoroutineRunning = false;
    }

    public void EndingTalkEvent()
    {
        EndingCanvas.SetActive(true);
        // 現在の行番号と列番号（固定で10列目を使用）
        int currentRow = 5;
        int columnIndex = 9; // 10列目（0始まりなので9）

        // Endingイベントを開始
        StartCoroutine(EndingEventCoroutine(currentRow, columnIndex));
    }

    private IEnumerator EndingEventCoroutine(int startRow, int columnIndex)
    {
        int rowCount = csvData.Count; // CSVの行数を取得
        Objectactivetrue(); // 会話イベント用UIを表示

        for (int row = startRow; row < rowCount; row++)
        {
            // CSVデータが空でないか確認
            if (columnIndex < csvData[row].Length && !string.IsNullOrEmpty(csvData[row][columnIndex]))
            {
                // 現在の行の 10 列目の内容を取得
                NowTalk = csvData[row][columnIndex];
                EventText.text = NowTalk; // UIに反映

                // ユーザーの入力待ち
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
            }
            else
            {
                SceneController.LoadNextScene("StartScene");
            }
        }
    }
}
