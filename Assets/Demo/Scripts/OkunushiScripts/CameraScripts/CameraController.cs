using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // �}�E�X���x
    public Transform playerBody;         // �v���C���[�̑́i�J���������t���Ă���I�u�W�F�N�g�j

    private float xRotation = 0f;        // �J�����̏㉺�����̉�]�p�x

    public bool isOpend = false;        //��������̑��L�����o�X���J����Ă��邩
    public bool isTimeline = false;     //�^�C�����C���̍Đ�����
    public bool isTalking = false;      //��b�����ǂ���

    void Start()
    {
        // �J�[�\������ʒ����ɌŒ肵�Ĕ�\���ɂ���
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isTimeline = GameManager.GetTimelineflug();
        isTalking = GameManager.GetIsTalking();
        isOpend = GameManager.GetIsOpend();

        if(!isTalking && !isTimeline && !isOpend)
        {
            // �}�E�X�̈ړ��ʂ��擾
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // �㉺�����̉�]���X�V���A�p�x�𐧌�
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // �J�����̏㉺�����̉�]��K�p
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // �v���C���[�̐��������̉�]��K�p
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}
