using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonselectCheck : MonoBehaviour
{

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
        SceneController.LoadNextScene("GameOverScene");
    }
}
