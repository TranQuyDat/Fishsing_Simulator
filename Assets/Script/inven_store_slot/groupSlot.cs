using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class GroupSlot : MonoBehaviour
{
    #region UI

    [Header("uiGroupSlot")]
    public TextMeshProUGUI detail;
    public TextMeshProUGUI txt_name;
    public Button ui_btnBuy;
    public Button ui_btnSell;
    public Button ui_btnDrop;
    public Button ui_btnEquip;
    public Button ui_btnUse;

    //ui ok_close
    [Header("ui_ok_close")]
    public GameObject ui_ok_close;
    public Button btnOK;
    public Slider slider;
    public TextMeshProUGUI txt_tile;
    public TextMeshProUGUI txt_minmax;
    public TextMeshProUGUI txt_cost;
    #endregion
    public string name;
    public List<slotItem> slots;
    
    public int maxCoutSlot;
    public slotItem curSlot;

    #region Virtual
    public virtual void btn_buy() { }
    public virtual void btn_sell() { }
    public virtual void btn_eqiup() { }
    public virtual void btn_use() { }
    public virtual void btn_drop() { }
    public virtual void updateGroupSlot()
    {
        bool b = curSlot == null || curSlot.item == null;
        if (detail == null || (b && detail.text == "")) return;
        if (b)
        {
            detail.text = "";
            return;
        }
        detail.text = curSlot.item.detail;
    }


    #endregion

    #region normal method
    public void SortAndMergeSlots()
    {
        for (int i = 0; i < slots.Count - 1; i++)
        {
            if (slots[i].item == null) continue;

            for (int j = i + 1; j < slots.Count; j++)
            {
                if (slots[j].item != null && slots[i].item == slots[j].item)
                {
                    int transferableAmount = Mathf.Min(slots[j].numItem, slots[i].maxNumItem - slots[i].numItem);
                    slots[i].addNumItem(transferableAmount);
                    slots[j].addNumItem(-transferableAmount);

                    if (slots[j].numItem == 0)
                    {
                        slots[j].clear();
                    }

                    if (slots[i].numItem == slots[i].maxNumItem)
                    {
                        break;
                    }
                }
            }
        }

        slots.Sort((a, b) => (a.item == null ? 1 : 0).CompareTo(b.item == null ? 1 : 0));
        updateUIslot();


    }
    public void updateUIslot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }

    public void addItem(itemData it, int numItem = 1)
    {

        for (int i = 0; i < slots.Count; i++)
        {
            //  slot !=null && numItem == maxNumItem || item != it => continue
            bool b = slots[i].item != null && (slots[i].numItem == slots[i].maxNumItem || slots[i].item != it);
            if (b) continue;
            if (slots[i].item != null && slots[i].item == it)// slot != null && item == it && numItem < maxNumItem => numItem++
            {
                slots[i].addNumItem(numItem);
                return;
            }
            slots[i].updateSlot(it, numItem);// slot == null
            return;
        }
    }
    public void removeItem(slotItem slot, int numItem = 1)
    {
        slot.addNumItem(-numItem);

        if (slot.numItem <= 0)
        {
            slot.clear();
        }
    }

    public void setUiOK(string tile, bool enSlider = true, bool enCost = false)
    {
        ui_ok_close.SetActive(true);
        txt_tile.text = tile;

        txt_minmax.gameObject.SetActive(enSlider);
        slider.gameObject.SetActive(enSlider);
        txt_cost.transform.parent.gameObject.SetActive(enCost);
    }

    public  void updateUIok()
    {
        if (!ui_ok_close.active || curSlot == null || curSlot.item == null) return;
        if (slider.gameObject.active)// dieu kien update  txt_minmax.text
        {
            txt_minmax.text = (int)(slider.value * curSlot.numItem) + "/" + curSlot.numItem;
        }
        if (txt_cost.gameObject.active)// dieu kien update  txt_cost.text
        {
            txt_cost.text = "cost : " + curSlot.item.price * (int)(slider.value * curSlot.numItem);
        }
    }
    #endregion
}
