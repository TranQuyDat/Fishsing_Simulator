using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public gamePlayPanel gamePlay;
    public gamePausePanel gamePause;
    public GameManager gameMngr;
    private void Awake()
    {
        gameMngr = this.GetComponent<GameManager>();
    }
    private void Update()
    {
        setActive(gamePlay.fishingUI, gameMngr.boatCtrl.isFishing);
    }

    public void setActive(GameObject ui, bool b)
    {
        ui.SetActive(b);
    }
}
