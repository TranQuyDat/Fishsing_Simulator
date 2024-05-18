using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayPanel : MonoBehaviour
{
    public GameObject fishingUI;
    public GameManager gameMngr;
    public buttons btn;

    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        btn.editImg.setFillAmount(1 / 4);
    }
    private void Update()
    {
        if(!Input.GetMouseButtonDown(0)) btn.editImg.AddFillAmount(-(1f/15));
    }

    #region button

    public void btnFishing()
    {
        btn.editImg.AddFillAmount(2f);
    }
    public void btnFishingcast()
    {
        gameMngr.player.cur_action = Action.fishing_cast;
    }

    #endregion
}
