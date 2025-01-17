using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    [SerializeField] GameObject MapCam;         // �}�b�v���f���J����
    [SerializeField] GameObject PlayerMapPin;   // �}�b�v��̃v���C���[�s��

    public bool isMapOpen = false;      // ����Map���J���Ă��邩�m�F�t���O

    public bool isMapShow = true;       // Map���J���邩�̊m�F�t���O

    void Start()
    {
        isMapOpen = GameManager.GetIsOpend();   // ������Ԃ��擾
    }

    void Update()
    {
        isMapShow = GameManager.GetIsMapShow();     // GameManager����t���O���̎󂯎��

        if(isMapShow)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                isMapOpen = !isMapOpen;             // ��Ԃ𔽓]

                MapCam.SetActive(isMapOpen);
                PlayerMapPin.SetActive(isMapOpen);

                GameManager.SetIsOpend(isMapOpen);  // ��Ԃ�GameManager�ɔ��f
            }
        }
        
    }

    public void MapHidden()     // �}�b�v���\���ɂ��郁�\�b�h
    {
        MapCam.SetActive(false);
        isMapOpen = false;
        GameManager.SetIsOpend(isMapOpen);
    }
}
