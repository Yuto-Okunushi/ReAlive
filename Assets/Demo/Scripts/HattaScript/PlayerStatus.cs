using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    public float maxHydration = 100f; // �ő吅����
    public float maxStress = 100f;    // �ő�X�g���X��

    private float currHyd;    // ���݂̐�����
    private float currStress; // ���݂̃X�g���X��

    public float hydRatePerUnit = 0.005f;    // �����������i�ړ�����1���j�b�g������j
    public float stressRatePerUnit = 0.005f; // �X�g���X�������i�ړ�����1���j�b�g������j

    private Player player;     // �v���C���[�̎Q��
    private Vector3 lastPos;   // �v���C���[�̍Ō�̈ʒu

    private PostProcessVolume ppVolume; // �|�X�g�v���Z�X�{�����[���̎Q��
    private List<PostProcessEffectSettings> effects = new List<PostProcessEffectSettings>(); // ���ʃ��X�g

    public UnityEngine.UI.Image hydrationGauge; // �����Q�[�W��Image
    public UnityEngine.UI.Image stressGauge;    // �X�g���X�Q�[�W��Image

    void Awake()
    {
        // �V���O���g���̐ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ������
        player = Player.Instance;
        currHyd = maxHydration;
        currStress = maxStress;
        lastPos = player.transform.position;

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
        float distance = Vector3.Distance(player.transform.position, lastPos); // �ړ������̌v�Z
        if (distance > 0)
        {
            // �ړ����������ꍇ�̂ݍX�V
            bool isRunning = Input.GetKey(KeyCode.LeftShift); // �v���C���[�������Ă��邩���m�F
            ReduceHydration(distance, isRunning); // ����������
            ReduceStress(distance);               // �X�g���X������
            lastPos = player.transform.position;  // �Ō�̈ʒu���X�V
        }
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
            Die(); // �v���C���[�����S������
        }
        else
        {
            // �X�g���X�ʂɉ����Č��ʂ�K�p
            float effectFactor = 0.3f * (1 - Mathf.Clamp01(currStress / maxStress)); // ���ʂ�����Ɏ�߂�
            ApplyEffects(effectFactor);
        }
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
                dof.focusDistance.value = Mathf.Lerp(10f, 7f, intensity); // ���ʂ�����Ɏ�߂�
                dof.aperture.value = Mathf.Lerp(5.6f, 8f, intensity); // ���ʂ�����Ɏ�߂�
            }
            else if (effect is ColorGrading cg)
            {
                // Color Grading�i�Â����ʁj�̓K�p
                cg.postExposure.value = Mathf.Lerp(0f, -3f, intensity); // ���ʂ�����Ɏ�߂�
                cg.contrast.value = Mathf.Lerp(0f, 30f, intensity); // ���ʂ�����Ɏ�߂�
            }
            else if (effect is Vignette vg)
            {
                // Vignette�i���͈Â����ʁj�̓K�p
                vg.intensity.value = Mathf.Lerp(0f, 0.3f, intensity); // ���ʂ�����Ɏ�߂�
                vg.smoothness.value = Mathf.Lerp(0.2f, 0.25f, intensity); // ���ʂ�����Ɏ�߂�
            }
        }
    }

    // �����Q�[�W���X�V
    void UpdateHydrationGauge()
    {
        hydrationGauge.fillAmount = currHyd / maxHydration;
    }

    // �X�g���X�Q�[�W���X�V
    void UpdateStressGauge()
    {
        stressGauge.fillAmount = currStress / maxStress;
    }
}
