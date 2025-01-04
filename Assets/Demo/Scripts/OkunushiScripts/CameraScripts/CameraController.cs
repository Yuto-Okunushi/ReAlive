using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度
    public Transform playerBody;         // プレイヤーの体（カメラを取り付けているオブジェクト）

    private float xRotation = 0f;        // カメラの上下方向の回転角度

    public bool isOpend = false;        //何かしらの他キャンバスが開かれているか
    public bool isTimeline = false;     //タイムラインの再生中か
    public bool isTalking = false;      //会話中かどうか

    void Start()
    {
        // カーソルを画面中央に固定して非表示にする
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isTimeline = GameManager.GetTimelineflug();
        isTalking = GameManager.GetIsTalking();
        isOpend = GameManager.GetIsOpend();

        if(!isTalking && !isTimeline && !isOpend)
        {
            // マウスの移動量を取得
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 上下方向の回転を更新し、角度を制限
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // カメラの上下方向の回転を適用
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // プレイヤーの水平方向の回転を適用
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}
