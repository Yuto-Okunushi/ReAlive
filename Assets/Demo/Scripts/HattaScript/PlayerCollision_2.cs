using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

// �X�e�[�W2��p

public class PlayerCollision_2 : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;
    [SerializeField] private GameObject SignPanel;
    [SerializeField] private float displayDuration = 3.0f; // �p�l����\�����鎞�ԁi�b�j
    [SerializeField] TimeLineControll TimeLineControll;
    [SerializeField] private UnityEngine.UI.Text signCollectionText; // UI �e�L�X�g

    public bool isOpend = false;        // ��������̑��L�����o�X���J����Ă��邩
    public bool isTimeline = false;     // �^�C�����C���̍Đ�����
    public bool isTalking = false;      // ��b�����ǂ���

    private bool[] collectedSigns;      // �W�����W��Ԃ�ǐ�
    private int totalSigns;             // �W���̑���
    private int collectedCount = 0;     // ���W�ς݂̕W����

    private void Start()
    {
        SignPanel.SetActive(false);
        totalSigns = signDate.Length;
        collectedSigns = new bool[totalSigns];
        UpdateSignCollectionUI(); // ������Ԃ��X�V
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign1")
        {
            CollectSign(0);
        }
        else if (other.gameObject.tag == "Sign2")
        {
            CollectSign(1);
        }
        else if (other.gameObject.tag == "Sign3")
        {
            CollectSign(2);
        }
        else if (other.gameObject.tag == "GameOverObject1")
        {
            isTimeline = true;
            GameManager.SetIsTimeline(isTimeline);
            // �^�C�����C���Đ����\�b�h���Ăяo��
            TimeLineControll.FallRockTimeLine();
        }
        else if (other.gameObject.tag == "Goal1")     // �S�[��1�̃I�u�W�F�N�g�ɓ������Ă��鎞
        {
            TimeLineControll.StageClear();
        }
        else if (other.gameObject.tag == "Goal2")    // �S�[��2�̃I�u�W�F�N�g�ɓ������Ă��鎞
        {
            TimeLineControll.StageClear();
        }
    }

    private void CollectSign(int index)
    {
        if (index >= 0 && index < collectedSigns.Length && !collectedSigns[index])
        {
            collectedSigns[index] = true; // �W�������W�ς݂ɐݒ�
            collectedCount++; // ���W�����X�V
            UpdateSignCollectionUI(); // UI ���X�V

            SendSignDate(index);
            StartCoroutine(DisplaySignPanel());

            // ���ׂĂ̕W�������W���ꂽ���`�F�b�N
            if (AllSignsCollected())
            {
                StartCoroutine(DelayedEarthquake());
            }
        }
    }

    // �n�k��x�点�Ĕ���������R���[�`��
    private IEnumerator DelayedEarthquake()
    {
        float delayTime = 1.0f; // �x�����ԁi�b�j
        yield return new WaitForSeconds(delayTime); // �w��b���ҋ@

        TimeLineControll.StopEarthquakeTimer(); // �n�k�^�C�}�[���~
        TimeLineControll.EarthquakeTimeline();  // �n�k�𔭐�
    }

    private bool AllSignsCollected()
    {
        return collectedCount == totalSigns;
    }

    private void UpdateSignCollectionUI()
    {
        if (signCollectionText != null)
        {
            signCollectionText.text = $"{collectedCount} / {totalSigns}"; // UI �e�L�X�g���X�V
        }
    }

    private void SendSignDate(int index)
    {
        if (index >= 0 && index < signDate.Length)
        {
            GameManager.SetSignDate(signDate[index]);
        }
        else
        {
            Debug.LogError("Invalid index for signDate array");
        }
    }

    private IEnumerator DisplaySignPanel()
    {
        SignPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        SignPanel.SetActive(false);
    }
}
