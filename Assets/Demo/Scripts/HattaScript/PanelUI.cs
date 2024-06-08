using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    public static PanelUI Instance { get; private set; } // �V���O���g���̃C���X�^���X

    private void Awake()
    {
        // �V���O���g���̃C���X�^���X��ݒ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �Q�[���I�u�W�F�N�g���V�[���ԂŔj������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject); // ���ɃC���X�^���X�����݂���ꍇ�͔j��
        }

        // DOTween�̏�����
        DOTween.Init();
    }

    private void Start()
    {
        // �p�l���̏�����]�p�x��ݒ�
        transform.rotation = Quaternion.Euler(90, 0, 0);

        // �p�l���̃A�j���[�V�������J�n
        StartCoroutine(PanelAnim());
    }

    public static IEnumerator PanelAnim()
    {
        // 0.5�b�ҋ@
        yield return new WaitForSeconds(0.5f);

        // �p�l����0.7�b�����ĉ�]������i90�x����0�x�ցj
        yield return Instance.transform.DORotate(new Vector3(0, 0, 0), 0.7f).WaitForCompletion();

        // 1�b�ҋ@
        yield return new WaitForSeconds(1f);

        // �p�l����0.7�b�����ĉ�]������i0�x����90�x�֖߂��j
        yield return Instance.transform.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();
    }
}
