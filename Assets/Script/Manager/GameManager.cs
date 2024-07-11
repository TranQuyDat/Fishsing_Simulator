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
    public LoadData loadData;
    public SettingData settingData;
    public Scenes curScene;
    public bool isStopGame = false;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        playerCtrl = FindObjectOfType<PlayerController>();
        if(playerCtrl!=null) fishingRodCtrl = playerCtrl.fishingRod;
        mainCamera = FindObjectOfType<cameraFollow>();
    }
    private void Start()
    {

        if (curScene != Scenes.menu && playerCtrl.transform.parent == null) 
            SceneManager.MoveGameObjectToScene(playerCtrl.gameObject, SceneManager.GetActiveScene());
    }
    private void Update()
    {
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
            Debug.Log("Update input");
        }
    }


    public void change2LoadingScene(Scenes oldScene,Scenes nextScene,bool isloadFrSave,DataSave dataSave = null)
    {

        loadData.setLoadData(oldScene,nextScene, TimeMngr.timeOfDay, isloadFrSave, dataSave);
        SceneManager.LoadScene(Scenes.loading.ToString());
    }

}
