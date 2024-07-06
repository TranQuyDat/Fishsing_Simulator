using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[Serializable]
public class DataPlayer
{
    public Vector3 pos;
    public int coins;
    public string parentName;
    public Vector3 posParent;
    public Vector3 rotation;
    public Scenes scene;
    public Action action;
    public area inArea;
    public bool canMove;
    public DataPlayer(Vector3 pos,Vector3 rotation, int coins, Scenes scene, area inArea, Action action = Action.idle ,
       bool canMove =true , string parentName="",Vector3 posParent=default)
    {
        this.pos = pos;
        this.rotation = rotation;
        this.coins = coins;
        this.scene = scene;
        this.parentName = parentName;
        this.posParent = posParent;
        this.action = action;
        this.inArea = inArea;
        this.canMove = canMove;
    }
}

[Serializable]
public class DataSave
{
    public string name;
    public string dateTime;
    public List<GroupSlotDataSave>  listStore= new List<GroupSlotDataSave>();

    public DataPlayer dataPlayer;
    public DataSave(string name , List<GroupSlotData> grSlots , DataPlayer dataPlayer)
    {
        this.name = name;
        dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.dataPlayer = dataPlayer;
        
        
        listStore.Clear();
        foreach (GroupSlotData grsl in grSlots)
        {
            listStore.Add(new GroupSlotDataSave(grsl));
        }
    }
  


}
