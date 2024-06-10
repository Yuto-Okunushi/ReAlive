using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private bool isTimeCount = false;
    public float nowTime = 60f;     //���݂̎���

    private void Awake()
    {
        timerText = GetComponent<Text>();       //�e�L�X�g�̎擾
    }

    void Start()
    {
        isTimeCount = true;
    }

    void Update()
    {
        
    }

    public void TimerStartStop()
    {
        if (isTimeCount)
        {
            nowTime -= Time.deltaTime;  // �o�ߎ��Ԃ����Z

            // �^�C�}�[��0�ȉ��ɂȂ������~
            if (nowTime <= 0)
            {
                isTimeCount = false;
                nowTime = 0;
            }

            // �b�ɕϊ�
            float seconds = nowTime % 60;

            // �^�C�}�[�̃e�L�X�g���X�V
            timerText.text = seconds.ToString("00.00");
        }
    }
}
