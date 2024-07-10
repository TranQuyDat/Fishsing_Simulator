using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeShip : GroupSlot
{
    public inventory iv;
    public slotItem_ship sl;
    public ShipManager shipMngr;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if(curSlot !=null && (sl ==null|| sl.gameObject !=curSlot.gameObject)) sl = curSlot.GetComponent<slotItem_ship>();
        if (curSlot == null || curSlot.item == null || sl.status != Status.Lock)
        {
            ui_btnBuy.interactable = false;
        }
        else ui_btnBuy.interactable = true;

        
    }


    public override void btn_buy()
    {
        if (curSlot == null || curSlot.item == null || sl.status != Status.Lock)
            return;

        if (iv.coin >= curSlot.item.price)
        {
            //unlock
            iv.coin -= curSlot.item.price;

            gameMngr.loadData.dataSave.dataPlayer.coins = iv.coin;
            sl.item.status = Status.unlock;
            sl.canUpdateIcon = true;
            sl.icon_lock.SetActive(false);
        }
        else
        {
            gameMngr.notify.setUpAndShow("You don't have enough money to buy it.");
        }
    }

    public override void btn_eqiup()
    {
        if (curSlot == null || curSlot.item == null || sl.status == Status.Lock) return;
        if (sl.status == Status.equiped)
        {
            gameMngr.notify.setUpAndShow("Ship was equiped");
        }
        else if (sl.status == Status.unlock)
        {
            //Equip
            sl.item.status = Status.equiped;
            shipMngr.it_Equiped = curSlot.item;
            shipMngr.setActiveShips();
            sl.canUpdateIcon = true;
            sl.icon_Equip.SetActive(true);
        }
    }

   

}
