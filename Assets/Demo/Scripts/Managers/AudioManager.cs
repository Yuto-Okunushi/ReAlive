using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;     //�ÓI�C���X�^���X��

    public enum BGMType     //�I�[�f�B�I�N���b�v�̔z������₷�����邽�߂̗񋓌^(BGM)
    {
        None,
        House,
        Store,
        ModeSelect,
        Game,
        Result,
        POPList
    }

    public enum SEType      //�I�[�f�B�I�N���b�v�̔z������₷�����邽�߂̗񋓌^(SE)
    {
        None,
        Decide,
        Cancel,
        CountDown,
        Start,
        Finish,
        Paper,
        ResultUnit,
        ResultDrum,
        Cursor,
        QuestionDisplay,
        Excellent,
        Great,
        Good,
        Unknown,
        CutIn,
        OpenDoor,
    }

    public enum VOICEType   //�I�[�f�B�I�N���b�v�̔z������₷�����邽�߂̗񋓌^(VOICE)
    {
        None,
        Yoshiyaruzo,
        Irassyaimase,
        Tesutodesu,
        Ichi,
        Ni,
        San,
        Start,
        Hajimaruyo,
        Yokudekimashita,
        Sassugaa,
        Waasugoi,
        Matane,
        Iikanji,
        Yattaa,
        Sugoinele,
        Tsugiwadekiruyo,
        Daijoubunandomo,
        Kondokosodekiruyo,
        Damemitai,
        Mousukoshiganbaroune,
        Maittanala,

        Goyukkuridozo,
        Hawawa,
        Ready_Go,
        Naniwotsukuro,
        Ganbarimasu,
        Isoide,
        Mataaeruyone,
        Arigatougozaimashita,
    }

    /*public enum VOICEType
    {
        None,                 // NONE
        Ikkuyo_1,             // �悵��邼�I
        Ikkuyo_2,             // ��������Ⴂ�܂��I
        HitagiClub,           // �e�X�g�ł�
        CountDown1,           // ����
        CountDown2,           // ��
        CountDown3,           // ����
        Start,                // �X�^�[�g�I
        Hajimeruyo,           // �͂��܂��
        Yattane,              // �悭�ł��܂���
        Rakusyo_,             // ����������
        Sugoine,              // ��[������
        Matane,               // �܂���
        Wa_iwa_i,             // ��������
        Yattayatta,           // �������
        Omedetou,             // �������˂�
        O_omedetou,           // ���͂ł����
        Ganbatte,             // ���v�A�Ȃ�ǂ�
        Ganbareganbare,       // ���x�����ł����
        Dodousiyou,           // ���߂݂���
        Bakanisitennoka,      // ���������撣�낤��
        Madaganbareru,        // �܂������Ȃ�

        //�����܂ł͕��q�̃{�C�X�ō���Ă����ߑ��̏ꏊ�ł̗񋓗v�f���������������Ă܂���
        //��������͐V�{�C�X�Ȃ̂ŕύX����K�v�͂���܂���

        Goyukkuridozo,
        Hawawa,
        Ready_Go,
        Naniwotsukuro,
        Ganbarimasu,
        Isoide,
        Mataaeruyone,
        Arigatougozaimashita,
    }*/

    [SerializeField] private AudioClip[] BGMc;         //BGMClip�̔z��
    [SerializeField] private AudioClip[] SEc;          //SEClip�̔z��
    [SerializeField] private AudioClip[] VOICEc;       //VOICEClip�̔z��

    [SerializeField] private AudioMixer audioMixer;    //�g���I�[�f�B�I�~�L�T�[���`

    private AudioSource[] audioSource;                          //�����g���I�[�f�B�I�\�[�X��ύX

    private bool isChangeVolume = false;                        //���݉��ʕύX�����ǂ���

    private void Awake()            //start���O�ɃI�[�f�B�I�}�l�[�W���v���t�@�u���V���O���g����
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        instance.audioSource = GetComponents<AudioSource>();    //�I�[�f�B�I�\�[�X�R���|�[�l���g�����O�Ɏ擾
    }

    private void Update()       //�펞�Q�[���}�l�[�W������̉����擾���ĉ���ݒ�
    {
        if (!instance.isChangeVolume)
        {
            SetAudioMixerVolume("BGM1", GameManager.GetVolumeBGM());
            SetAudioMixerVolume("BGM2", GameManager.GetVolumeBGM());
        }
        SetAudioMixerVolume("SE", GameManager.GetVolumeSE());
        SetAudioMixerVolume("VOICE", GameManager.GetVolumeVOICE());
    }

    public static void PlayBGM(BGMType bgmType)         //BGM�𗬂�����
    {
        if (instance == null) return;                               //�G���[���

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)bgmType;

        if (instance.audioSource[0].isPlaying)                      //�I�[�f�B�I�\�[�X0�Ԗڂ��g�p���Ă�����A0�Ԗڂ̃N���b�v�ύX
        {
            instance.audioSource[0].clip = instance.BGMc[index];
            instance.audioSource[0].Play();
        }
        else if (instance.audioSource[1].isPlaying)                 //�I�[�f�B�I�\�[�X0�Ԗڂ��g�p���Ă�����A�Ԗڂ̃N���b�v�ύX
        {
            instance.audioSource[1].clip = instance.BGMc[index];
            instance.audioSource[1].Play();
        }
        else                                                        //�I�[�f�B�I�\�[�X�g�p���ĂȂ�������0�Ԗڂ̃N���b�v�ύX
        {
            instance.audioSource[0].clip = instance.BGMc[index];
            instance.audioSource[0].Play();
        }
    }

    public static void CrossFadeBGM(BGMType bgmType)    //BGM�̃N���X�t�F�[�h����
    {
        if (instance == null) return;                               //�G���[���

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)bgmType;

        if (instance.audioSource[0].isPlaying && !instance.isChangeVolume)
        {
            instance.audioSource[1].clip = instance.BGMc[index];
            instance.audioSource[1].Play();
            instance.StartCoroutine(CrossFade(GameManager.GetDurationCrossBGM(), "BGM1", "BGM2", instance.audioSource[0]));

        }
        else if (instance.audioSource[1].isPlaying && !instance.isChangeVolume)
        {
            instance.audioSource[0].clip = instance.BGMc[index];
            instance.audioSource[0].Play();
            instance.StartCoroutine(CrossFade(GameManager.GetDurationCrossBGM(), "BGM2", "BGM1", instance.audioSource[1]));
        }
        else if (!instance.isChangeVolume)
        {
            instance.audioSource[0].clip = instance.BGMc[0];
            instance.audioSource[1].clip = instance.BGMc[0];
        }
    }

    private static IEnumerator CrossFade(float duration, string currentParametor, string nextParametor, AudioSource ad)
    //�N���X�t�F�[�h����(�N���X�t�F�[�h���鎞��, ���݂̃I�[�f�B�I�~�L�T�[�̃p�����[�^, ���̃I�[�f�B�I�~�L�T�[�̃p�����[�^, �g�p����I�[�f�B�I�\�[�X)
    {
        float elapsedTime = 0f;     //�o�ߎ���
        float startTime = Time.time;    //���̊֐����Ă΂ꂽ�Ƃ��̎��Ԃ̏����l
        float maxVolume = GameManager.GetVolumeBGM();   //�Ō�ɖ߂��ő�l�̉���

        instance.isChangeVolume = true;

        while (elapsedTime < duration)      //�o�ߎ��Ԃ������̃N���X�t�F�[�h���鎞�ԂɒB����܂ŌJ��Ԃ�
        {
            elapsedTime = Time.time - startTime;

            SetAudioMixerVolume(currentParametor, maxVolume * (1 - (elapsedTime / duration)));
            SetAudioMixerVolume(nextParametor, maxVolume * (elapsedTime / duration));

            yield return null;
        }
        instance.isChangeVolume = false;
        ad.Stop();
    }

    public static void PlaySE(SEType seType)        //SE�𗬂�����
    {
        if (instance == null) return;

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)seType;
        if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //�z��O�ւ̃A�N�Z�X�𐧌�����G���[�������
        {
            instance.audioSource[2].PlayOneShot(instance.SEc[index]);
        }
    }

    public static void PlayVOICE(VOICEType voiceType)       //VOICE�𗬂�����
    {
        if (instance == null) return;

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)voiceType;
        if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //�z��O�ւ̃A�N�Z�X�𐧌�����G���[�������
        {
            instance.audioSource[3].PlayOneShot(instance.VOICEc[index]);
        }
    }

    public static void PlayRandomVOICE(string[] randomVoice)        //�����_����VOICE�𗬂�����
    {
        if (instance == null) return;

        string[] voiceTypeNames = Enum.GetNames(typeof(VOICEType));
        instance.audioSource = instance.GetComponents<AudioSource>();

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);  //�V�[�h�l�����݂̎����ɐݒ�
        int randomaizer = UnityEngine.Random.Range(0, randomVoice.Length);

        for (int i = 0; i < voiceTypeNames.Length; i++)
        {
            if (randomVoice[randomaizer] == voiceTypeNames[i])
            {
                if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //�z��O�ւ̃A�N�Z�X�𐧌�����G���[�������
                {
                    instance.audioSource[3].PlayOneShot(instance.VOICEc[i]);
                }
            }
        }
    }

    public static float GetAudioMixerVolume(string parameter)       //�I�[�f�B�I�~�L�T�[�̃{�����[����Get����v���p�e�B���\�b�h
    {
        float value;
        bool result = instance.audioMixer.GetFloat(parameter, out value);

        if (result)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    public static void SetAudioMixerVolume(string parameter, float value)       //�I�[�f�B�I�~�L�T�[�̃{�����[����Set����v���p�e�B���\�b�h
    {
        if (instance == null || instance.audioMixer == null) return;

        value /= 10;
        float volume = Mathf.Clamp(Mathf.Log10(value) * 10f, -80f, 20f);        //�f�V�x���P�ʂ̂��ߑΐ��ŉ��ʌv�Z
        instance.audioMixer.SetFloat(parameter, volume);
    }


}
