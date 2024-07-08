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
    public void add(ItemData itdt, int numit)
    {
        foreach(Item it in items)
        {
            if(itdt == it.itdt)
            {
                it.NumIt += numit;
                return;
            }
        }
        Item newit = new(itdt, numit);
        items.Add(newit);
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
        itdtS.tyleItem = itdt.tyleItem;
        itdtS.nameItem = itdt.nameItem;
        string name = (itdt.imgItem != null) ? itdt.imgItem.name : "0";
        itdtS.imgItemPath = DataSave.savePathIt(itdt.imgItem)+"_"+ name;//path+_+name
        itdtS.price = itdt.price;
        itdtS.detail = itdt.detail;
        itdtS.meshrenPath = DataSave.savePathIt(itdt.meshren);
        itdtS.matPath = DataSave.savePathIt(itdt.mat);
        itdtS.maxNuminSlot = itdt.maxNuminSlot;
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
        itdt.tyleItem = itdtS.tyleItem;
        itdt.nameItem = itdtS.nameItem;

        itdt.imgItem = DataSave.GetDtFrPathTsprite<Sprite>(itdtS.imgItemPath);
        itdt.price = itdtS.price;
        itdt.detail = itdtS.detail;
        itdt.meshren = DataSave.GetDtFrPath<Mesh>(itdtS.meshrenPath);
        itdt.mat = DataSave.GetDtFrPath<Material>(itdtS.matPath);
        itdt.maxNuminSlot = itdtS.maxNuminSlot;
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
            it.itdtS.pathItem = AssetDatabase.GetAssetPath(it.itdt);
            it.save();
        }
    }
}
