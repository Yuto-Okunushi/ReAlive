using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//ステージ3専用
public class TimeLineControll_4 : MonoBehaviour
{
    public GameObject[] objectsToActivate; // ランダムで設定するオブジェクト群


    [SerializeField] public PlayableDirector[] Timelines;       // 通常のイベントで使われるタイムライン
    [SerializeField] public PlayableDirector[] FlashBackTimelines;      // フラッシュバックの時に使うタイムライン
    [SerializeField] GameObject DistractionImage1;       // 目的地の通知をするイメージ
    [SerializeField] GameObject DistractionImage2;       // 目的地の通知をするイメージ

    public float earthquakeTime = 10.0f;
    public float time = 0.0f;
    private bool isCount = true;
    public bool isTimeline = false;
    public bool isMapShow = true;
    public int randomTimeLinenum;       // ランダムでタイムラインを再生させるための変数

    void Start()
    {
        earthquakeTime = Random.Range(30.0f, 40.0f);
    }

    public void Update()
    {
        if (isCount)
        {
            time += Time.deltaTime;
            if (time >= earthquakeTime)
            {
                Debug.Log("スクリプトが実行されました");
                EarthquakeTimeline();
                isCount = false;
            }
        }
    }

    public void EarthquakeTimeline() => Timelines[0].Play();
    public void FallRockTimeLine() => Timelines[1].Play();
    public void FlashBackTimeLine() => Timelines[2].Play();
    public void DeadTimeLine() => Timelines[3].Play();
    public void StageClear() => Timelines[4].Play();        // ステージクリア時のタイムラインを再生する

    public void FlugTure()
    {
        isTimeline = true;
        GameManager.SetIsTimeline(isTimeline);
    }

    public void FlugFalse()
    {
        isTimeline = false;
        GameManager.SetIsTimeline(isTimeline);
    }

    public void ShowMapFlag()
    {
        isMapShow = false;
        GameManager.SetIsMapShow(isMapShow);
    }

    public void destination() // ランダムに目的地を設定するシステム
    {
        // 全オブジェクトを非アクティブにする
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // ランダムに1つのオブジェクトをアクティブにする
        int randomIndex = Random.Range(0, objectsToActivate.Length);
        objectsToActivate[randomIndex].SetActive(true);

        // ランダムなオブジェクトに応じて通知イメージを表示し、2秒後に非表示にする
        if (randomIndex == 0)
        {
            DistractionImage1.SetActive(true);
            DistractionImage2.SetActive(false);
            StartCoroutine(HideImageAfterDelay(DistractionImage1, 6.0f));
        }
        else if (randomIndex == 1)
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(true);
            StartCoroutine(HideImageAfterDelay(DistractionImage2, 6.0f));
        }
        else
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(false);
        }
    }

    // 指定したイメージを一定時間後に非表示にするコルーチン
    private IEnumerator HideImageAfterDelay(GameObject image, float delay)
    {
        yield return new WaitForSeconds(delay);  // 指定秒数待機
        image.SetActive(false);                 // イメージを非表示
    }

    public void GoNextScene1()       // TimeLineでシーン遷移をさせるためのメソッド
    {
        SceneController.LoadNextScene("stage_4");
    }

    public void RamdomFlashBackTimeLine()       // ランダムにフラッシュバックのタイムラインを再生させるメソッド
    {
        randomTimeLinenum = Random.Range(0, 3);      // 0～2の数値をランダムに代入
        FlashBackTimelines[randomTimeLinenum].Play();       // 代入した数値に対応したタイムラインを再生
    }

    public void StopEarthquakeTimer()
    {
        isCount = false; // タイマーを無効化
    }

    public void EarthQuakeTimeReset()
    {
        earthquakeTime = Random.Range(30.0f, 40.0f);        // ランダムでもう一度時間を設定
        time = 0;                                           // 計測の時間を0に戻してまたカウントを始める
    }

}
