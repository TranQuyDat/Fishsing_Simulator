using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public enum SoundType
{
    //music Loop
    music_mapPort,
    music_mapCity,
    music_mapsea,
    music_Underwater,
    music_menu,
    // soundFx
    sfx_run,
    sfx_caughtFish,
    sfx_click,
    sfx_reeling,
    sfx_notify,

}
public class soundManager : MonoBehaviour
{
    public GameManager gameMngr;
    public AudioSource musicTheme;
    public AudioSource SFX;
    public AudioMixer mixer;
    [Header("Music")]
    public AudioClip music_mapPort;
    public AudioClip music_mapCity;
    public AudioClip music_mapsea;
    public AudioClip music_Underwater;
    public AudioClip music_menu;
    [Header("SFX")]
    public AudioClip sfx_run;
    public AudioClip sfx_caughtFish;
    public AudioClip sfx_reeling;
    public AudioClip sfx_click;
    public AudioClip sfx_notify;

    public Dictionary<SoundType, AudioClip> dicSound;
    public SoundType curMusicScene;

    public float curVolumeMusic;
    public float curVolumeSound;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        dicSound = new Dictionary<SoundType, AudioClip>
        {
            { SoundType.music_menu , music_menu },
            { SoundType.music_mapPort , music_mapPort },
            { SoundType.music_mapCity , music_mapCity },
            { SoundType.music_mapsea , music_mapsea },
            { SoundType.music_Underwater , music_Underwater },
            { SoundType.sfx_run , sfx_run },
            { SoundType.sfx_click , sfx_click },
            { SoundType.sfx_notify , sfx_notify },
            { SoundType.sfx_reeling , sfx_reeling },
            { SoundType.sfx_caughtFish , sfx_caughtFish },
        };
    }
    private void Start()
    {
        mixer.SetFloat("music", 40 * Mathf.Log10(gameMngr.settingData.volume));
        mixer.SetFloat("sound", 40 * Mathf.Log10(gameMngr.settingData.soundFx));
    }

    private void Update()
    {
        if(curVolumeMusic != gameMngr.settingData.volume)
        {
            setVolumeMusic(gameMngr.settingData.volume);
            curVolumeMusic = gameMngr.settingData.volume;
        }
        if (curVolumeSound != gameMngr.settingData.soundFx)
        {
            setVolumeSFX(gameMngr.settingData.soundFx);
            curVolumeSound = gameMngr.settingData.soundFx;
        }
    }

    public void playMusic(SoundType st, bool isloop, float delaytime = 0)
    {
        musicTheme.loop = isloop;
        musicTheme.clip = dicSound[st];
        musicTheme.PlayDelayed(delaytime);
    }
    public void playSFX(SoundType st)
    {
        SFX.PlayOneShot(dicSound[st]);
    }


    public void setVolumeMusic(float v)
    {
        mixer.SetFloat("music", 40 * Mathf.Log10(v));
    }
    public void setVolumeSFX(float v)
    {
        mixer.SetFloat("sound", 40 * Mathf.Log10(v));
    }
}