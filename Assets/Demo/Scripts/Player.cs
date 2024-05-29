using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 0.5f;

    void Update()
    {
        // プレイヤー移動の正規化
        Vector3 moveDirection = GetNormalizedInput();
        Move(moveDirection);

        // プレイヤーの向きを移動方向に向ける
        if (moveDirection != Vector3.zero)
        {
            Turn(moveDirection);
        }
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

        return moveDirection;
    }

    // プレイヤーを移動させる関数
    void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    // プレイヤーの向きを移動方向に向ける関数
    void Turn(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * turnSpeed * Time.deltaTime);
    }
}
