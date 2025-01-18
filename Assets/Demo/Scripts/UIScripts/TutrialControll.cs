using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialControll : MonoBehaviour
{
    [SerializeField] GameObject tutrialImage1;      // チュートリアルで表示する画像１
    [SerializeField] GameObject tutrialImage2;      // チュートリアルで表示する画像２

    private int tutrialNumber = 0;                  // チュートリアルの段階を表す変数
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
                SceneController.LoadNextScene("β_DemoScene");
            }
        }
    }
}
