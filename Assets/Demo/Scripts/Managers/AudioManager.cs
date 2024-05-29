using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;     //静的インスタンス化

    public enum BGMType     //オーディオクリップの配列を見やすくするための列挙型(BGM)
    {
        None,
        House,
        Store,
        ModeSelect,
        Game,
        Result,
        POPList
    }

    public enum SEType      //オーディオクリップの配列を見やすくするための列挙型(SE)
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

    public enum VOICEType   //オーディオクリップの配列を見やすくするための列挙型(VOICE)
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
        Ikkuyo_1,             // よしやるぞ！
        Ikkuyo_2,             // いらっしゃいませ！
        HitagiClub,           // テストです
        CountDown1,           // いち
        CountDown2,           // に
        CountDown3,           // さん
        Start,                // スタート！
        Hajimeruyo,           // はじまるよ
        Yattane,              // よくできました
        Rakusyo_,             // さっすがぁ
        Sugoine,              // わーすごい
        Matane,               // またね
        Wa_iwa_i,             // いいかんじ
        Yattayatta,           // やったぁ
        Omedetou,             // すごいねぇ
        O_omedetou,           // 次はできるよ
        Ganbatte,             // 大丈夫、なんども
        Ganbareganbare,       // 今度こそできるよ
        Dodousiyou,           // だめみたい
        Bakanisitennoka,      // もう少し頑張ろうね
        Madaganbareru,        // まいったなぁ

        //ここまでは撫子のボイスで作ってたため他の場所での列挙要素名も書き換えられてません
        //ここからは新ボイスなので変更する必要はありません

        Goyukkuridozo,
        Hawawa,
        Ready_Go,
        Naniwotsukuro,
        Ganbarimasu,
        Isoide,
        Mataaeruyone,
        Arigatougozaimashita,
    }*/

    [SerializeField] private AudioClip[] BGMc;         //BGMClipの配列
    [SerializeField] private AudioClip[] SEc;          //SEClipの配列
    [SerializeField] private AudioClip[] VOICEc;       //VOICEClipの配列

    [SerializeField] private AudioMixer audioMixer;    //使うオーディオミキサーを定義

    private AudioSource[] audioSource;                          //随時使うオーディオソースを変更

    private bool isChangeVolume = false;                        //現在音量変更中かどうか

    private void Awake()            //startより前にオーディオマネージャプレファブをシングルトン化
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
        instance.audioSource = GetComponents<AudioSource>();    //オーディオソースコンポーネントを事前に取得
    }

    private void Update()       //常時ゲームマネージャからの音を取得して音を設定
    {
        if (!instance.isChangeVolume)
        {
            SetAudioMixerVolume("BGM1", GameManager.GetVolumeBGM());
            SetAudioMixerVolume("BGM2", GameManager.GetVolumeBGM());
        }
        SetAudioMixerVolume("SE", GameManager.GetVolumeSE());
        SetAudioMixerVolume("VOICE", GameManager.GetVolumeVOICE());
    }

    public static void PlayBGM(BGMType bgmType)         //BGMを流す処理
    {
        if (instance == null) return;                               //エラー回避

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)bgmType;

        if (instance.audioSource[0].isPlaying)                      //オーディオソース0番目を使用していたら、0番目のクリップ変更
        {
            instance.audioSource[0].clip = instance.BGMc[index];
            instance.audioSource[0].Play();
        }
        else if (instance.audioSource[1].isPlaying)                 //オーディオソース0番目を使用していたら、番目のクリップ変更
        {
            instance.audioSource[1].clip = instance.BGMc[index];
            instance.audioSource[1].Play();
        }
        else                                                        //オーディオソース使用してなかったら0番目のクリップ変更
        {
            instance.audioSource[0].clip = instance.BGMc[index];
            instance.audioSource[0].Play();
        }
    }

    public static void CrossFadeBGM(BGMType bgmType)    //BGMのクロスフェード処理
    {
        if (instance == null) return;                               //エラー回避

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
    //クロスフェード処理(クロスフェードする時間, 現在のオーディオミキサーのパラメータ, 次のオーディオミキサーのパラメータ, 使用するオーディオソース)
    {
        float elapsedTime = 0f;     //経過時間
        float startTime = Time.time;    //この関数が呼ばれたときの時間の初期値
        float maxVolume = GameManager.GetVolumeBGM();   //最後に戻す最大値の音量

        instance.isChangeVolume = true;

        while (elapsedTime < duration)      //経過時間が引数のクロスフェードする時間に達するまで繰り返し
        {
            elapsedTime = Time.time - startTime;

            SetAudioMixerVolume(currentParametor, maxVolume * (1 - (elapsedTime / duration)));
            SetAudioMixerVolume(nextParametor, maxVolume * (elapsedTime / duration));

            yield return null;
        }
        instance.isChangeVolume = false;
        ad.Stop();
    }

    public static void PlaySE(SEType seType)        //SEを流す処理
    {
        if (instance == null) return;

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)seType;
        if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //配列外へのアクセスを制限するエラー回避処理
        {
            instance.audioSource[2].PlayOneShot(instance.SEc[index]);
        }
    }

    public static void PlayVOICE(VOICEType voiceType)       //VOICEを流す処理
    {
        if (instance == null) return;

        instance.audioSource = instance.GetComponents<AudioSource>();
        int index = (int)voiceType;
        if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //配列外へのアクセスを制限するエラー回避処理
        {
            instance.audioSource[3].PlayOneShot(instance.VOICEc[index]);
        }
    }

    public static void PlayRandomVOICE(string[] randomVoice)        //ランダムにVOICEを流す処理
    {
        if (instance == null) return;

        string[] voiceTypeNames = Enum.GetNames(typeof(VOICEType));
        instance.audioSource = instance.GetComponents<AudioSource>();

        UnityEngine.Random.InitState(DateTime.Now.Millisecond);  //シード値を現在の時刻に設定
        int randomaizer = UnityEngine.Random.Range(0, randomVoice.Length);

        for (int i = 0; i < voiceTypeNames.Length; i++)
        {
            if (randomVoice[randomaizer] == voiceTypeNames[i])
            {
                if (instance.audioSource.Length > 2 && instance.audioSource[2] != null)     //配列外へのアクセスを制限するエラー回避処理
                {
                    instance.audioSource[3].PlayOneShot(instance.VOICEc[i]);
                }
            }
        }
    }

    public static float GetAudioMixerVolume(string parameter)       //オーディオミキサーのボリュームをGetするプロパティメソッド
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

    public static void SetAudioMixerVolume(string parameter, float value)       //オーディオミキサーのボリュームをSetするプロパティメソッド
    {
        if (instance == null || instance.audioMixer == null) return;

        value /= 10;
        float volume = Mathf.Clamp(Mathf.Log10(value) * 10f, -80f, 20f);        //デシベル単位のため対数で音量計算
        instance.audioMixer.SetFloat(parameter, volume);
    }


}
