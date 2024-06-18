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
        icon_Equip.SetActive(status == Status.equiped);
        icon_lock.SetActive(status == Status.Lock);
    }
    private void Update()
    {
        updateSlot(item,numItem);
        updateIcon();
        updateEquiped();
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
        if(st.it_Equiped != item)
        {
            status = Status.unlock;

            canUpdateIcon = true;
        }
    }
}
