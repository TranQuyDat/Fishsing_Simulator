using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameManager gameMngr;
    public SettingUI settingUI;
    private void Awake()
    {
        Time.timeScale = 1;
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
        gameMngr.saveLoadGame.load(gameMngr.loadData.newNameSave);
    }
    public void btn_NewGame()
    {
        gameMngr.change2LoadingScene(Scenes.menu, Scenes.city);
    }

    public void btn_Quit()
    {
        Application.Quit();
    }
}
