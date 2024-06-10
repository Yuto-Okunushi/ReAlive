using UnityEngine;

public class EarthquakeEffect : MonoBehaviour
{
    public float duration = 1.0f; // 地震の持続時間
    public float magnitude = 0.1f; // 地震の揺れの大きさ

    private Vector3 originalPosition;
    private float elapsed = 0.0f;       //再キー入力受付時間
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

        // 地震のトリガーをキー入力で行う
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TriggerEarthquake();
            Debug.Log("エンターキーが押されました");
        }
    }

    public void TriggerEarthquake()
    {
        elapsed = 0.0f;
        isShaking = true;
    }
}
