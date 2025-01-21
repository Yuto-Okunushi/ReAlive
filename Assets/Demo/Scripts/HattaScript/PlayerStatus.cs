using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance = null;

    public float maxHydration = 100f; // �ő吅����
    public float maxStress = 100f;    // �ő�X�g���X��

    public float currHyd = 0;    // ���݂̐�����
    public float currStress = 0; // ���݂̃X�g���X��


    public float hydRatePerUnit = 0.005f;    // �����������i�ړ�����1���j�b�g������j
    public float stressRatePerUnit = 0.005f; // �X�g���X�������i�ړ�����1���j�b�g������j

    private Player player;     // �v���C���[�̎Q��
    private Vector3 lastPos;   // �v���C���[�̍Ō�̈ʒu

    private PostProcessVolume ppVolume; // �|�X�g�v���Z�X�{�����[���̎Q��
    private List<PostProcessEffectSettings> effects = new List<PostProcessEffectSettings>(); // ���ʃ��X�g

    public UnityEngine.UI.Image hydrationGauge; // �����Q�[�W��Image
    public UnityEngine.UI.Image stressGauge;    // �X�g���X�Q�[�W��Image

    //==���傪�ǉ��������=================================================================
    public int playerinitialmony = 3000;        //�v���C���[�̏�����
    public int playerHaveItem = 0;          //���݂̃v���C���[�A�C�e��������
    public int playerMaxHaveItem = 5;       //���E������
    [SerializeField] TalkEventController talkEventController;        // ��b�V�X�e���Q��
    [SerializeField] TimeLineControll timeLineControll;         // TimelineControll�Q��
    //=====================================================================================

    

    void Start()
    {
        // ������
        currHyd = maxHydration;
        currStress = maxStress;
        lastPos = player.transform.position;
        GameManager.SetPlayerMony(playerinitialmony);   //�ŏ��̏��������󂯓n��
        GameManager.SetPlayerHydration(currHyd);        //�ŏ��̐����ʂ��󂯓n��
        GameManager.SetPlayerStress(currStress);        //�ŏ��̃X�g���X�l���󂯓n��
        GameManager.SetTotalItem(playerHaveItem);       //�ŏ��ɏ��������󂯓n��


        // �|�X�g�v���Z�X���ʂ̎擾
        ppVolume = FindObjectOfType<PostProcessVolume>();
        if (ppVolume != null && ppVolume.profile != null)
        {
            foreach (var setting in ppVolume.profile.settings)
            {
                effects.Add(setting);
            }
        }

        // UI�̏�����
        UpdateHydrationGauge();
        UpdateStressGauge();

             
    }

    void Update()
    {
        // �X�e�[�^�X�X�V
        UpdateStats();
    }

    // �v���C���[�̈ʒu�Ɋ�Â��ăX�e�[�^�X���X�V
    void UpdateStats()
    {
        GameManager.GetPlayerHydration(currHyd);
        GameManager.GetPlayerStress(currStress);
        playerHaveItem = GameManager.GetItemTotal();
        float distance = Vector3.Distance(player.transform.position, lastPos); // �ړ������̌v�Z
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // �v���C���[�������Ă��邩���m�F
        ReduceHydration(distance, isRunning); // ����������
        ReduceStress(distance);               // �X�g���X������
        lastPos = player.transform.position;  // �Ō�̈ʒu���X�V
        playerinitialmony = GameManager.GetPlayerMony();        //�Q�[���}�l�[�W���[����f�[�^�̎󂯎��
    }

    // ���������������A�ړ����x�𒲐�
    void ReduceHydration(float distance, bool isRunning)
    {
        float rate = isRunning ? hydRatePerUnit * 1.2f : hydRatePerUnit; // �����Ă���ꍇ�͌�������1.2�{�ɂ���
        currHyd -= distance * rate; // �����ʂ�����
        UpdateHydrationGauge(); // �����Q�[�W���X�V
        if (currHyd <= 0)
        {
            // ������0�ɂȂ����ꍇ�̏���
            currHyd = 0;
            Die(); // �v���C���[�����S������
        }
        else if(currHyd >= maxHydration)     //�l���ő�l�ȏ�ɂ��Ȃ��Ƃ��̏���
        {
            currHyd = maxHydration;
        }
        else
        {
            // �����ʂɉ����Ĉړ����x�𒲐�
            float speedFactor = Mathf.Clamp01(currHyd / maxHydration);
            player.walkSpeed = player.baseWalkSpeed * speedFactor;
            player.runSpeed = player.baseRunSpeed * speedFactor;
        }
    }

    // �X�g���X�����������A���o���ʂ�K�p
    void ReduceStress(float distance)
    {
        currStress -= distance * stressRatePerUnit; // �X�g���X�ʂ�����
        UpdateStressGauge(); // �X�g���X�Q�[�W���X�V
        if (currStress <= 0)
        {
            // �X�g���X��0�ɂȂ����ꍇ�̏���
            currStress = 0;
        }
        else if(currStress >= maxStress)        //�ő�l�ȏ�ɂȂ������̏���
        {
            currStress = maxStress;
        }
        // �X�g���X�ʂɉ����Č��ʂ�K�p
        float effectFactor = 0.3f * (1 - Mathf.Clamp01(currStress / maxStress)); // ���ʂ�����Ɏ�߂�
        ApplyEffects(effectFactor);
    }

    // �v���C���[�����S�����鏈��
    void Die()
    {
        player.gameObject.SetActive(false);
    }

    // �|�X�g�v���Z�X���ʂ�K�p
    void ApplyEffects(float intensity)
    {
        foreach (var effect in effects)
        {
            if (effect is DepthOfField dof)
            {
                // Depth of Field�i�ڂ₯���ʁj�̓K�p
                dof.focusDistance.value = Mathf.Lerp(10f, 2f, intensity); // ���ʂ�����Ɏ�߂�
                dof.aperture.value = Mathf.Lerp(5.6f, 22f, intensity); // ���ʂ�����Ɏ�߂�
            }
            else if (effect is ColorGrading cg)
            {
                // Color Grading�i�Â����ʁj�̓K�p
                cg.postExposure.value = Mathf.Lerp(0f, -10f, intensity); // ���ʂ�����Ɏ�߂�
                cg.contrast.value = Mathf.Lerp(0f, 100f, intensity); // ���ʂ�����Ɏ�߂�
            }
            else if (effect is Vignette vg)
            {
                // Vignette�i���͈Â����ʁj�̓K�p
                vg.intensity.value = Mathf.Lerp(0f, 1f, intensity); // ���ʂ�����Ɏ�߂�
                vg.smoothness.value = Mathf.Lerp(0.2f, 0.5f, intensity); // ���ʂ�����Ɏ�߂�
            }
        }
    }

    // �����Q�[�W���X�V
    void UpdateHydrationGauge()
    {
        hydrationGauge.fillAmount = currHyd / maxHydration;
        GameManager.SetPlayerHydration(currHyd);
    }

    // �X�g���X�Q�[�W���X�V
    void UpdateStressGauge()
    {
        stressGauge.fillAmount = currStress / maxStress;
        GameManager.SetPlayerStress(currStress);

    }

    //�v���C���[��Ԃ������l�ɖ߂����\�b�h
    public void ResetPlayerStates()
    {
        Debug.Log("���̃X�N���v�g�͎��s����Ă��I");
        // ������
        currHyd = maxHydration;
        // �X�g���X�l
        currStress = maxStress;
        // ������
        playerinitialmony = 3000;
        // ���݂̃v���C���[�A�C�e��������
        playerHaveItem = 0;
        // �Q�[���̍ŏ��ɓ��ʒm������V�X�e�����Đ�
        talkEventController.notice();
        //�n�k���N���鎞�Ԃ����Z�b�g
        timeLineControll.EarthQuakeTimeReset();
    }
}
