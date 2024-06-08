using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    private bool buttonSelected = false;
    public Button startButton; // 初期選択するボタンをInspectorで設定

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時に一度だけボタンを選択する
        SelectButton(startButton);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ボタン選択はここで行わない
    }

    // ボタンを選択するメソッド
    void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
