using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonselectCheck : MonoBehaviour
{

    public void StartButtonCheck()
    {
        Debug.Log("スタートボタンが選択されました");
        SceneController.LoadNextScene("Demo_Scenes");
    }

    public void RestartButtonCheck()
    {
        Debug.Log("リスタートボタンが押されました");
        SceneController.LoadNextScene("指定するシーン");
    }

    public void EndButtonCheck()
    {
        Debug.Log("エンドボタンが選択されました");
        Application.Quit();
    }
}
