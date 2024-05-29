using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 0.5f;

    void Update()
    {
        // �v���C���[�ړ��̐��K��
        Vector3 moveDirection = GetNormalizedInput();
        Move(moveDirection);

        // �v���C���[�̌������ړ������Ɍ�����
        if (moveDirection != Vector3.zero)
        {
            Turn(moveDirection);
        }
    }

    // ���K�����ꂽ�v���C���[�ړ��̊֐�
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

    // �v���C���[���ړ�������֐�
    void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    // �v���C���[�̌������ړ������Ɍ�����֐�
    void Turn(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, moveSpeed * turnSpeed * Time.deltaTime);
    }
}
