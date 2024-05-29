using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;      //静的インスタンス化

    public delegate void Delegete(string nextSceneName);    //メソッドを引数に持たせるための変数化デリゲート型
    [SerializeField] private GameObject sceneCanvas;        //シーンコントローラの子オブジェクト、シーンキャンバスのゲームオブジェクト型としての取得
    private List<Animator> scAnimator = new List<Animator>();       //シーンコントローラのアニメーターをリストとして取得

    private float time = 0;     //シーンロード中の時間

    private void Awake()            //startより前にシーンコントローラプレファブをシングルトン化
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }



    //====================================================================================//

    private void Start()
    {
        GameManager.SetNowScene(SceneManager.GetActiveScene().name);        //現在のシーン名をゲームマネージャにセット
    }

    private void Update()
    {
        if (GameManager.GetIsSceneLoading())        //シーンロード中ならtimeを増加
        {
            time += Time.deltaTime;
        }
        else if (!GameManager.GetIsSceneLoading())      //シーンロード終わり次第timeを初期化
        {
            time = 0;
        }
    }

    //====================================================================================//

    static public void LoadNextScene(string nextSceneName)      //次のシーンを呼び出すメソッド
    {
        GameManager.SetNowScene(nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    //======================================================================================//

    static public void SceneTransitionAnimation(GameObject parent)      //シーントランジションアニメーション　動かすオブジェクトの親オブジェクトが第一引数
    {

        if (instance.scAnimator != null && instance.scAnimator.Count > 0)       //配列外へのアクセスを制限するエラー回避処理
        {
            instance.scAnimator.Clear();
        }
        Animator[] localAnimator;
        localAnimator = parent.GetComponentsInChildren<Animator>();     //親オブジェクトの子オブジェクトのアニメーターコンポーネントをすべて取得
        instance.scAnimator.AddRange(localAnimator);                    //メンバ変数に再定義
        for (int i = 0; i < localAnimator.Length; i++)                  //取得した分のオブジェクトコンポーネント回数分繰り返し
        {
            instance.scAnimator[i].SetTrigger("Transition");
        }
    }

    static public void SceneTransitionAnimation(GameObject parent, string transitionName)       //上と同じメソッドオーバーロードしてます　第二引数にアニメーションのトリガー
    {

        if (instance.scAnimator != null && instance.scAnimator.Count > 0)
        {
            instance.scAnimator.Clear();
        }
        Animator[] localAnimator;
        localAnimator = parent.GetComponentsInChildren<Animator>();
        instance.scAnimator.AddRange(localAnimator);
        for (int i = 0; i < localAnimator.Length; i++)
        {
            instance.scAnimator[i].SetTrigger(transitionName);
        }
    }
}