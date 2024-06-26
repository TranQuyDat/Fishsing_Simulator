using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "shopdata", menuName = "data/shopData")]
public class ShopData : ScriptableObject
{
    public Item[] items;

}

[System.Serializable]
public class Item
{
    public ItemData itdt;
    public int NumIt;
}
