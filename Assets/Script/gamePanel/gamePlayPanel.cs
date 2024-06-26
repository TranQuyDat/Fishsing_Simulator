using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePlayPanel : MonoBehaviour
{
    public GameObject fishingUI;
    public GameObject fishInfoUI;
    public GameManager gameMngr;
    public buttons btn;

    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        btn.editImg.setFillAmount(0);
    }
    private void Update()
    {
        if(fishingUI != null && gameMngr.shipMngr != null)
            setActive(fishingUI, gameMngr.shipMngr.curShipCtrl.isFishing);
        if (fishInfoUI != null && gameMngr.fishingRodCtrl != null) 
            setActive(fishInfoUI, gameMngr.fishingRodCtrl.wasCaughtFish);
        updateAmount();
    }


    public void updateAmount()
    {
        bool b = gameMngr == null || gameMngr.fishMngr == null || !fishingUI.active;
        if (b) return;
        if (gameMngr.fishMngr.theLuckyFish != null && gameMngr.fishMngr.theLuckyFishAI.acFish == Action.ateBait &&
            btn.editImg.imgValue.fillAmount >= 0 && btn.editImg.imgValue.fillAmount < 1f)
        {
            btn.editImg.setFillAmount(1 - Mathf.Round((gameMngr.fishMngr.dis / gameMngr.fishMngr.maxdis) * 100f) * 0.01f);
        }
    }


    #region button
    public void btnFishing()
    {
        gameMngr.fishingRodCtrl.isPull = true;
        if (btn.editImg.imgValue.fillAmount >= 0.95f)
        {
            btn.editImg.setFillAmount(1);
        }
    }
    public void btnFishingcast()
    {
        if (gameMngr.playerCtrl.cur_action == Action.fishing_cast
         || gameMngr.playerCtrl.cur_action == Action.fishing_reel) return;
        gameMngr.playerCtrl.cur_action = Action.fishing_cast;
    }


    #endregion


    public void setActive(GameObject ui, bool b)
    {
        if (ui == null ||
            (ui.active && b) ||
            (!ui.active && !b)
            ) return;
        ui.SetActive(b);
    }
}
