using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimeLineControll : MonoBehaviour
{
    [SerializeField] public PlayableDirector[] Timelines;

    // 地震発生時間
    public float earthquakeTime = 10.0f;
    // 時間経過
    public float time = 0.0f;

    // カウントをするかどうかの判断
    private bool isCount = true;

    // TimeLine再生中か確認するフラグ
    public bool isTimeline = false;

    // Map表示が可能かのフラグ
    public bool isMapShow = true;

    public void Update()
    {
        if(isCount)
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
        //０番目のタイムラインを再生
        Timelines[0].Play();
    }

    public void FallRockTimeLine()
    {
        //１番目のタイムラインを再生
        Timelines[1].Play();
    }

    public void FlashBackTimeLine()
    {
        //１番目のタイムラインを再生
        Timelines[2].Play();
    }

    public void DeadTimeLine()
    {
        //１番目のタイムラインを再生
        Timelines[3].Play();
    }

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

    public void ShowMapFlag()       // マップ表示を無効にするフラグ
    {
        isMapShow = false;
        GameManager.SetIsMapShow(isMapShow);        // GameManagerにフラグのデータを送信
    }
}
