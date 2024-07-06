using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public int NumIt;
    public Item(ItemData itdt,int NumIt)
    {
        this.itdt = itdt;
        this.NumIt = NumIt;
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
    }
}
