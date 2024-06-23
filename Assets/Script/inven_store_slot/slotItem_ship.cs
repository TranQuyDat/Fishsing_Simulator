using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotItem_ship : slotItem
{

    public GameObject icon_Equip;
    public GameObject icon_lock;
    public Status status;
    public bool canUpdateIcon=false;
    private void Start()
    {
        if(item !=null)
            status = item.status;
        icon_Equip.SetActive(status == Status.equiped);
        icon_lock.SetActive(status == Status.Lock);
    }
    private void Update()
    {
        updateSlot(item,numItem);
        updateIcon();
        updateEquiped();
    }
    public override void updateSlot(itemData it, int num)
    {
        base.updateSlot(it, num);
        if(item != null && item.status != status)
        {
            status = item.status;
            canUpdateIcon = true;
        }
    }
    public void updateIcon()
    {
        if (!canUpdateIcon) return;
        icon_Equip.SetActive(status == Status.equiped);
        icon_lock.SetActive(status == Status.Lock);
        canUpdateIcon = false;
    }
    public void updateEquiped()
    {
        if (status == Status.Lock) return;
        storeShip st = groupSlot.GetComponent<storeShip>();
        if(st.shipMngr.it_Equiped != item)
        {
            item.status = Status.unlock;
            canUpdateIcon = true;
        }
    }
}
