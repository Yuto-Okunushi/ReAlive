using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialControll : MonoBehaviour
{
    [SerializeField] GameObject tutrialImage1;      // �`���[�g���A���ŕ\������摜�P
    [SerializeField] GameObject tutrialImage2;      // �`���[�g���A���ŕ\������摜�Q

    private int tutrialNumber = 0;                  // �`���[�g���A���̒i�K��\���ϐ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            tutrialNumber++;
            if(tutrialNumber == 1)
            {
                tutrialImage1.SetActive(false);
                tutrialImage2.SetActive(true);
            }
            else if(tutrialNumber == 2)
            {
                tutrialImage2.SetActive(true);
            }
            else if(tutrialNumber <= 3)
            {
                SceneController.LoadNextScene("��_DemoScene");
            }
        }
    }
}
