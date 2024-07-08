using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public GameObject ui_inven_shop;
    public GameObject uiDialog;
    public GameObject uiGlobalMap;
    public gamePlayPanel gamePlay;
    public gamePausePanel gamePause;
    public GameManager gameMngr;
    public bool stopCtrlWhenUIsActive;
    private void Awake()
    {
        gameMngr = this.GetComponent<GameManager>();
    }
    private void Update()
    {
        stopCtrlWhenUIsActive = (ui_inven_shop.active || uiDialog.active || uiGlobalMap.active);
        if (gamePause.gameObject.active) gameMngr.isStopGame = true;
        else gameMngr.isStopGame = false;

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

    
}
