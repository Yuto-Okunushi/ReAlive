using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PanelUI : MonoBehaviour
{
    public static PanelUI Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public Transform prepPanel; // �����t�F�[�Y�̃p�l��
    public Transform evacPanel; // ���t�F�[�Y�̃p�l��

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
        if (prepPanel != null)
        {
            prepPanel.rotation = Quaternion.Euler(90, 0, 0);
            prepPanel.gameObject.SetActive(false); // ������Ԃł̓p�l�����\���ɂ���
        }

        if (evacPanel != null)
        {
            evacPanel.rotation = Quaternion.Euler(90, 0, 0);
            evacPanel.gameObject.SetActive(false); // ������Ԃł̓p�l�����\���ɂ���
        }
    }

    public IEnumerator PrepAnim()
    {
        if (prepPanel == null) yield break;

        // �p�l����\��
        prepPanel.gameObject.SetActive(true);

        // 0.5�b�ҋ@
        yield return new WaitForSeconds(0.5f);

        // �p�l����0.7�b�����ĉ�]������i90�x����0�x�ցj
        yield return prepPanel.DORotate(new Vector3(0, 0, 0), 0.7f).WaitForCompletion();

        // 1�b�ҋ@
        yield return new WaitForSeconds(1f);

        // �p�l����0.7�b�����ĉ�]������i0�x����90�x�֖߂��j
        yield return prepPanel.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // �p�l�����\���ɂ���
        prepPanel.gameObject.SetActive(false);
    }

    public IEnumerator EvacAnim()
    {
        if (evacPanel == null) yield break;

        // �p�l����\��
        evacPanel.gameObject.SetActive(true);

        // 0.5�b�ҋ@
        yield return new WaitForSeconds(0.5f);

        // �p�l����0.7�b�����ĉ�]������i90�x����0�x�ցj
        yield return evacPanel.DORotate(new Vector3(0, 0, 0), 0.7f).WaitForCompletion();

        // 1�b�ҋ@
        yield return new WaitForSeconds(1f);

        // �p�l����0.7�b�����ĉ�]������i0�x����90�x�֖߂��j
        yield return evacPanel.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // �p�l�����\���ɂ���
        evacPanel.gameObject.SetActive(false);
    }
}
