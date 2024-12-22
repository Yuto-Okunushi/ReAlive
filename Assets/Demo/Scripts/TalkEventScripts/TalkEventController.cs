using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;　//追加


public class TalkEventController : MonoBehaviour
{
    [SerializeField] GameObject TalkEventPanel;     //イベントテキストのキャンバス
    [SerializeField] Text EventText;                //イベント内容を表示するテキスト
    [SerializeField] Text CharactorNameText;        //話しているキャラクターの名前
    string NowTalk;
    string FirstName;
    public bool isTalking = false;

    public int CharactorNameNumber = 2;
    public int TalkEventName = 5;

    //CSV関連
    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    private void Start()
    {
        //SCV関連
        csvFile = Resources.Load("TalkEventContrnts") as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isTalking)
        {
            //if文でもし次の要素に何か物があった時にしたい
            TalkManager();
        }
    }

    public void Objectactivetrue()
    {
        //talk中の判断
        isTalking = true;
        GameManager.SetIsTalking(isTalking);
        //オブジェクトの表示
        TalkEventPanel.SetActive(true);

    }

    public void Objectactivefalse()
    {
        //オブジェクトの非表示
        TalkEventPanel.SetActive(false);
        isTalking = false;
        isTalking =  GameManager.GetIsTalking();
    }

    public void SelectEvent1()
    {
        if(isTalking)
        {
            FirstName = csvData[CharactorNameNumber][0];
            NowTalk = csvData[TalkEventName][0];
            //キャラクターの名前を代入
            CharactorNameText.text = FirstName;
            //一番最初の会話内容を入れる
            EventText.text = NowTalk;
            
            //ゲームマネージャーにデータ受け渡し
            GameManager.SendCSVTalk(NowTalk);
            GameManager.SendCSVCharactorName(FirstName);
        }
    }

    //会話を次に進めるメソッド
    public void TalkManager()
    {
        // 境界チェック
        if (TalkEventName + 1 < csvData.Count && csvData[TalkEventName + 1].Length > 0)
        {
            TalkEventName++;
            NowTalk = csvData[TalkEventName][0];
            EventText.text = NowTalk;
        }
        else
        {
            Objectactivefalse(); // 次の要素がない場合は非表示にする
            isTalking = false;
            GameManager.SetIsTalking(isTalking);
        }
    }

}
