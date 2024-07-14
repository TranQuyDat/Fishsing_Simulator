using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameUiManager uiMngr;
    public fishManager fishMngr;
    public ShipManager shipMngr;
    public TimeManager TimeMngr;
    public soundManager soundMngr;
    public PlayerController playerCtrl;
    public fishingRodController fishingRodCtrl;
    public inventory iv;
    public GameObject instructionBTN;
    public cameraFollow mainCamera;
    public Notifycation notify;
    public SaveLoadGame saveLoadGame;
    public GlobalMap globalMap;
    public Transform surfaceWater;
    public LoadData loadData;
    public SettingData settingData;
    public path pathdata;
    public Scenes curScene;
    public bool isStopGame = false;
    public static Dictionary<int, string> dicPath = new Dictionary<int, string>();
    private void Awake()
    {
        initDicPath();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        playerCtrl = FindObjectOfType<PlayerController>();
        if(playerCtrl!=null) fishingRodCtrl = playerCtrl.fishingRod;
        mainCamera = FindObjectOfType<cameraFollow>();
    }
    private void Start()
    {   
        if (curScene != Scenes.loading)
        {
            soundMngr.playMusic(soundMngr.curMusicScene, true);
        }
        if (curScene != Scenes.menu && playerCtrl.transform.parent == null) 
            SceneManager.MoveGameObjectToScene(playerCtrl.gameObject, SceneManager.GetActiveScene());
    }
    private void Update()
    {
        updateSound();
        if (curScene == Scenes.menu) return;
        if (isStopGame) Time.timeScale = 0;
        else Time.timeScale = 1;

        if (playerCtrl == null)
        {
            playerCtrl = FindObjectOfType<PlayerController>();
            fishingRodCtrl = playerCtrl.fishingRod;
        }

        if (playerCtrl != null && saveLoadGame.playerCtrl == null)
        {
            playerCtrl.resetInput();
            saveLoadGame.playerCtrl = playerCtrl;
            //Debug.Log("Update input");
        }
    }

    public void updateSound()
    {
        if (surfaceWater == null) return;
        if (mainCamera.transform.position.y > surfaceWater.position.y
            && soundMngr.musicTheme.clip != soundMngr.dicSound[soundMngr.curMusicScene])
        {
            soundMngr.playMusic(soundMngr.curMusicScene, true);
        }
        else if (mainCamera.transform.position.y < surfaceWater.position.y
            && soundMngr.musicTheme.clip != soundMngr.dicSound[SoundType.music_Underwater])
        {

            soundMngr.playMusic(SoundType.music_Underwater, true);
        }
    }
    public void initDicPath()
    {
        if (dicPath.Count > 0) return;
        foreach(StringPair p in pathdata.listPath)
        {
            if(p.key!=null)
                dicPath.Add(p.key.GetInstanceID(), p.value);
        }
    }

    public static string getPath(Object obj)
    {
        if (obj == null) return null;
        int k = obj.GetInstanceID();
        return dicPath[k];
    }
    public void change2LoadingScene(Scenes oldScene,Scenes nextScene,bool isloadFrSave,DataSave dataSave = null)
    {

        loadData.setLoadData(oldScene,nextScene, TimeMngr.timeOfDay, isloadFrSave, dataSave);
        SceneManager.LoadScene(Scenes.loading.ToString());
    }

}
