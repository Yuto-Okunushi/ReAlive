using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//�X�e�[�W3��p
public class TimeLineControll_4 : MonoBehaviour
{
    public GameObject[] objectsToActivate; // �����_���Őݒ肷��I�u�W�F�N�g�Q


    [SerializeField] public PlayableDirector[] Timelines;       // �ʏ�̃C�x���g�Ŏg����^�C�����C��
    [SerializeField] public PlayableDirector[] FlashBackTimelines;      // �t���b�V���o�b�N�̎��Ɏg���^�C�����C��
    [SerializeField] GameObject DistractionImage1;       // �ړI�n�̒ʒm������C���[�W
    [SerializeField] GameObject DistractionImage2;       // �ړI�n�̒ʒm������C���[�W

    public float earthquakeTime = 10.0f;
    public float time = 0.0f;
    private bool isCount = true;
    public bool isTimeline = false;
    public bool isMapShow = true;
    public int randomTimeLinenum;       // �����_���Ń^�C�����C�����Đ������邽�߂̕ϐ�

    void Start()
    {
        earthquakeTime = Random.Range(30.0f, 40.0f);
    }

    public void Update()
    {
        if (isCount)
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

    public void EarthquakeTimeline() => Timelines[0].Play();
    public void FallRockTimeLine() => Timelines[1].Play();
    public void FlashBackTimeLine() => Timelines[2].Play();
    public void DeadTimeLine() => Timelines[3].Play();
    public void StageClear() => Timelines[4].Play();        // �X�e�[�W�N���A���̃^�C�����C�����Đ�����

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

    public void ShowMapFlag()
    {
        isMapShow = false;
        GameManager.SetIsMapShow(isMapShow);
    }

    public void destination() // �����_���ɖړI�n��ݒ肷��V�X�e��
    {
        // �S�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // �����_����1�̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
        int randomIndex = Random.Range(0, objectsToActivate.Length);
        objectsToActivate[randomIndex].SetActive(true);

        // �����_���ȃI�u�W�F�N�g�ɉ����Ēʒm�C���[�W��\�����A2�b��ɔ�\���ɂ���
        if (randomIndex == 0)
        {
            DistractionImage1.SetActive(true);
            DistractionImage2.SetActive(false);
            StartCoroutine(HideImageAfterDelay(DistractionImage1, 6.0f));
        }
        else if (randomIndex == 1)
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(true);
            StartCoroutine(HideImageAfterDelay(DistractionImage2, 6.0f));
        }
        else
        {
            DistractionImage1.SetActive(false);
            DistractionImage2.SetActive(false);
        }
    }

    // �w�肵���C���[�W����莞�Ԍ�ɔ�\���ɂ���R���[�`��
    private IEnumerator HideImageAfterDelay(GameObject image, float delay)
    {
        yield return new WaitForSeconds(delay);  // �w��b���ҋ@
        image.SetActive(false);                 // �C���[�W���\��
    }

    public void GoNextScene1()       // TimeLine�ŃV�[���J�ڂ������邽�߂̃��\�b�h
    {
        SceneController.LoadNextScene("stage_4");
    }

    public void RamdomFlashBackTimeLine()       // �����_���Ƀt���b�V���o�b�N�̃^�C�����C�����Đ������郁�\�b�h
    {
        randomTimeLinenum = Random.Range(0, 3);      // 0�`2�̐��l�������_���ɑ��
        FlashBackTimelines[randomTimeLinenum].Play();       // ����������l�ɑΉ������^�C�����C�����Đ�
    }

    public void StopEarthquakeTimer()
    {
        isCount = false; // �^�C�}�[�𖳌���
    }

    public void EarthQuakeTimeReset()
    {
        earthquakeTime = Random.Range(30.0f, 40.0f);        // �����_���ł�����x���Ԃ�ݒ�
        time = 0;                                           // �v���̎��Ԃ�0�ɖ߂��Ă܂��J�E���g���n�߂�
    }

}
