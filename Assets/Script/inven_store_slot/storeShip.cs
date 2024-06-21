using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeShip : GroupSlot
{
    public inventory iv;
    public itemData it_Equiped;
    public slotItem_ship sl;
    // Start is called before the first frame update
    private void Awake()
    {

    }

    // Update is called once per frame
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
            sl.item.status = Status.unlock;

            sl.canUpdateIcon = true;
            sl.icon_lock.SetActive(false);
        }
        else
        {
            print("khong du tien");
        }
    }

    public override void btn_eqiup()
    {
        if (curSlot == null || curSlot.item == null || sl.status == Status.Lock) return;
        if (sl.status == Status.equiped)
        {
            // unEquip
            sl.item.status = Status.unlock;
            it_Equiped = null;
            sl.canUpdateIcon = true;
            sl.icon_Equip.SetActive(false);
        }
        else if (sl.status == Status.unlock)
        {
            //Equip
            sl.item.status = Status.equiped;
            it_Equiped = curSlot.item;
            sl.canUpdateIcon = true;
            sl.icon_Equip.SetActive(true);
        }
    }

}
