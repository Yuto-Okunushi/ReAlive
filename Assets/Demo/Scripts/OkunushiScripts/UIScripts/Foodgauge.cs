using UnityEngine;
using UnityEngine.UI;

public class Foodgauge : MonoBehaviour
{
    public Slider FoodSlider; // スライダーUI
    public float maxGauge = 10f; // 最大ゲージ値（秒）

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
            // 制限時間が過ぎた場合の処理
            Debug.Log("食料値0になった");
        }
    }
}
