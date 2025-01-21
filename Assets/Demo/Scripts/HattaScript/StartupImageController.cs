using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//���ѓ��e�\��

public class StartupImageController : MonoBehaviour
{
    public Image startupImage; // �\������摜
    public float displayTime = 3.0f; // �摜��\�����鎞�ԁi�b�j

    void Start()
    {
        if (startupImage != null)
        {
            StartCoroutine(ShowStartupImage());
        }
    }

    private IEnumerator ShowStartupImage()
    {
        // �摜��\��
        startupImage.enabled = true;

        // �w�莞�ԑҋ@
        yield return new WaitForSeconds(displayTime);

        // �摜���\��
        startupImage.enabled = false;
    }
}
