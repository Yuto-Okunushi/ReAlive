using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneAnimationController : MonoBehaviour
{

    Animator animator;      //アニメーション
    int OpenSmartPhone = 0;     //ボタンを押された回数を調べる

    //会話中かどうか
    bool isTalking = false;


    // Start is called before the first frame update
    void Start()
    {
        //アニメーターのコンポーネントを取得する
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTalking = GameManager.GetIsTalking();

        if(!isTalking)
        {
            if (OpenSmartPhone == 0)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SmartPhoneOpneAnimation();
                }
            }
            else if (OpenSmartPhone == 1)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SmartPhoneCloseAnimation();
                }
            }
        }
        
    }

    //スマホパネルを表示するアニメーション
    public void SmartPhoneOpneAnimation()
    {
        //アニメーターのboolを変更する
        animator.SetBool("IsOpend", true);
        OpenSmartPhone++;
    }

    public void SmartPhoneCloseAnimation()
    {
        //アニメーターのboolを変更する
        animator.SetBool("IsOpend", false);
        OpenSmartPhone--;
    }
}
