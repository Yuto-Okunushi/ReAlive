using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;  // �ʏ�̈ړ����x
    public float runSpeed = 10f;  // ���鎞�̈ړ����x
    public Camera playerCamera;   // �v���C���[�J�����̎Q��

    void Start()
    {
        // �Q�[���J�n���Ƀ}�E�X�J�[�\���������ă��b�N����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �v���C���[�ړ��̐��K��
        Vector3 moveDirection = GetNormalizedInput();
        Move(moveDirection);
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

        // �J�����̌����Ɋ�Â��Ĉړ������𒲐�
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0f; // �㉺�̈ړ��𖳎�
        right.y = 0f; // �㉺�̈ړ��𖳎�

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * moveDirection.z + right * moveDirection.x;
        return desiredMoveDirection;
    }

    // �v���C���[���ړ�������֐�
    void Move(Vector3 direction)
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
    }
}
