using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimeLineControll : MonoBehaviour
{
    [SerializeField] private PlayableDirector[] Timelines;

    //�n�k��������
    public float earthquakeTime = 10.0f;
    //���Ԍo��
    public float time = 0.0f;

    //�J�E���g�����邩�ǂ����̔��f
    private bool isCount = true;

    public bool isTimeline = false;

    public void Update()
    {
        if(isCount)
        {
            time += Time.deltaTime;
            if (time >= earthquakeTime)
            {
                Debug.Log("�X�N���v�g�����s����܂���");
                EarthquakeTimeline();
                isCount = false;
            }
        }
    }

    public void EarthquakeTimeline()
    {
        //�O�Ԗڂ̃^�C�����C�����Đ�
        Timelines[0].Play();
    }

    public void FlugTure()
    {
        isTimeline = true;
        GameManager.SetIsTimeline(isTimeline);
    }

    public void FlugFalse()
    {
        isTimeline = false;
        GameManager.SetIsTimeline(isTimeline);
    }
}
