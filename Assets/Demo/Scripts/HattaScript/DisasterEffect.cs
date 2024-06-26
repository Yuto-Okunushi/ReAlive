using UnityEngine;
using Cinemachine;

public class DisasterEffect : MonoBehaviour
{
    public static DisasterEffect Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public float duration = 30.0f; // �ЊQ�̎������Ԃ�����
    public float initialMagnitude = 1.0f; // �����̗h��̑傫��
    public float finalMagnitude = 0.1f; // �ŏI�I�ȗh��̑傫��

    private float elapsed = 0.0f; // �o�ߎ���
    private bool isShaking = false; // �ЊQ�����������ǂ����̃t���O

    private CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    private CinemachineBasicMultiChannelPerlin noise; // Cinemachine Noise

    void Awake()
    {
        // �V���O���g���̃C���X�^���X��ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.Debug.Log("DisasterEffect �C���X�^���X���ݒ肳��܂���");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Cinemachine Virtual Camera �̎擾
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            UnityEngine.Debug.Log("CinemachineVirtualCamera ��������܂���: " + virtualCamera.name);
        }
        else
        {
            UnityEngine.Debug.LogError("CinemachineVirtualCamera ��������܂���B");
        }

        // ���������ɗh����~�߂�
        StopDisaster();
    }

    void Update()
    {
        // �ЊQ���������̏ꍇ
        if (isShaking && noise != null)
        {
            // �������ԓ��ł���Ηh����v�Z
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float currentMagnitude = Mathf.Lerp(initialMagnitude, finalMagnitude, elapsed / duration);
                noise.m_AmplitudeGain = currentMagnitude;
                noise.m_FrequencyGain = currentMagnitude;
                UnityEngine.Debug.Log("�J�������h��Ă��܂�: Amplitude=" + noise.m_AmplitudeGain + ", Frequency=" + noise.m_FrequencyGain);
            }
            // �������Ԃ𒴂����猳�̈ʒu�ɖ߂�
            else
            {
                StopDisaster();
            }
        }
    }

    // �ЊQ���J�n���郁�\�b�h
    public void TriggerDisaster()
    {
        elapsed = 0.0f;
        isShaking = true;
        UnityEngine.Debug.Log("�ЊQ���g���K�[����܂���");
    }

    // �ЊQ���~���郁�\�b�h
    public void StopDisaster()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
            UnityEngine.Debug.Log("�J�����̗h�ꂪ�I�����܂���");
        }
        isShaking = false;
    }
}
