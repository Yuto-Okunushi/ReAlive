using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } // シングルトンのインスタンス

    public float baseWalkSpeed = 5f;  // 通常の移動速度
    public float baseRunSpeed = 10f;  // 走る時の移動速度
    public Camera playerCam;          // プレイヤーカメラの参照

    public float walkSpeed; // 現在の歩く速度
    public float runSpeed; // 現在の走る速度

    void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ゲームオブジェクトがシーン間で破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合はこのオブジェクトを破棄
        }
    }

    void Start()
    {
        // ゲーム開始時にマウスカーソルを消してロックする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 初期速度を設定
        walkSpeed = baseWalkSpeed;
        runSpeed = baseRunSpeed;
    }

    void Update()
    {
        // プレイヤー移動の正規化
        Vector3 moveDir = GetInput();
        Move(moveDir);
    }

    // 正規化されたプレイヤー移動の関数
    Vector3 GetInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);
        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize(); // 入力の正規化
        }

        // カメラの向きに基づいて移動方向を調整
        Vector3 forward = playerCam.transform.forward;
        Vector3 right = playerCam.transform.right;

        forward.y = 0f; // 上下の移動を無視
        right.y = 0f; // 上下の移動を無視

        forward.Normalize();
        right.Normalize();

        // カメラの向きに基づいて移動方向を決定
        Vector3 desiredMoveDir = forward * moveDir.z + right * moveDir.x;
        return desiredMoveDir;
    }

    // プレイヤーを移動させる関数
    void Move(Vector3 direction)
    {
        float currSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // 走るか歩くかを決定
        transform.Translate(direction * currSpeed * Time.deltaTime, Space.World); // プレイヤーを移動
    }
}
