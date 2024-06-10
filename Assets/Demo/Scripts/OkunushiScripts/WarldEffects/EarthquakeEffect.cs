using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    public float duration = 1.0f; // �n�k�̎�������
    public float magnitude = 0.1f; // �n�k�̗h��̑傫��

    private Vector3 originalPosition;
    private float elapsed = 0.0f;       //�ăL�[���͎�t����
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            }
            else
            {
                transform.localPosition = originalPosition;
                elapsed = 0.0f;
                isShaking = false;
            }
        }

        // �n�k�̃g���K�[���L�[���͂ōs��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TriggerEarthquake();
            Debug.Log("�G���^�[�L�[��������܂���");
        }
    }

    public void TriggerEarthquake()
    {
        elapsed = 0.0f;
        isShaking = true;
    }
}
