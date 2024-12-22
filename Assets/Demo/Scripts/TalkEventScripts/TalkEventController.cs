using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;�@//�ǉ�


public class TalkEventController : MonoBehaviour
{
    [SerializeField] GameObject TalkEventPanel;     //�C�x���g�e�L�X�g�̃L�����o�X
    [SerializeField] Text EventText;                //�C�x���g���e��\������e�L�X�g
    [SerializeField] Text CharactorNameText;        //�b���Ă���L�����N�^�[�̖��O
    string NowTalk;
    string FirstName;
    public bool isTalking = false;

    public int CharactorNameNumber = 2;
    public int TalkEventName = 5;

    //CSV�֘A
    private TextAsset csvFile; // CSV�t�@�C��
    private List<string[]> csvData = new List<string[]>(); // CSV�t�@�C���̒��g�����郊�X�g

    private void Start()
    {
        //SCV�֘A
        csvFile = Resources.Load("TalkEventContrnts") as TextAsset; // Resources�ɂ���CSV�t�@�C�����i�[
        StringReader reader = new StringReader(csvFile.text); // TextAsset��StringReader�ɕϊ�

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1�s���ǂݍ���
            csvData.Add(line.Split(',')); // csvData���X�g�ɒǉ�����
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isTalking)
        {
            //if���ł������̗v�f�ɉ����������������ɂ�����
            TalkManager();
        }
    }

    public void Objectactivetrue()
    {
        //talk���̔��f
        isTalking = true;
        GameManager.SetIsTalking(isTalking);
        //�I�u�W�F�N�g�̕\��
        TalkEventPanel.SetActive(true);

    }

    public void Objectactivefalse()
    {
        //�I�u�W�F�N�g�̔�\��
        TalkEventPanel.SetActive(false);
        isTalking = false;
        isTalking =  GameManager.GetIsTalking();
    }

    public void SelectEvent1()
    {
        if(isTalking)
        {
            FirstName = csvData[CharactorNameNumber][0];
            NowTalk = csvData[TalkEventName][0];
            //�L�����N�^�[�̖��O����
            CharactorNameText.text = FirstName;
            //��ԍŏ��̉�b���e������
            EventText.text = NowTalk;
            
            //�Q�[���}�l�[�W���[�Ƀf�[�^�󂯓n��
            GameManager.SendCSVTalk(NowTalk);
            GameManager.SendCSVCharactorName(FirstName);
        }
    }

    //��b�����ɐi�߂郁�\�b�h
    public void TalkManager()
    {
        // ���E�`�F�b�N
        if (TalkEventName + 1 < csvData.Count && csvData[TalkEventName + 1].Length > 0)
        {
            TalkEventName++;
            NowTalk = csvData[TalkEventName][0];
            EventText.text = NowTalk;
        }
        else
        {
            Objectactivefalse(); // ���̗v�f���Ȃ��ꍇ�͔�\���ɂ���
            isTalking = false;
            GameManager.SetIsTalking(isTalking);
        }
    }

}
