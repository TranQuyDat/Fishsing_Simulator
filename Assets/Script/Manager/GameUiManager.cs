using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class SettingUI
{
    public GameObject ui;
    public Slider sl_Volume;
    public Slider sl_SFX;
    public void updateVolume(SettingData data)
    {
        sl_Volume.value = data.volume;
        sl_SFX.value = data.soundFx;
    }

    public void saveVolume(SettingData data)
    {
        data.setVLandFX(sl_Volume.value, sl_SFX.value);
    }
}
public class GameUiManager : MonoBehaviour
{
    public GameObject ui_inven_shop;
    public GameObject uiDialog;
    public GameObject uiGlobalMap;
    public gamePlayPanel gamePlay;
    public gamePausePanel gamePause;
    public GameManager gameMngr;
    public bool stopCtrlWhenUIsActive;


    public int countClick = 0;
    public bool isdoubleClick = false;
    private void Awake()
    {
        gameMngr = this.GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameMngr.curScene == Scenes.menu) return;
            
        stopCtrlWhenUIsActive = (ui_inven_shop.active || uiDialog.active || uiGlobalMap.active);
        
        if (gamePause.gameObject.active) gameMngr.isStopGame = true;
        else gameMngr.isStopGame = false;
        
        gamePlay.uiTimesOfDay.swtIconTimeOfDay(gameMngr.TimeMngr.timeOfDay);
        if (ui_inven_shop != null)
        {
            setActiveChildOfUI_if(ui_inven_shop, false);
        }
        
        
    }
    public void setActiveChildOfUI_if(GameObject ui,bool b)
    {
        if (ui.active != b) return;
        for (int i = 0; i < ui.transform.childCount; i++)
        {
            ui.transform.GetChild(i).gameObject.SetActive(b);
        }
    }
    public void btnCloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }
    public void btnOpentUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void btnLoad(slotLoad slot)
    {
        StartCoroutine(checDoubleClick());
        if (!isdoubleClick)
        {
            return;
        }
        print("click" + slot.name);
        gameMngr.saveLoadGame.load(slot.nameFile.text);
    }

    IEnumerator checDoubleClick()
    {
        countClick += 1;
        if (countClick > 1)
        {
            isdoubleClick = true;
            countClick = 0;
            StopCoroutine(checDoubleClick());
        }
        yield return new WaitForSeconds(0.5f);
        if (countClick <= 1)
        {
            isdoubleClick = false;
            countClick = 0;
        }

    }
}
