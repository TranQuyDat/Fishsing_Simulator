using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class slotItem : MonoBehaviour 
{
    public GroupSlot groupSlot;
    public itemData item;
    public int numItem;
    public int maxNumItem;
    public Image imgItem;
    public TextMeshProUGUI txt_numItem;
    private void Start()
    {
        updateSlot(item, numItem);
    }
    public void updateSlot(itemData it,int num)
    {
        if (it == null ) return;
        item = it;
        imgItem.gameObject.SetActive(true);
        imgItem.sprite = it.imgItem;
        numItem = num;
        txt_numItem.text = "x" + numItem;
        maxNumItem = it.maxNuminSlot;
    }
    public void addNumItem( int value)
    {
        numItem += value;
        txt_numItem.text = "x" + numItem;
    }
    public void combine(slotItem[] slots)
    {
        if (numItem <= 0 || numItem >= maxNumItem) return; 
        foreach(slotItem sl in slots)
        {
            if (sl.item != this.item) continue;
            this.combine(sl);
        }
    }
    public void combine(slotItem otherSlot)
    {
        int delNum = maxNumItem - numItem;
        if(delNum >= otherSlot.numItem)
        {
            this.addNumItem(otherSlot.numItem);
            otherSlot.clear();
        }
        else
        {
            this.addNumItem(delNum);
            otherSlot.addNumItem(delNum - otherSlot.numItem);
        }
    }
    public void clear()
    {
        item = null;
        imgItem.sprite = null;
        imgItem.gameObject.SetActive(false);
    }

    public void click()
    {
        groupSlot.curSlot = this;
    }

     
}
