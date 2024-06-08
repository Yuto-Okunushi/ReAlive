using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;  // 通常の移動速度
    public float runSpeed = 10f;  // 走る時の移動速度
    public Camera playerCamera;   // プレイヤーカメラの参照

    void Start()
    {
        // ゲーム開始時にマウスカーソルを消してロックする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // プレイヤー移動の正規化
        Vector3 moveDirection = GetNormalizedInput();
        Move(moveDirection);
    }

    // 正規化されたプレイヤー移動の関数
    Vector3 GetNormalizedInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // カメラの向きに基づいて移動方向を調整
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0f; // 上下の移動を無視
        right.y = 0f; // 上下の移動を無視

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveDirection.z + right * moveDirection.x;
        return desiredMoveDirection;
    }

    // プレイヤーを移動させる関数
    void Move(Vector3 direction)
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
    }
}
