using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetPlayerMony : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text MonyText;
    private int Nowmony;

    // Update is called once per frame
    void Update()
    {
        Nowmony = GameManager.GetPlayerMony();
        MonyText.text= Nowmony.ToString();
    }
}
