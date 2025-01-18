using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;
    [SerializeField] private GameObject SignPanel;
    [SerializeField] private float displayDuration = 3.0f; // パネルを表示する時間（秒）
    [SerializeField] TimeLineControll TimeLineControll;

    public bool isOpend = false;        //何かしらの他キャンバスが開かれているか
    public bool isTimeline = false;     //タイムラインの再生中か
    public bool isTalking = false;      //会話中かどうか

    private void Start()
    {
        SignPanel.SetActive(false);
    }

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign1")
        {
            Debug.Log("標識1にぶつかりました");
            SendSignDate(0);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign2")
        {
            Debug.Log("標識2にぶつかりました");
            SendSignDate(1);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign3")
        {
            Debug.Log("標識3にぶつかりました");
            SendSignDate(2);
            StartCoroutine(DisplaySignPanel());
        }
        else if(other.gameObject.tag == "GameOverObject1")
        {
            isTimeline = true;
            GameManager.SetIsTimeline(isTimeline);
            //タイムライン再生メソッドを呼び出す
            TimeLineControll.FallRockTimeLine();
        }
        else if(other.gameObject.tag == "Goal1")     // ゴール１のオブジェクトに当たっている時
        {
            TimeLineControll.StageClear();
        }
        else if (other.gameObject.tag == "Goal2")    // ゴール２のオブジェクトに当たっている時
        {
            TimeLineControll.StageClear();
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
