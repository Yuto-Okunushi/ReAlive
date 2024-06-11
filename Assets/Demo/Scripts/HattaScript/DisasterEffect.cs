using UnityEngine;

public class DisasterEffect : MonoBehaviour
{
    public static DisasterEffect Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public float duration = 1.0f; // �ЊQ�̎�������
    public float magnitude = 0.1f; // �ЊQ�̗h��̑傫��

    private Vector3 originalPosition; // ���̈ʒu��ۑ�
    private float elapsed = 0.0f; // �o�ߎ���
    private bool isShaking = false; // �ЊQ�����������ǂ����̃t���O

    void Awake()
    {
        // �V���O���g���̃C���X�^���X��ݒ�
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
        // �����ʒu��ۑ�
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // �ЊQ���������̏ꍇ
        if (isShaking)
        {
            // �������ԓ��ł���Ηh����v�Z
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            }
            // �������Ԃ𒴂����猳�̈ʒu�ɖ߂�
            else
            {
                transform.localPosition = originalPosition;
                elapsed = 0.0f;
                isShaking = false;
            }
        }
    }

    // �ЊQ���J�n���郁�\�b�h
    public void TriggerDisaster()
    {
        elapsed = 0.0f;
        isShaking = true;
    }
}
