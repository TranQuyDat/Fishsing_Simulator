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
    public void btn2city()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea == area.ship)
        {
            gameManager.notify.setUpAndShow("you need out your ship");
            return;
        }

        obj.ischangeScene = true;
        gameManager.change2LoadingScene(obj.scenes,Scenes.city);
    }
    public void btn2Port()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        obj.ischangeScene = true;

        gameManager.change2LoadingScene(obj.scenes, Scenes.port);
    }
    public void btn2Sea1()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship )
        {
            gameManager.notify.setUpAndShow("you are not in Boat ");
            return;
        }
        GameObject parent = obj.transform.parent.gameObject;
        if(parent.name != "speed_BoatV1" && parent.name != "scout_BoatV1")
        {
            gameManager.notify.setUpAndShow("you need 'Speed Boat' or 'Scout Boat' ");
            return;
        }
        obj.ischangeScene = true;

        gameManager.change2LoadingScene(obj.scenes, Scenes.sea1);
    }
    public void btn2Sea2()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship)
        {
            gameManager.notify.setUpAndShow("you are not in ship");
            return;
        }

        GameObject parent = obj.transform.parent.gameObject;
        if (parent.name != "scout_BoatV1")
        {
            gameManager.notify.setUpAndShow("you need 'Scout Boat' ");
            return;
        }
        obj.ischangeScene = true;

        gameManager.change2LoadingScene(obj.scenes, Scenes.sea2);
    }



}
