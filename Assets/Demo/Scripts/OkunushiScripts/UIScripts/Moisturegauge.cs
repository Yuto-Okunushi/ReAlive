using UnityEngine;
using UnityEngine.UI;

public class Moisturegauge : MonoBehaviour
{
    public Slider moistureSlider; // スライダーUI
    public float maxGauge = 10f; // 最大ゲージ値（秒）

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
            // 制限時間が過ぎた場合の処理
            Debug.Log("水分値0になった");
        }
    }

}
