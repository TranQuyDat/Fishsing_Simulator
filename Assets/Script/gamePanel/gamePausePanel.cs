using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GameSaveUI
{
    public GameObject ui;
    public InputField NameField;
    public void setNameField(string text)
    {
        NameField.text = text;
    }
}
[System.Serializable]
public class GameLoadUI
{
    public GameObject ui;
}
public class gamePausePanel : MonoBehaviour
{
    public GameManager gameMngr;
    public GameSaveUI gameSaveUI;
    public GameLoadUI gameLoadUI;
    public SettingUI settingUI;
    // Start is called before the first frame update
    private void Awake()
    {
        gameMngr = GameObject.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUIsetting();
    }
    public void updateUIsetting()
    {
        if (settingUI.sl_Volume.value == gameMngr.settingData.volume &&
             settingUI.sl_SFX.value == gameMngr.settingData.soundFx) return;

        if (!settingUI.ui.active)
        {
            settingUI.updateVolume(gameMngr.settingData);
        }
        else
        {
            settingUI.saveVolume(gameMngr.settingData);
        }
    }
    public void selectBoxSave(slotSave slot)
    {
        gameSaveUI.setNameField(slot.nameFile.text);
    }
    public void btnSave(InputField inputfield)
    {
        gameMngr.saveLoadGame.save(inputfield.text);
    }

    public void btnReturn2MainMenu()
    {
        SceneManager.LoadScene(Scenes.menu.ToString());
    }
}
