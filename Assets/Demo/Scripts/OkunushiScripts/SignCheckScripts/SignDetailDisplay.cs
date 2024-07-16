using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDetailDisplay : MonoBehaviour
{
    [SerializeField] private Text Detail;
    [SerializeField] private Image SignImage;

    // Update is called once per frame
    public void Update()
    {
        SignDate signDate = GameManager.GetSignDate();

        Detail.text = $"{signDate.signName}\n{signDate.signDetaile}";
        SignImage.sprite = signDate.signImage;
    }
}


