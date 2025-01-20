using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonselectCheck : MonoBehaviour
{
    // Playerスクリプトを参照
    [SerializeField] Player playerScripts;
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
        SceneController.LoadNextScene("Demo_Title");
    }

    public void EndButtonCheck()
    {
        Debug.Log("エンドボタンが選択されました");
        Application.Quit();
    }

    public void GoGameOverScene()
    {
        //プレイヤーを初期位置に戻すメソッド
        playerScripts.ResetPlayerPostion();
        // Sceneを再ロード
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneController.LoadNextScene("GameOverScene");
    }
}
