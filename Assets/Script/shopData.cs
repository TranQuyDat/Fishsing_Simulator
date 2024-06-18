using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public itemData itdt;
    public int NumIt;
}

[CreateAssetMenu(fileName = "shopdata", menuName = "data/shopData")]
public class ShopData : ScriptableObject
{
    public Item[] items;

}
