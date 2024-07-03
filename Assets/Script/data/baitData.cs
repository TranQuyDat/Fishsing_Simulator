using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemdata", menuName = "data/baitData", order =1)]
public class baitData : ItemData
{
    public Bait bait;
    public Vector3 scale = new Vector3(0.5f,0.5f,0.5f);
}

