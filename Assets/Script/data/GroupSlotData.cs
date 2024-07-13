using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopdata", menuName = "data/GroupSlotData")]
public class GroupSlotData : ScriptableObject
{
    public  List<Item> items;
    public List<Item> itemsReset;
    public void add(ItemData itdt, int numit)
    {
        foreach(Item it in items)
        {
            if(itdt == it.itdt)
            {
                it.NumIt += numit;
                if (it.NumIt <= 0) items.Remove(it);
                return;
            }
        }
        Item newit = new(itdt, numit);
        items.Add(newit);
    }

    public void reset()
    {
        items = itemsReset;
    }
}

[System.Serializable]
public class Item
{
    public ItemData itdt;
    public ItemDataSave itdtS;
    public int NumIt;
    public Item(ItemData itdt,int NumIt)
    {
        this.itdt = itdt;
        this.NumIt = NumIt;
    }
    public void save()
    {
        string name = (itdt.imgItem != null) ? itdt.imgItem.name : "0";
        itdtS.status = itdt.status;
    }
    public void Load()
    {
        if(itdt == null)
        {
            itdt = DataSave.GetDtFrPath<ItemData>(itdtS.pathItem);
            if(itdt == null)
            {
                Debug.Log("not find path");
            } 
        }
        itdt.status = itdtS.status;
    }
   
}
[System.Serializable]
public class GroupSlotDataSave 
{
    public string name;
    public List<Item> items;
    public GroupSlotDataSave(GroupSlotData grdt)
    {
        name = grdt.name;
        items = new List<Item>(grdt.items);
        foreach(Item it in items)
        {
            it.itdtS.pathItem = GameManager.getPath(it.itdt);
            it.save();
        }
    }
}
