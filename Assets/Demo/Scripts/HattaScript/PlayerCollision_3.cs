using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W4��p

public class PlayerCollision_3 : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;
    [SerializeField] private GameObject SignPanel;
    [SerializeField] private float displayDuration = 3.0f; // �p�l����\�����鎞�ԁi�b�j
    [SerializeField] TimeLineControll_2 timeLineControll_2;

    public bool isOpend = false;        //��������̑��L�����o�X���J����Ă��邩
    public bool isTimeline = false;     //�^�C�����C���̍Đ�����
    public bool isTalking = false;      //��b�����ǂ���

    private void Start()
    {
        SignPanel.SetActive(false);
    }

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        // ����̏ꏊ�ɓ��B�����ꍇ
        for (int i = 0; i < timeLineControll_2.specificLocations.Length; i++)
        {
            if (other.gameObject == timeLineControll_2.specificLocations[i])
            {
                Debug.Log($"����̏ꏊ {i} �ɓ��B���܂���");
                timeLineControll_2.destination(); // �ړI�n�������_���ŃA�N�e�B�u��
                break;
            }
        }

        if (other.gameObject.tag == "Sign1")
        {
            Debug.Log("�W��1�ɂԂ���܂���");
            SendSignDate(0);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign2")
        {
            Debug.Log("�W��2�ɂԂ���܂���");
            SendSignDate(1);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign3")
        {
            Debug.Log("�W��3�ɂԂ���܂���");
            SendSignDate(2);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign4")
        {
            Debug.Log("�W��4�ɂԂ���܂���");
            SendSignDate(3);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "GameOverObject1")
        {
            isTimeline = true;
            GameManager.SetIsTimeline(isTimeline);
            //�^�C�����C���Đ����\�b�h���Ăяo��
            timeLineControll_2.FallRockTimeLine();
        }
        else if (other.gameObject.tag == "Goal1")     // �S�[���P�̃I�u�W�F�N�g�ɓ������Ă��鎞
        {
            timeLineControll_2.StageClear();
        }
        else if (other.gameObject.tag == "Goal2")    // �S�[���Q�̃I�u�W�F�N�g�ɓ������Ă��鎞
        {
            timeLineControll_2.StageClear();
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
