using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class FishingUI
{
    public GameObject ui;
    public Animation ani;
    public Button btnfishingcast;
    public void btnCastRod()
    {
        ani = ani.GetComponent<Animation>();
        ani.Play("uiFishingOpen");
        btnfishingcast.interactable = false;
    }

    public void btnStopFishing()
    {
        ani.Play("uiFishingClose");
        btnfishingcast.interactable = true;
    }
}
[System.Serializable]
public class FishInfoUI 
{
    public GameObject ui;
    public TextMeshProUGUI txt_name;
    public TextMeshProUGUI txt_inch;
    public Image imgFish;
    ItemData fishdata;
    public void setUp(ItemData fishDT)
    {
        fishdata = fishDT;
        txt_name.text = fishDT.nameItem;
        imgFish.sprite = fishDT.imgItem;
    }

    public void btn_close(inventory iv)
    {
        iv.addItem(fishdata);
        fishdata = null;
        ui.SetActive(false);
    }
}
public class gamePlayPanel : MonoBehaviour
{
    public FishingUI fishingUI;
    public FishInfoUI fishInfoUI;
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
        if(fishingUI != null && gameMngr.shipMngr != null && gameMngr.shipMngr.curShipCtrl !=null)
            setActive(fishingUI.ui, gameMngr.shipMngr.curShipCtrl.isFishing);
        if (fishInfoUI != null && gameMngr.fishingRodCtrl != null) 
            setActive(fishInfoUI.ui, gameMngr.fishingRodCtrl.wasCaughtFish && gameMngr.fishingRodCtrl.fish != null);
        updateAmount();
        if (fishInfoUI.ui.active)
        {
            fishInfoUI.setUp(gameMngr.fishingRodCtrl.fish.fishData);
        }

        if (gameMngr.fishingRodCtrl != null && gameMngr.fishingRodCtrl.wasCaughtFish)
        {
            btnStopFishing();
        }
    }

    public void updateAmount()
    {
        bool b = gameMngr == null || gameMngr.fishMngr == null || !fishingUI.ui.active || !gameMngr.fishingRodCtrl.isfishing;
        if (b)
        {
            btn.editImg.setFillAmount(0);
            return;
        }
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
    public void btnFishingcast( )
    {
        if (gameMngr.playerCtrl.cur_action == Action.fishing_cast
         || gameMngr.playerCtrl.cur_action == Action.fishing_reel) return;
        gameMngr.playerCtrl.cur_action = Action.fishing_cast;
        fishingUI.btnCastRod();
        print(fishingUI.ani["uiFishingOpen"]);
    }

    public void btnStopFishing()
    {
        fishingUI.btnStopFishing();
        gameMngr.fishingRodCtrl.Reset();
        gameMngr.fishMngr.Reset();
    }

    public void btnUpDownSurfaceWater()
    {
        GameObject target ;
        if (gameMngr.mainCamera.isCameraUp)
        {
            target = gameMngr.fishingRodCtrl.hook.gameObject;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("posCamera");
        }
        gameMngr.mainCamera.changeTarget(target);
    }

    public void btnCloseFishInfoUI()
    {
        fishInfoUI.btn_close(gameMngr.iv);
        gameMngr.fishingRodCtrl.fish.destroy();
        gameMngr.fishMngr.Reset();
        gameMngr.fishingRodCtrl.Reset();
        btn.editImg.setFillAmount(0);
        gameMngr.notify.setUpAndShow("fish added to inventory");
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
