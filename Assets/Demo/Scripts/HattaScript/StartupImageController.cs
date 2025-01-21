using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//実績内容表示

public class StartupImageController : MonoBehaviour
{
    public Image startupImage; // 表示する画像
    public float displayTime = 3.0f; // 画像を表示する時間（秒）

    void Start()
    {
        if (startupImage != null)
        {
            StartCoroutine(ShowStartupImage());
        }
    }

    private IEnumerator ShowStartupImage()
    {
        // 画像を表示
        startupImage.enabled = true;

        // 指定時間待機
        yield return new WaitForSeconds(displayTime);

        // 画像を非表示
        startupImage.enabled = false;
    }
}
