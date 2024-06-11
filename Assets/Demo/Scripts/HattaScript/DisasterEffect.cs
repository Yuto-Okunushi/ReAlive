using UnityEngine;

public class DisasterEffect : MonoBehaviour
{
    public static DisasterEffect Instance { get; private set; } // シングルトンのインスタンス

    public float duration = 1.0f; // 災害の持続時間
    public float magnitude = 0.1f; // 災害の揺れの大きさ

    private Vector3 originalPosition; // 元の位置を保存
    private float elapsed = 0.0f; // 経過時間
    private bool isShaking = false; // 災害が発生中かどうかのフラグ

    void Awake()
    {
        // シングルトンのインスタンスを設定
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
        // 初期位置を保存
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // 災害が発生中の場合
        if (isShaking)
        {
            // 持続時間内であれば揺れを計算
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            }
            // 持続時間を超えたら元の位置に戻す
            else
            {
                transform.localPosition = originalPosition;
                elapsed = 0.0f;
                isShaking = false;
            }
        }
    }

    // 災害を開始するメソッド
    public void TriggerDisaster()
    {
        elapsed = 0.0f;
        isShaking = true;
    }
}
