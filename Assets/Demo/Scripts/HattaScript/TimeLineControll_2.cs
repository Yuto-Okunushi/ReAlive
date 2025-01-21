using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// ステージ4専用

public class TimeLineControll_2 : MonoBehaviour
{
    public GameObject[] objectsToActivate; // ランダムで設定するオブジェクト群
    public GameObject[] specificLocations; // 特定の場所を管理する配列


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
        // 特定の場所をすべて非アクティブ化
        foreach (var location in specificLocations)
        {
            location.SetActive(false);
        }
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

    public void EarthquakeTimeline()
    {
        Timelines[0].Play();

        // 地震発生後に特定の場所をアクティブ化
        ActivateSpecificLocation(0); // 必要に応じて適切なインデックスを設定
    }
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
            StartCoroutine(HideImageAfterDelay(DistractionImage1, 2.0f));
        }
        else if (randomIndex == 1)
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(true);
            StartCoroutine(HideImageAfterDelay(DistractionImage2, 2.0f));
        }
        else
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(false);
        }
    }

    public void ActivateSpecificLocation(int index)
    {
        // 全ての特定の場所を非アクティブ化
        foreach (var location in specificLocations)
        {
            location.SetActive(false);
        }

        // 指定した場所をアクティブ化
        if (index >= 0 && index < specificLocations.Length)
        {
            specificLocations[index].SetActive(true);
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
        SceneController.LoadNextScene("AnyScene");
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

}
