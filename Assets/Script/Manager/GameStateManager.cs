using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public State curState;
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
        swGameState();
    }

    public void swGameState()
    {
        if (!ischangeState) return;
        gamePlay.gameObject.SetActive(curState==State.gamePlay);
        gamePause.gameObject.SetActive(curState == State.gamePause);
        ischangeState = false;
    }
    
}
