using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObujectManager : MonoBehaviour
{
    [SerializeField] Canvas inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectOpen();
    }

    void ObjectOpen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool isActive = !inventory.gameObject.activeSelf;
            inventory.gameObject.SetActive(isActive);
            Debug.Log("スペースキーが押されました");
            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    
}
