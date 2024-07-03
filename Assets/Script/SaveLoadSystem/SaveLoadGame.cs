using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveLoadGame : MonoBehaviour
{
    public GameManager gameMngr;
    public inventory iv;
    public List<GroupSlotData> grSlots;
    public Transform player;

    public string pathSave ;
    string dirPath;
    public GameObject prefabSave;
    public Transform parentSave;
    public GameObject prefabLoad;
    public Transform parentLoad;
    public string[] saveFiles;
    public int curLengthFiles;
    private void Awake()
    {
        dirPath = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
    }
    private void Update()
    {
        if (Directory.Exists(dirPath) ) 
            saveFiles = Directory.GetFiles(dirPath, "*.json");
        if (curLengthFiles !=saveFiles.Length)
        {
            curLengthFiles = saveFiles.Length;
            ShowAllFileSave();
        }
    }
    public void ShowAllFileSave()
    {
        if (Directory.Exists(dirPath))
        {
            saveFiles = Directory.GetFiles(dirPath, "*.json");
            foreach (string filePath in saveFiles)
            {
                
                CreateSaveBox(filePath, prefabSave, parentSave);
                CreateLoadBox(filePath, prefabLoad, parentLoad);
            }
        }
    }
    public void CreateSaveBox(string filePath,GameObject prefab,Transform parent)
    {
        string json = File.ReadAllText(filePath);
        SaveData loadData = JsonUtility.FromJson<SaveData>(json);
        GameObject savebox = Instantiate(prefab, parent);
        slotSave save = savebox.GetComponent<slotSave>();
        save.nameFile.text = loadData.name;
        save.dateTime.text = loadData.dateTime;
    }
    public void CreateLoadBox(string filePath,GameObject prefab,Transform parent)
    {
        string json = File.ReadAllText(filePath);
        SaveData loadData = JsonUtility.FromJson<SaveData>(json);
        GameObject savebox = Instantiate(prefab, parent);
        slotLoad save = savebox.GetComponent<slotLoad>();
        save.nameFile.text = loadData.name;
        save.dateTime.text = loadData.dateTime;
    }
    public void save(string name, Scenes scene)
    {

        pathSave = dirPath +"/filesave_"+name+".json";
        SaveData saveData = new SaveData(name, player.position, scene,iv, grSlots);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(pathSave, json);
    }

    public void load(string name)
    {
        pathSave = dirPath + "filesave_" + name + ".json";

        if(Directory.Exists(pathSave))
        {
            string json = File.ReadAllText(pathSave);
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);
            player.position = loadData.posPlayer;
            gameMngr.changeScene(loadData.scene);
            iv = loadData.iv;
            for (int i = 0; i < grSlots.Count; i++)
            {
                if (loadData.dicStore.TryGetValue(grSlots[i].name, out GroupSlotData loadedGrSlot))
                {
                    grSlots[i] = loadedGrSlot;
                }
            }
        }
    }
}
