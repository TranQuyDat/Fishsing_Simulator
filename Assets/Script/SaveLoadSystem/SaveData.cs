using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData
{
    public string name;
    public string dateTime;
    public Vector3 posPlayer;
    public Scenes scene;
    public inventory iv;
    public Dictionary<string, GroupSlotData> dicStore= new Dictionary<string, GroupSlotData>();

    public SaveData(string name,Vector3 posPlayer,Scenes scene,inventory iv,List<GroupSlotData> grSlots)
    {
        this.name = name;
        dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.posPlayer = posPlayer;
        this.scene = scene;
        this.iv = iv;
        dicStore.Clear();
        foreach(var grsl in grSlots)
        {
            dicStore.Add(grsl.name, grsl);
        }
    }

}
