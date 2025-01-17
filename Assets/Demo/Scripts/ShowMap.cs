using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    [SerializeField] GameObject MapCam;         // マップを映すカメラ
    [SerializeField] GameObject PlayerMapPin;   // マップ上のプレイヤーピン

    public bool isMapOpen = false;      // 現在Mapを開いているか確認フラグ

    public bool isMapShow = true;       // Mapを開けるかの確認フラグ

    void Start()
    {
        isMapOpen = GameManager.GetIsOpend();   // 初期状態を取得
    }

    void Update()
    {
        isMapShow = GameManager.GetIsMapShow();     // GameManagerからフラグ情報の受け取り

        if(isMapShow)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                isMapOpen = !isMapOpen;             // 状態を反転

                MapCam.SetActive(isMapOpen);
                PlayerMapPin.SetActive(isMapOpen);

                GameManager.SetIsOpend(isMapOpen);  // 状態をGameManagerに反映
            }
        }
        
    }

    public void MapHidden()     // マップを非表示にするメソッド
    {
        MapCam.SetActive(false);
        isMapOpen = false;
        GameManager.SetIsOpend(isMapOpen);
    }
}
