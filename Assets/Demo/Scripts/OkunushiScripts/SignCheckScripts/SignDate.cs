using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu( menuName = "ScriptableObjects/Create SignDate", fileName ="SignDatefile")]

public class SignDate : ScriptableObject
{
    public string signName;        //�W���̖��O
    public int singNumber;         //�W���̓o�^�ԍ�
    public string signDetaile;      //�W���̏ڍאݒ�
    public Sprite itemImage;       //�W���̃C���[�W�摜
}
