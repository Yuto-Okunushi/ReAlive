using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private bool isTimeCount = false;
    public float nowTime = 60f;     //現在の時間

    private void Awake()
    {
        timerText = GetComponent<Text>();       //テキストの取得
    }

    void Start()
    {
        isTimeCount = true;
    }

    void Update()
    {
        
    }

    public void TimerStartStop()
    {
        if (isTimeCount)
        {
            nowTime -= Time.deltaTime;  // 経過時間を減算

            // タイマーが0以下になったら停止
            if (nowTime <= 0)
            {
                isTimeCount = false;
                nowTime = 0;
            }

            // 秒に変換
            float seconds = nowTime % 60;

            // タイマーのテキストを更新
            timerText.text = seconds.ToString("00.00");
        }
    }
}
