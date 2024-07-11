using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public enum SoundType
{
    //music Loop
    music_gamePlay,
    music_menu,
    // soundFx
    sfx_run,
    sfx_caughtFish,

}
public class soundManager : MonoBehaviour
{
    public GameManager gameMngr;
    public AudioSource musicTheme;
    public AudioSource SFX;
    public AudioMixer mixer;
    [Header("Music")]
    public AudioClip music_gamePlay;
    public AudioClip music_menu;
    [Header("SFX")]
    public AudioClip sfx_run;
    public AudioClip sfx_caughtFish;

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
            { SoundType.music_gamePlay , music_gamePlay },
            { SoundType.sfx_run , sfx_run },
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