using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimeLineControll : MonoBehaviour
{
    [SerializeField] private PlayableDirector[] Timelines;

    //地震発生時間
    public float earthquakeTime = 10.0f;
    //時間経過
    public float time = 0.0f;

    //カウントをするかどうかの判断
    private bool isCount = true;

    public bool isTimeline = false;

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
}
