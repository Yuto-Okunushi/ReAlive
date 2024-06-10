using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public float baseWalkSpeed = 5f;  // �ʏ�̈ړ����x
    public float baseRunSpeed = 10f;  // ���鎞�̈ړ����x
    public Camera playerCam;          // �v���C���[�J�����̎Q��

    public float walkSpeed; // ���݂̕������x
    public float runSpeed; // ���݂̑��鑬�x

    void Awake()
    {
        // �V���O���g���̃C���X�^���X��ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �Q�[���I�u�W�F�N�g���V�[���ԂŔj������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���ɃC���X�^���X�����݂���ꍇ�͂��̃I�u�W�F�N�g��j��
        }
    }

    void Start()
    {
        // �Q�[���J�n���Ƀ}�E�X�J�[�\���������ă��b�N����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // �������x��ݒ�
        walkSpeed = baseWalkSpeed;
        runSpeed = baseRunSpeed;
    }

    void Update()
    {
        // �v���C���[�ړ��̐��K��
        Vector3 moveDir = GetInput();
        Move(moveDir);
    }

    // ���K�����ꂽ�v���C���[�ړ��̊֐�
    Vector3 GetInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);
        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize(); // ���͂̐��K��
        }

        // �J�����̌����Ɋ�Â��Ĉړ������𒲐�
        Vector3 forward = playerCam.transform.forward;
        Vector3 right = playerCam.transform.right;

        forward.y = 0f; // �㉺�̈ړ��𖳎�
        right.y = 0f; // �㉺�̈ړ��𖳎�

        forward.Normalize();
        right.Normalize();

        // �J�����̌����Ɋ�Â��Ĉړ�����������
        Vector3 desiredMoveDir = forward * moveDir.z + right * moveDir.x;
        return desiredMoveDir;
    }

    // �v���C���[���ړ�������֐�
    void Move(Vector3 direction)
    {
        float currSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // ���邩������������
        transform.Translate(direction * currSpeed * Time.deltaTime, Space.World); // �v���C���[���ړ�
    }
}
