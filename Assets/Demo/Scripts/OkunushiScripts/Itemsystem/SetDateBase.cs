using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetDateBase : MonoBehaviour
{
    [SerializeField] private ItemData itemDatabase;

    public string itemDescription;

    public string itemDetails;
 
    void Update()
    {
        GameManager.SetItemName(itemDescription);
        GameManager.SetItemDetailsName(itemDetails);
    }
}
