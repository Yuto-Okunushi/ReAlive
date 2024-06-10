using System.Collections;
using UnityEngine;

public class DisasterSystem : MonoBehaviour
{
    public static DisasterSystem Instance { get; private set; } // �V���O���g���̃C���X�^���X

    public Animator disasterAnim; // �ЊQ�����A�j���[�V����
    public Vector3 startPoint; // �X�^�[�g�n�_
    public Vector3 destPoint; // �ړI�n

    private bool disasterTrig = false;
    private float evacTimer = 180f; // 3��(180�b)�̃^�C�}�[

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
    }

    void Start()
    {
        // �Q�[�����[�v�̃R���[�`�����J�n
        StartCoroutine(GameLoop());
    }

    // �Q�[�����[�v�̊Ǘ�
    IEnumerator GameLoop()
    {
        while (true)
        {
            // �����t�F�[�Y�̊J�n
            yield return StartCoroutine(Prep());
            // �ЊQ�����t�F�[�Y
            yield return StartCoroutine(Disaster());
            // ���t�F�[�Y�̊J�n
            yield return StartCoroutine(Evac());
        }
    }

    // �����t�F�[�Y�̏���
    IEnumerator Prep()
    {
        UnityEngine.Debug.Log("�����t�F�[�Y�J�n");

        // �ЊQ�t���O�̃��Z�b�g
        disasterTrig = false;

        // �����t�F�[�Y�̃A�j���[�V�������J�n
        yield return StartCoroutine(PanelUI.Instance.PrepAnim());

        // �����_���Ȏ��ԑҋ@ (3������4���̊�)
        float randomTime = UnityEngine.Random.Range(180f, 240f);
        yield return new WaitForSeconds(randomTime);

        // �ЊQ�������g���K�[
        TriggerDisaster();
    }

    // �ЊQ�������g���K�[
    void TriggerDisaster()
    {
        if (!disasterTrig)
        {
            disasterTrig = true;
            UnityEngine.Debug.Log("�ЊQ����");
            // �ЊQ�����A�j���[�V�������J�n
            disasterAnim.SetTrigger("StartDisaster");
        }
    }

    // �ЊQ�����t�F�[�Y�̏���
    IEnumerator Disaster()
    {
        UnityEngine.Debug.Log("�ЊQ�t�F�[�Y�J�n");

        // �ЊQ�����A�j���[�V�����̍Đ����ԑҋ@
        yield return new WaitForSeconds(disasterAnim.GetCurrentAnimatorStateInfo(0).length);

        // �����t�F�[�Y�̏I������
        EndPrep();
    }

    // �����t�F�[�Y�̏I������
    void EndPrep()
    {
        UnityEngine.Debug.Log("�����t�F�[�Y�I��");
        // �����t�F�[�Y�𖳌���
        if (PanelUI.Instance.prepPanel != null)
        {
            PanelUI.Instance.prepPanel.gameObject.SetActive(false);
        }
    }

    // ���t�F�[�Y�̏���
    IEnumerator Evac()
    {
        UnityEngine.Debug.Log("���t�F�[�Y�J�n");

        // ���t�F�[�Y�̃A�j���[�V�������J�n
        yield return StartCoroutine(PanelUI.Instance.EvacAnim());

        float timer = evacTimer;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            // �v���C���[���ړI�n�ɓ��B�������`�F�b�N
            if (Vector3.Distance(Player.Instance.transform.position, destPoint) < 1f)
            {
                ReachDest();
                yield break; // �R���[�`�����I��
            }

            yield return null;
        }

        // �^�C�}�[���؂ꂽ�ꍇ
        EndEvac();
    }

    // ���t�F�[�Y�̏I�������i�^�C���A�E�g�j
    void EndEvac()
    {
        UnityEngine.Debug.Log("���t�F�[�Y�I�� - �^�C���A�E�g");

        // �v���C���[�I�u�W�F�N�g������
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
    }

    // �v���C���[���ړI�n�ɓ��B�������̏���
    void ReachDest()
    {
        UnityEngine.Debug.Log("�v���C���[���ړI�n�ɓ��B");

        // �v���C���[���X�^�[�g�n�_�ɖ߂�
        Player.Instance.transform.position = startPoint;
    }
}
