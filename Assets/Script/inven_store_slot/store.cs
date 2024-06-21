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
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want buy?",true,true);
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {

            int num = (int)(slider.value * curSlot.numItem);
            int cost = curSlot.item.price * num;
            if (iv.coin >= cost)
            {
                print("buy"+curSlot.item);
                iv.coin -= cost;
                iv.addItem(curSlot.item, num);
                removeItem(curSlot, num);
                ui_ok_close.SetActive(false);
                curSlot = null;
            }
            else
            {
                print("you have not enought coin");
            }

            btnOK.onClick.RemoveAllListeners();
        }));
    }

}
