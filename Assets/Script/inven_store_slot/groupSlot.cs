using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupSlot : MonoBehaviour
{
    public string name;
    public List<slotItem> slots;
    public Dictionary<itemData, int> dicItem;

    public int maxCoutSlot;
    public slotItem curSlot;
}
