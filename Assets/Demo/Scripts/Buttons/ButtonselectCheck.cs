using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonselectCheck : MonoBehaviour
{
    // Playerスクリプトを参照
    [SerializeField] Player playerScripts;
    // PlayerStatusスクリプト参照
    [SerializeField] PlayerStatus playerStatus;
    public void StartButtonCheck()
    {
        Debug.Log("スタートボタンが選択されました");
        SceneController.LoadNextScene("β_DemoScene");
    }

    public void RestartButtonCheck()
    {
        Debug.Log("リスタートボタンが押されました");
        SceneController.LoadNextScene("β_DemoScene");
    }

    public void TitleButtonCheck()
    {
        Debug.Log("タイトルボタンが押されました");
        SceneController.LoadNextScene("StartScene");
    }

    public void EndButtonCheck()
    {
        Debug.Log("エンドボタンが選択されました");
        Application.Quit();
    }

    public void GoNextScen2()       // TimeLineでシーン遷移をさせるためのメソッド
    {
        SceneController.LoadNextScene("stage_3");
    }
    public void GoNextScene3()       // TimeLineでシーン遷移をさせるためのメソッド
    {
        SceneController.LoadNextScene("stage_4");
    }
    public void GoNextScene4()       // TimeLineでシーン遷移をさせるためのメソッド
    {
        SceneController.LoadNextScene("stage_5");
    }

    public void GoGameOverScene()
    {
        //プレイヤーを初期位置に戻すメソッド
        playerScripts.ResetPlayerPostion();
        // プレイヤーのステータスを初期に戻して、最初の通知を飛ばすシステム
        playerStatus.ResetPlayerStates();
    }
}
