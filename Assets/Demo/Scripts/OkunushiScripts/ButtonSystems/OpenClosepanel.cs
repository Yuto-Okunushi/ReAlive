using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosepanel : MonoBehaviour
{
    [SerializeField] GameObject OpenPanel;

    public void Openpanel()
    {
        OpenPanel.SetActive(true);
    }


}
