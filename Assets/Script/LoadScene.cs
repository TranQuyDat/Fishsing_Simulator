using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public LoadData loadData;
    public GameManager gameMngr;
    bool isloaded = false;
    private void Update()
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        if (players.Length == 1 && !isloaded && gameMngr.playerCtrl !=null)
        {
            gameMngr.playerCtrl.rb.isKinematic = true;
            if (!loadData.isloadFrSave)
            {
                loadScene();
            }
            else
            {
                loadFromSave();
            }
            isloaded = true;
        }
    }
    public void loadScene()
    {
        bool b = loadData.oldScene == Scenes.sea1 || loadData.oldScene == Scenes.sea2;
        if (loadData.nextScene == Scenes.sea1 || loadData.nextScene == Scenes.sea2 ||b)
        {
            BoatController parent = FindObjectOfType<BoatController>();
            parent.transform.parent = null;

            gameMngr.playerCtrl.inArea = area.ship;
            gameMngr.playerCtrl.transform.position = parent.posSit.position;
            gameMngr.playerCtrl.canMove = false;
            gameMngr.playerCtrl.cur_action = Action.sitting;
            DontDestroyOnLoad(parent.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameMngr.playerCtrl.gameObject);
        }
        gameMngr.playerCtrl.ischangeScene = true;
        SceneManager.LoadSceneAsync(loadData.nextScene.ToString());
    }
    public void loadFromSave()
    {
        //add player to ship if have ship
        DataPlayer dataPlayer = loadData.dataSave.dataPlayer;
        DataRod dataRod = loadData.dataSave.datagRod;
        if (dataPlayer.parentName != "")
        {
            Transform parent = GameObject.Find(dataPlayer.parentName).transform;
            parent.parent = null;
            parent.position = dataPlayer.posParent;
            gameMngr.playerCtrl.transform.SetParent(parent);
            DontDestroyOnLoad(parent.gameObject);
        }
        else DontDestroyOnLoad(gameMngr.playerCtrl.gameObject);
        loadData.time = loadData.dataSave.timeIngame;
        gameMngr.playerCtrl.importData(dataPlayer);
        gameMngr.fishingRodCtrl.importData(dataRod);
        SceneManager.LoadSceneAsync(loadData.nextScene.ToString());
    }


}
