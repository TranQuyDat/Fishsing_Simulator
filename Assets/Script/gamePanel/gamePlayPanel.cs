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

    public void playAniFishingNormal()
    {
        btnfishingcast.interactable = true;
        ani.Play("uiFishingNormal");
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

[System.Serializable]
public class UiTimesOfDay
{
    public Sprite dawn;
    public Sprite day;
    public Sprite evening;
    public Sprite night;

    public Image imgIcon;
    public TextMeshProUGUI txt;

    public void swtIconTimeOfDay( float t)
    {
        (imgIcon.sprite,txt.text) = t switch
        {
            _ when (t>=3f && t<6f) => (dawn ,"Dawn") ,
            _ when (t>=6f && t<15f) => (day, "day"),
            _ when (t>=15f && t<18f) => (evening, "evening"),
            _ when (t>=18f || t<3f) => (night, "night"),
        };
    }
}
public class gamePlayPanel : MonoBehaviour
{
    public GameManager gameMngr;
    public FishingUI fishingUI;
    public FishInfoUI fishInfoUI;
    public EditImgUI editImgUI;
    public UiTimesOfDay uiTimesOfDay;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        editImgUI.setFillAmount(0);
    }
    private void Update()
    {
        if(fishingUI != null )
            setActive(fishingUI.ui, gameMngr.shipMngr!=null && gameMngr.shipMngr.curShipCtrl != null && gameMngr.shipMngr.curShipCtrl.isFishing);
        if (fishInfoUI != null ) 
            setActive(fishInfoUI.ui,gameMngr.fishingRodCtrl != null && gameMngr.fishingRodCtrl.wasCaughtFish && gameMngr.fishingRodCtrl.fish != null);
        updateAmount();
        if (fishInfoUI.ui.active)
        {
            fishInfoUI.setUp(gameMngr.fishingRodCtrl.fish.fishData);
        }

     
    }

    public void updateAmount()
    {
        bool b = gameMngr == null || gameMngr.fishMngr == null || !fishingUI.ui.active 
           || gameMngr.fishingRodCtrl==null || !gameMngr.fishingRodCtrl.isfishing;
        if (b)
        {
            editImgUI.setFillAmount(0);
            return;
        }
            if (gameMngr.fishMngr.theLuckyFish != null && gameMngr.fishMngr.theLuckyFishAI.acFish == Action.ateBait &&
            editImgUI.imgValue.fillAmount >= 0 && editImgUI.imgValue.fillAmount < 1f)
        {
            editImgUI.setFillAmount(1 - Mathf.Round((gameMngr.fishMngr.dis / gameMngr.fishMngr.maxdis) * 100f) * 0.01f);
        }
    }


    #region button
    public void btnFishing()
    {
        gameMngr.fishingRodCtrl.isPull = true;
        if (editImgUI.imgValue.fillAmount >= 0.95f)
        {
            editImgUI.setFillAmount(1);
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
        GameObject target = GameObject.FindGameObjectWithTag("posCamera");
        gameMngr.mainCamera.changeTarget(target);
    }

    public void btnUpDownSurfaceWater()
    {
        if (!gameMngr.fishingRodCtrl.isfishing) return;
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
        editImgUI.setFillAmount(0);
        gameMngr.notify.setUpAndShow("fish added to inventory");

        btnStopFishing();
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
