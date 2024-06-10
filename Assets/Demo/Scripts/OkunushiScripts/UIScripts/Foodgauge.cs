using UnityEngine;
using UnityEngine.UI;

public class Foodgauge : MonoBehaviour
{
    public Slider FoodSlider; // �X���C�_�[UI
    public float maxGauge = 10f; // �ő�Q�[�W�l�i�b�j

    private float remainingTime;

    void Start()
    {
        if (FoodSlider != null)
        {
            FoodSlider.maxValue = maxGauge;
            FoodSlider.value = maxGauge;
            remainingTime = maxGauge;
        }
    }

    void Update()
    {
        
    }

    public void FoodgaugeStartStop()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (FoodSlider != null)
            {
                FoodSlider.value = remainingTime;
            }
        }
        else
        {
            // �������Ԃ��߂����ꍇ�̏���
            Debug.Log("�H���l0�ɂȂ���");
        }
    }
}
