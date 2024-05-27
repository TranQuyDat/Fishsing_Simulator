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
    }
    private void Start()
    {
        btn.editImg.setFillAmount(0);
    }
    private void Update()
    {
        if (gameMngr.fishMngr.theLuckyFish !=null && gameMngr.fishMngr.theLuckyFishAI.acFish == Action.ateBait &&
            btn.editImg.imgValue.fillAmount >= 0 && btn.editImg.imgValue.fillAmount <= 0.90f)
        {
            btn.editImg.setFillAmount(1 - Mathf.Round( (gameMngr.fishMngr.dis / gameMngr.fishMngr.cur_dis) *100f )*0.01f);
        }
        
    }

    #region button

    public void btnFishing()
    {
        gameMngr.fishMngr.theLuckyFishAI.isPull = true;
        if (btn.editImg.imgValue.fillAmount > 0.90f)
        {
            btn.editImg.setFillAmount(1);
            return;
        }
            StartCoroutine(resetIspullofFish());
    }
    IEnumerator resetIspullofFish()
    {
        yield return new WaitForSeconds(0.5f);
        gameMngr.fishMngr.theLuckyFishAI.isPull = false;
    }
    public void btnFishingcast()
    {

        gameMngr.fishingRodCtrl.isCasting = true;
        gameMngr.playerCtrl.cur_action = Action.fishing_cast;
    }

    #endregion
}
