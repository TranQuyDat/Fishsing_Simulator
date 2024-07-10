using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class DataGame
{
    public List<GroupSlotData> grSlDatas;
    public List<ItemData> shipDatas;
    public Dictionary<string, GroupSlotData> dicgrSlData = new Dictionary<string, GroupSlotData>();

    public void resetData( List<GroupSlotDataSave> listStore)
    {
        //reset group slot data
        foreach (GroupSlotDataSave gr in listStore)
        {
            dicgrSlData[gr.name].items = gr.items;
            foreach (Item it in dicgrSlData[gr.name].items)
            {
                it.Load();
            }
        }
        //reset Ship Status
        foreach(ItemData it in shipDatas)
        {
            if (it.name == "boat")
            {
                it.status = Status.equiped;
                continue;
            }
            it.status = Status.Lock;
        }
    }
}
public class Menu : MonoBehaviour
{
    public DataSave newGameData;
    public GameManager gameMngr;
    public SettingUI settingUI;

    public DataGame data;
    private void Awake()
    {
        Time.timeScale = 1;
        foreach(GroupSlotData grslDT in data.grSlDatas)
        {
            data.dicgrSlData.Add(grslDT.name, grslDT);
        }
    }

    private void Update()
    {
        updateUIsetting();
    }

    public void updateUIsetting()
    {
        if (settingUI.sl_Volume.value == gameMngr.settingData.volume &&
             settingUI.sl_SFX.value == gameMngr.settingData.soundFx) return;

        if (!settingUI.ui.active )
        {
            settingUI.updateVolume(gameMngr.settingData);
        }
        else
        {
            settingUI.saveVolume(gameMngr.settingData);
        }
    }
    public void btn_continue()
    {
        string dirPath = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(dirPath)) return;
        DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
        FileInfo[] saveFiles = directoryInfo.GetFiles( "*.json");
        if (saveFiles.Length == 0) return;
        FileInfo lastestFile = saveFiles.OrderByDescending(f => f.LastWriteTime).First();
        print(lastestFile.Name);
        gameMngr.saveLoadGame.load(lastestFile.Name);
    }
    public void btn_NewGame()
    {
        newGameData.dateTime = Time.deltaTime.ToString("yyyy-MM-dd HH:mm:ss");
        gameMngr.change2LoadingScene(Scenes.menu, Scenes.city,false,newGameData);

        data.resetData(newGameData.listStore);
    }

    public void btn_Quit()
    {
        Application.Quit();
    }
}
