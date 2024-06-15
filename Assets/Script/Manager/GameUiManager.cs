using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public gamePlayPanel gamePlay;
    public gamePausePanel gamePause;
    public GameManager gameMngr;

    public bool ischangeState;
    private void Awake()
    {
        gameMngr = this.GetComponent<GameManager>();
    }
    private void Update()
    {
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
