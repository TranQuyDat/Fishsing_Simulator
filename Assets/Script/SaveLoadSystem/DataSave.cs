using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[Serializable]
public class DataRod
{
    public String tagBait;
    public string meshBaitPath;
    public string matBaitPath;
    public DataRod(string tagBait, Mesh meshBait, Material matBait)
    {
        this.tagBait = tagBait;
        this.meshBaitPath = DataSave.savePathIt(meshBait) ;
        this.matBaitPath = DataSave.savePathIt(matBait);
    }
}

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
    public float timeIngame;
    public List<GroupSlotDataSave>  listStore= new List<GroupSlotDataSave>();

    public DataPlayer dataPlayer;
    public DataRod datagRod;
    public DataSave(string name , float timeIngame, List<GroupSlotData> grSlots , DataPlayer dataPlayer, DataRod datagRod)
    {
        this.name = name;
        dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.timeIngame = timeIngame;
        this.dataPlayer = dataPlayer;
        this.datagRod = datagRod;
        
        listStore.Clear();
        
        foreach (GroupSlotData grsl in grSlots)
        {
            listStore.Add(new GroupSlotDataSave(grsl));
        }
    }
  
    public static T GetDtFrPath<T>(string p) where T : UnityEngine.Object
    {
        //"Assets/Resources/"
        T obj = Resources.Load<T>(p);
        if (p.StartsWith("Assets/Resources/"))
        {
            string[] pathSplit = p.Split("."); ;
            string path = pathSplit[0].Substring("Assets/Resources/".Length);
            obj = Resources.Load<T>(path);
            Debug.Log(path + " | obj:" + obj);
        }
        if(obj == null)
        {
            Debug.Log("not find path" + p);
        }
        return obj;
    }
    public static T GetDtFrPathTsprite<T>(string p) where T : UnityEngine.Object
    {
       //"Assets/Resources/img/fish.png_fish_0"
        T obj = Resources.Load<T>(p);
        if (p.StartsWith("Assets/Resources/")) {
            string[] pathSplit = p.Split("_");
            int id = int.Parse(pathSplit[2]);
            pathSplit = pathSplit[0].Split(".");
            string path = pathSplit[0].Substring("Assets/Resources/".Length);
            obj = Resources.LoadAll<T>(path)[id];
        }
        if(obj == null)
        {
            Debug.Log("not find path" + p);
        }
        return obj;
    }

    public static string savePathIt(UnityEngine.Object obj)
    {
        string path = AssetDatabase.GetAssetPath(obj);
        if (String.IsNullOrEmpty(path))
        {
            Debug.Log("path is null or empty");
        }
        return path;
    }

}
