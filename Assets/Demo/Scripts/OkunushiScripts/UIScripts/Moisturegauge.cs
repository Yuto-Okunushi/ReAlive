using UnityEngine;
using UnityEngine.UI;

public class Moisturegauge : MonoBehaviour
{
    public Slider moistureSlider; // �X���C�_�[UI
    public float maxGauge = 10f; // �ő�Q�[�W�l�i�b�j

    private float remainingTime;

    void Start()
    {
        if (moistureSlider != null)
        {
            moistureSlider.maxValue = maxGauge;
            moistureSlider.value = maxGauge;
            remainingTime = maxGauge;
        }
    }

    void Update()
    {
        
    }

    public void MoisturegaugeStartStop()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (moistureSlider != null)
            {
                moistureSlider.value = remainingTime;
            }
        }
        else
        {
            // �������Ԃ��߂����ꍇ�̏���
            Debug.Log("�����l0�ɂȂ���");
        }
    }

}
