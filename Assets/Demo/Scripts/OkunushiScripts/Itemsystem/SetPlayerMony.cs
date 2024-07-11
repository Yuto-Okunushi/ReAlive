using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerMony : MonoBehaviour
{
    [SerializeField] Text MonyText;

    // Update is called once per frame
    void Update()
    {
        int Nowmony = GameManager.GetPlayerMony();
        MonyText.text = Nowmony.ToString();
    }
}
