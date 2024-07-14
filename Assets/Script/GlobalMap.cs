using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{

    GameManager gameManager;
    private void Awake()
    {

        gameManager = FindObjectOfType<GameManager>();
    }
    public DataSave  createData()
    {
        DataPlayer dataPlayer = gameManager.playerCtrl.exportData();
        DataRod dataRod = gameManager.fishingRodCtrl.exportData();
        DataSave dataSave = new DataSave(gameManager.TimeMngr.timeOfDay, dataPlayer, dataRod);
        return dataSave;
    }
    public void btn2city()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();

        if (obj.scenes == Scenes.city)
        {
            gameManager.notify.setUpAndShow("you are here");
            return;
        }
        if (obj.inArea == area.ship)
        {
            gameManager.notify.setUpAndShow("you need out your ship");
            return;
        }

        obj.ischangeScene = true;
        gameManager.playerCtrl.inArea = area.city;
        gameManager.change2LoadingScene(obj.scenes,Scenes.city,false, createData());
    }
    public void btn2Port()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.scenes == Scenes.port)
        {
            gameManager.notify.setUpAndShow("you are here");
            return;
        }
        obj.ischangeScene = true;
        gameManager.playerCtrl.inArea = area.port;
        gameManager.change2LoadingScene(obj.scenes, Scenes.port, false, createData());
    }
    public void btn2Sea1()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship )
        {
            gameManager.notify.setUpAndShow("you are not in Boat ");
            return;
        }
        if(obj.scenes == Scenes.sea1)
        {
            gameManager.notify.setUpAndShow("you are here");
            return;
        }
        GameObject parent = obj.transform.parent.gameObject;
        if(parent.name != "speed_BoatV1" && parent.name != "scout_BoatV1")
        {
            gameManager.notify.setUpAndShow("you need 'Speed Boat' or 'Scout Boat' ");
            return;
        }
        obj.ischangeScene = true;

        gameManager.playerCtrl.inArea = area.ship;
        gameManager.change2LoadingScene(obj.scenes, Scenes.sea1, false, createData());
    }
    public void btn2Sea2()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship)
        {
            gameManager.notify.setUpAndShow("you are not in ship");
            return;
        }

        if (obj.scenes == Scenes.sea2)
        {
            gameManager.notify.setUpAndShow("you are here");
            return;
        }
        GameObject parent = obj.transform.parent.gameObject;
        if (parent.name != "scout_BoatV1")
        {
            gameManager.notify.setUpAndShow("you need 'Scout Boat' ");
            return;
        }
        obj.ischangeScene = true;

        gameManager.playerCtrl.inArea = area.ship;
        gameManager.change2LoadingScene(obj.scenes, Scenes.sea2, false, createData());
    }



}
