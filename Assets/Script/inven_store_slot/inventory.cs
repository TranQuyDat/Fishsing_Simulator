using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class inventory : GroupSlot
{
    public TextMeshProUGUI txt_coin;
    public int coin;//save
    public store st;
    public UseItem useItem;
    private void Awake()
    {

    }
    private void Update()
    {
        updateUIok();
        updateGroupSlot();
    }

    private void OnEnable()
    {
        importShopData(Data);
        SortAndMergeSlots();
        txt_coin.text = "" + coin;
    }
    override
    public void updateGroupSlot()
    {
        if(txt_coin.text != coin.ToString())
        {
            int c = int.Parse(txt_coin.text);
            c += (c<coin)?1:-1;
            txt_coin.text = "" +c;
        }

        bool b = curSlot == null || curSlot.item == null;
        if (b && detail.text == "") return;
        if (b)
        {
            detail.text = "";
            return;
        }
        detail.text = curSlot.item.detail; 
    }



    public void btn_sell() 
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want sell?",true,true);
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int num = (int)(slider.value * curSlot.numItem);
            int cost = curSlot.item.price * num;
            coin += cost;
            print("sell" + curSlot.item);
            //sell
            st.addItem(curSlot.item, num);
            removeItem(curSlot, num);
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
            curSlot = null;
        }));
    }
    public void btn_use() 
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want use?",false);
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            print("use"+curSlot.item);
            //use
            useItem.use(curSlot.item);

            removeItem(curSlot, 1);
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
            curSlot = null;
        }));
    }
    public void btn_drop()
    {
        if (curSlot == null || curSlot.item == null) return;
        ui_ok_close.SetActive(true);
        setUiOK("are you want drop?");
        btnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            print("drop" + curSlot.item);
            //drop
            removeItem(curSlot,(int)(slider.value * curSlot.numItem)) ;

            SortAndMergeSlots();
            ui_ok_close.SetActive(false);
            btnOK.onClick.RemoveAllListeners();
            curSlot = null;
        }));

    }


}
