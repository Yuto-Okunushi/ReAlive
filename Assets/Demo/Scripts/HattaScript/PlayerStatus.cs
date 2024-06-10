using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public float maxHydration = 100f; // �ő吅��
    public float maxStress = 100f; // �ő�X�g���X

    private float currHyd; // ���݂̐���
    private float currStress; // ���݂̃X�g���X

    public float hydRatePerUnit = 0.1f; // �ړ�����1���j�b�g������̐���������
    public float stressRatePerUnit = 0.1f; // �ړ�����1���j�b�g������̃X�g���X������

    private Player player; // �v���C���[�̎Q��
    private Vector3 lastPos; // �v���C���[�̍Ō�̈ʒu

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
        player = Player.Instance; // �v���C���[�̃C���X�^���X���擾
        currHyd = maxHydration; // �������ő�l�ɐݒ�
        currStress = maxStress; // �X�g���X���ő�l�ɐݒ�
        lastPos = player.transform.position; // �ŏ��̈ʒu��ݒ�
        UnityEngine.Debug.Log("�����������B���݂̐�����: " + currHyd + ", ���݂̃X�g���X��: " + currStress); // ������Ԃ����O�ɏo��
    }

    void Update()
    {
        UpdateStatus(); // �X�e�[�^�X�̍X�V
    }

    // �v���C���[�̈ʒu�Ɋ�Â��ăX�e�[�^�X���X�V
    void UpdateStatus()
    {
        float distance = Vector3.Distance(player.transform.position, lastPos); // �ړ��������v�Z
        UnityEngine.Debug.Log("�ړ�����: " + distance); // �f�o�b�O���O�Ɉړ��������o��
        if (distance > 0)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // �v���C���[�������Ă��邩�ǂ������m�F
            UnityEngine.Debug.Log("�����Ă���: " + isRunning); // �f�o�b�O���O�Ƀv���C���[�������Ă��邩�ǂ������o��
            DecreaseHyd(distance, isRunning); // �ړ������ɉ����Đ���������
            DecreaseStress(distance); // �ړ������ɉ����ăX�g���X������
            lastPos = player.transform.position; // �Ō�̈ʒu���X�V
        }
    }

    // ���������������A���x�𒲐�
    void DecreaseHyd(float distance, bool isRunning)
    {
        float rate = isRunning ? hydRatePerUnit * 2 : hydRatePerUnit; // �����Ă���ꍇ�͌�������{�ɂ���
        currHyd -= distance * rate;
        UnityEngine.Debug.Log("���݂̐�����: " + currHyd); // �f�o�b�O���O�Ɍ��݂̐������o��
        if (currHyd <= 0)
        {
            currHyd = 0;
            PlayerDie(); // ������0�ɂȂ�����v���C���[������
        }
        else
        {
            float speedFactor = Mathf.Clamp01(currHyd / maxHydration); // �����ɉ����đ��x�𒲐�
            player.walkSpeed = player.baseWalkSpeed * speedFactor;
            player.runSpeed = player.baseRunSpeed * speedFactor;
        }
    }

    // �X�g���X�����������A��ʂ̂ڂ₯�𒲐�
    void DecreaseStress(float distance)
    {
        currStress -= distance * stressRatePerUnit;
        UnityEngine.Debug.Log("���݂̃X�g���X��: " + currStress); // �f�o�b�O���O�Ɍ��݂̃X�g���X���o��
        if (currStress <= 0)
        {
            currStress = 0;
            PlayerDie(); // �X�g���X��0�ɂȂ�����v���C���[������
        }
        else
        {
            float blurFactor = 1 - Mathf.Clamp01(currStress / maxStress); // �X�g���X�ɉ����Ăڂ₯�𒲐�
            ApplyBlur(blurFactor);
        }
    }

    // �v���C���[�����ʏ���
    void PlayerDie()
    {
        player.gameObject.SetActive(false); // �v���C���[���\���ɂ���
        UnityEngine.Debug.Log("�v���C���[�����S���܂���"); // �f�o�b�O���O�Ƀv���C���[�̎��S���o��
        // ���̃Q�[���I�[�o�[����
    }

    // �ڂ₯���ʂ�K�p
    void ApplyBlur(float intensity)
    {
        // �����ɂڂ₯���ʂ�K�p����R�[�h��ǉ�
        // intensity ���g�p���Ăڂ₯���ʂ𒲐����܂�
    }
}
