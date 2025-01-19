using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    public static Player_2 Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public float baseWalkSpeed = 5f;  // �ʏ�̈ړ����x
    public float baseRunSpeed = 10f;  // ���鎞�̈ړ����x
    public Camera playerCam;          // �v���C���[�J�����̎Q��

    public float walkSpeed; // ���݂̕������x
    public float runSpeed; // ���݂̑��鑬�x
    public float jumpForce = 5f; // �W�����v��

    private Rigidbody rb; // Rigidbody�̎Q��
    private bool isGrounded; // �v���C���[���n�ʂɐڂ��Ă��邩�ǂ����̃t���O

    public bool isOpend = false;        //��������̑��L�����o�X���J����Ă��邩
    public bool isTimeline = false;     //�^�C�����C���̍Đ�����
    public bool isTalking = false;      //��b�����ǂ���

    //==���傪�ǉ��������==============================================================
    [SerializeField] Canvas shopcanvs;
    [SerializeField] GameObject shopobject;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject MapCam;
    //==================================================================================



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

        rb = GetComponent<Rigidbody>(); // Rigidbody�R���|�[�l���g���擾
    }

    void Start()
    {
        // �Q�[���J�n���Ƀ}�E�X�J�[�\���������ă��b�N����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.SetIsTimeline(isTimeline);
        // �������x��ݒ�
        walkSpeed = baseWalkSpeed;
        runSpeed = baseRunSpeed;
    }

    void Update()
    {
        isTimeline = GameManager.GetTimelineflug();
        isTalking = GameManager.GetIsTalking();
        isOpend = GameManager.GetIsOpend();

        // �v���C���[�ړ��̐��K��

        ObjectOpen();
        if (!isTalking && !isTimeline && !isOpend)
        {
            if (!shopcanvs.gameObject.activeSelf && !inventory.gameObject.activeSelf && !isTimeline)
            {
                Vector3 moveDir = GetInput();
                Move(moveDir);
            }

            // �W�����v�̓��͂��`�F�b�N
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {

        }
    }

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

        // �J�����̌����Ɋ�Â��Ĉړ����������肵���]
        Vector3 desiredMoveDir = -(forward * moveDir.z + right * moveDir.x);
        return desiredMoveDir;
    }


    // �v���C���[���ړ�������֐�
    void Move(Vector3 direction)
    {
        float currSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; // ���邩������������
        transform.Translate(direction * currSpeed * Time.deltaTime, Space.World); // �v���C���[���ړ�
    }

    // �W�����v�̏���
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // �n�ʂɐڐG�����Ƃ��̏���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject == shopobject)
        {
            isOpend = true;

            Openshopcanvs();
        }


    }

    // �I�u�W�F�N�g�Ƃ̐ڐG�����o���鏈��
    void OnTriggerEnter(Collider other)
    {

    }

    //�V���b�v�L�����o�X��\�������鏈��
    public void Openshopcanvs()
    {
        shopcanvs.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UnityEngine.Debug.Log("Shop canvas opened");
    }


    //�C���x���g���L�����o�X��\�������鏈��
    void ObjectOpen()
    {
        if (!shopcanvs.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            isOpend = true;
            bool isActive = !inventory.gameObject.activeSelf;
            inventory.gameObject.SetActive(isActive);
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void OpenMap()
    {

    }
}
