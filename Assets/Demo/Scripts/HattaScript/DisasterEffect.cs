using UnityEngine;
using Cinemachine;

public class DisasterEffect : MonoBehaviour
{
    public static DisasterEffect Instance { get; private set; } // シングルトンのインスタンス

    public float duration = 30.0f; // 災害の持続時間を延長
    public float initialMagnitude = 1.0f; // 初期の揺れの大きさ
    public float finalMagnitude = 0.1f; // 最終的な揺れの大きさ

    private float elapsed = 0.0f; // 経過時間
    private bool isShaking = false; // 災害が発生中かどうかのフラグ

    private CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    private CinemachineBasicMultiChannelPerlin noise; // Cinemachine Noise

    void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.Debug.Log("DisasterEffect インスタンスが設定されました");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Cinemachine Virtual Camera の取得
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            UnityEngine.Debug.Log("CinemachineVirtualCamera が見つかりました: " + virtualCamera.name);
        }
        else
        {
            UnityEngine.Debug.LogError("CinemachineVirtualCamera が見つかりません。");
        }

        // 初期化時に揺れを止める
        StopDisaster();
    }

    void Update()
    {
        // 災害が発生中の場合
        if (isShaking && noise != null)
        {
            // 持続時間内であれば揺れを計算
            if (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float currentMagnitude = Mathf.Lerp(initialMagnitude, finalMagnitude, elapsed / duration);
                noise.m_AmplitudeGain = currentMagnitude;
                noise.m_FrequencyGain = currentMagnitude;
                UnityEngine.Debug.Log("カメラが揺れています: Amplitude=" + noise.m_AmplitudeGain + ", Frequency=" + noise.m_FrequencyGain);
            }
            // 持続時間を超えたら元の位置に戻す
            else
            {
                StopDisaster();
            }
        }
    }

    // 災害を開始するメソッド
    public void TriggerDisaster()
    {
        elapsed = 0.0f;
        isShaking = true;
        UnityEngine.Debug.Log("災害がトリガーされました");
    }

    // 災害を停止するメソッド
    public void StopDisaster()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
            UnityEngine.Debug.Log("カメラの揺れが終了しました");
        }
        isShaking = false;
    }
}
