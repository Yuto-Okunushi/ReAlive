using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TalkEventController : MonoBehaviour
{
    // 会話オブジェクト
    [SerializeField] GameObject FirstTalk1;
    [SerializeField] GameObject FirstTalk2;
    [SerializeField] GameObject FirstTalk3;
    [SerializeField] TimeLineControll timeLineControll;     // タイムラインコントローラーのメソッドを呼び出すため

    // 通知音
    [SerializeField] AudioClip noticeClip;
    private AudioSource audioSource;

    // スマホUIのアニメーション
    [SerializeField] Animator StartSmartPhoneUI;

    // 会話イベント用UI
    [SerializeField] GameObject TalkEventPanel;
    [SerializeField] Text EventText;
    [SerializeField] Text CharactorNameText;

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
            timeLineControll.destination();     // 目的地のImageを表示するメソッド
        }
    }

    public void notice()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            isNoticeActive = true;
        }
    }

    public void FirstTalkControll()
    {
        if (isCoroutineRunning) return;
        talkStep++;
        switch (talkStep)
        {
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
                break;
        }
    }

    private IEnumerator ActivateFirstTalk2()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(1f);
        isCoroutineRunning = false;
    }
}
