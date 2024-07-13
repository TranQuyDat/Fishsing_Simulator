using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SaveLoadGame : MonoBehaviour
{
    public GameManager gameMngr;
    public List<GroupSlotData> grSlots;
    public Dictionary<string, GroupSlotData> dicSlots = new Dictionary<string, GroupSlotData>();
    public PlayerController playerCtrl;

    #region save load values
    public string pathSave ;
    public string dirPath;
    public GameObject prefabSave;
    public Transform parentSave;
    public GameObject prefabLoad;
    public Transform parentLoad;
    public string[] saveFiles;
    public Dictionary<string, slotSave> dicSlotSave = new Dictionary<string, slotSave>();
    public Dictionary<string, slotLoad> dicSlotLoad = new Dictionary<string, slotLoad>();
    public int curLengthFiles;
    #endregion

    private void Awake()
    {
        dirPath = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        foreach(GroupSlotData gr in grSlots)
        {
            dicSlots.Add(gr.name, gr);
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

                string json = File.ReadAllText(filePath);
                DataSave loadData = JsonUtility.FromJson<DataSave>(json);
                if (dicSlotSave.ContainsKey(loadData.name))
                {
                    continue;
                }
                CreateSaveBox(loadData, prefabSave, parentSave);
                CreateLoadBox(loadData, prefabLoad, parentLoad);
            }
        }
    }
    public void CreateSaveBox(DataSave loadData, GameObject prefab,Transform parent)
    {
        
        GameObject savebox = Instantiate(prefab, parent);
        slotSave save = savebox.GetComponent<slotSave>();
        save.nameFile.text = loadData.name;
        save.dateTime.text = loadData.dateTime;
        dicSlotSave.Add(save.nameFile.text, save);
    }
    public void CreateLoadBox(DataSave loadData, GameObject prefab,Transform parent)
    {
        GameObject savebox = Instantiate(prefab, parent);
        slotLoad save = savebox.GetComponent<slotLoad>();
        save.nameFile.text = loadData.name;
        save.dateTime.text = loadData.dateTime;
        dicSlotLoad.Add(save.nameFile.text, save);
    }
    public DataSave getData(string nameFile)
    {
        string json = "";
        if (nameFile.StartsWith("filesave_") && nameFile.EndsWith(".json"))
            json = File.ReadAllText(dirPath + "/" + nameFile);
        else json = File.ReadAllText(dirPath + "/filesave_" + nameFile + ".json");
        DataSave loadData = JsonUtility.FromJson<DataSave>(json);
        return loadData;
    }
    public void save(string name)
    {

        pathSave = dirPath +"/filesave_"+name+".json";

        DataPlayer dataPlayer = playerCtrl.exportData();

        DataRod dataRod = gameMngr.fishingRodCtrl.exportData();

        DataSave saveData = new DataSave(name,gameMngr.TimeMngr.timeOfDay, grSlots, dataPlayer,dataRod);
        
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(pathSave, json);
        //print(saveData.listStore.Count);
        //print(json);
        gameMngr.notify.setUpAndShow("saved",1);
        DataSave save = getData(name);
        if (!dicSlotSave.ContainsKey(name)) return;
        dicSlotSave[name].dateTime.text = save.dateTime;
        dicSlotLoad[name].dateTime.text = save.dateTime;
    }

    public void load(string name)
    {
        //print(Directory.Exists(dirPath));
        if (Directory.Exists(dirPath))
        {
            DataSave loadData = getData(name);
            Scenes scene = (gameMngr.curScene!=Scenes.menu) ? playerCtrl.scenes : Scenes.menu;
            gameMngr.change2LoadingScene(scene, loadData.dataPlayer.scene,true,loadData);
            

            //load Groupslot
            foreach(GroupSlotDataSave gr in loadData.listStore)
            {
                dicSlots[gr.name].items = gr.items;
                foreach (Item it in dicSlots[gr.name].items)
                {
                    it.Load();
                }
            }
            
        }
    }


    public void deleteFileSave(slotSave slot)
    {
        string path = dirPath + "/filesave_" + slot.nameFile.text + ".json";
        File.Delete(path);

        GameObject obj = dicSlotSave[slot.nameFile.text].gameObject;
        dicSlotSave.Remove(slot.nameFile.text);
        Destroy(obj);

        obj = dicSlotLoad[slot.nameFile.text].gameObject;
        dicSlotLoad.Remove(slot.nameFile.text);
        Destroy(obj);
    }
    public void deleteFileSave(slotLoad slot)
    {
        string path = dirPath + "/filesave_" + slot.nameFile.text + ".json";
        File.Delete(path);

        GameObject obj = dicSlotSave[slot.nameFile.text].gameObject;
        dicSlotSave.Remove(slot.nameFile.text);
        Destroy(obj);

        obj = dicSlotLoad[slot.nameFile.text].gameObject;
        dicSlotLoad.Remove(slot.nameFile.text);
        Destroy(obj);
    }
}
