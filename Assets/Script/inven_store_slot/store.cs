using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class store : GroupSlot
{
    public inventory iv;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        txt_name.text = name;
    }
    private void Update()
    {
        updateUIok();
        updateGroupSlot();
    }


    override
    public void btn_buy()
    {
        gameMngr.soundMngr.playSFX(SoundType.sfx_click);
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want buy?",true,true);
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            gameMngr.soundMngr.playSFX(SoundType.sfx_click);
            int num = (int)(slider.value * curSlot.numItem);
            int cost = curSlot.item.price * num;
            if (iv.coin >= cost)
            {
                print("buy"+curSlot.item);
                
                iv.addItem(curSlot.item, num);
                if (iv.canAddToData)
                {
                    iv.Data.add(curSlot.item, num);
                    gameMngr.loadData.dataSave.dataPlayer.coins -= cost;
                }
                removeItem(curSlot, num);
                ui_ok_close.SetActive(false);
                curSlot = null;
            }
            else
            {
                gameMngr.notify.setUpAndShow("You don't have enough money to buy it.");
            }

            btnOK.onClick.RemoveAllListeners();
        }));
    }

}
