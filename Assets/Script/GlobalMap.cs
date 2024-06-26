using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    public void btn2city()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea == area.ship) return;
        obj.ischangeScene = true;
        DontDestroyOnLoad(obj.gameObject);
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.changeScene(Scenes.city);
    }public void btn2Port()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        obj.ischangeScene = true;
        DontDestroyOnLoad(obj.gameObject);
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.changeScene(Scenes.port);
    }
    public void btn2Sea1()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship) return;
        obj.ischangeScene = true;

        GameObject parent = obj.transform.parent.gameObject;
        parent.transform.parent = null;
        DontDestroyOnLoad(parent);
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.changeScene(Scenes.sea1);
    }
    public void btn2Sea2()
    {
        PlayerController obj = FindObjectOfType<PlayerController>();
        if (obj.inArea != area.ship) return;
        obj.ischangeScene = true;

        GameObject parent = obj.transform.parent.gameObject;
        parent.transform.parent = null;
        DontDestroyOnLoad(parent);
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.changeScene(Scenes.sea2);
    }



}
