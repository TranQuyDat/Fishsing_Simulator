using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class inventory : GroupSlot
{
    #region UI
    public GameObject ui_btnSell;
    public GameObject ui_btnUse;
    public GameObject ui_btnDrop;
    public GameObject ui_btnClose;

    //ui ok_close
    public GameObject ui_ok_close;
    public Button btnOK;
    public Slider slider;
    public TextMeshProUGUI txt_tile;
    public TextMeshProUGUI txt_minmax;
    public GameObject content;
    #endregion
    public TextMeshProUGUI detail;
    bool isSelcted = false;
    private void Awake()
    {
    }
    private void Update()
    {
        if (ui_ok_close.active && slider.gameObject.active)// dieu kien update  txt_minmax.text
        {
            txt_minmax.text = (int)(slider.value * curSlot.numItem) + "/" + curSlot.numItem;
        }
        updateInvenDetail();



    }

    public void updateInvenDetail()
    {
        bool b = curSlot == null || curSlot.item == null;
        if (b && detail.text == "") return;
        if (b)
        {
            detail.text = "";
            return;
        }
        detail.text = curSlot.item.detail; 
    }
    public void addItem(itemData it,int numItem=1)
    {

        for(int i = 0; i < slots.Count; i++)
        {
            //  slot !=null && numItem == maxNumItem || item != it => continue
            bool b = slots[i].item != null && (slots[i].numItem == slots[i].maxNumItem || slots[i].item != it);
            if (b) continue;
            if(slots[i].item != null && slots[i].item == it)// slot != null && item == it && numItem < maxNumItem => numItem++
            {
                slots[i].addNumItem(numItem);
                return;
            }
            slots[i].updateSlot(it, numItem) ;// slot == null
            return;
        }
    }
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
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }

    public void btn_sell() 
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want sell?");
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            print("sell");
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
        }));
    }
    public void btn_use() 
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want use?",false);
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            print("use");
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
        }));
    }
    public void btn_drop()
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want drop?");
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {

            curSlot.addNumItem(-(int)(slider.value * curSlot.numItem)) ;

            if (curSlot.numItem <= 0)
            {
                curSlot.clear();
            }
            SortAndMergeSlots();
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
        }));

    }

    public void setUiOK(string tile,bool enSlider = true)
    {
        ui_ok_close.SetActive(true);
        txt_tile.text = tile;
        
        txt_minmax.gameObject.SetActive(enSlider);
        slider.gameObject.SetActive(enSlider);
        if (!enSlider) return;
        txt_minmax.text = (int)(slider.value*curSlot.numItem) + "/" + curSlot.numItem;
    }
}
