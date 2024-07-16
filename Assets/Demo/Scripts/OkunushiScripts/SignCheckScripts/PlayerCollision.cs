using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] SignDate[] signDate;
    [SerializeField] private GameObject SignPanel;
    [SerializeField] private float displayDuration = 3.0f; // �p�l����\�����鎞�ԁi�b�j

    private void Start()
    {
        SignPanel.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sign1")
        {
            Debug.Log("�W��1�ɂԂ���܂���");
            SendSignDate(0);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign2")
        {
            Debug.Log("�W��2�ɂԂ���܂���");
            SendSignDate(1);
            StartCoroutine(DisplaySignPanel());
        }
        else if (other.gameObject.tag == "Sign3")
        {
            Debug.Log("�W��3�ɂԂ���܂���");
            SendSignDate(2);
            StartCoroutine(DisplaySignPanel());
        }
    }

    private void SendSignDate(int index)
    {
        if (index >= 0 && index < signDate.Length)
        {
            GameManager.SetSignDate(signDate[index]);
        }
        else
        {
            Debug.LogError("Invalid index for signDate array");
        }
    }

    private IEnumerator DisplaySignPanel()
    {
        SignPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        SignPanel.SetActive(false);
    }
}
