using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="itemdata",menuName ="data/itemdata")]
public class ItemData :ScriptableObject
{
    public TyleItem tyleItem;
    public string nameItem;
    public Sprite imgItem;
    public int price;
    [TextArea] public string detail;
    public Mesh meshren;
    public Material mat;
    public tag tag;
    public int maxNuminSlot=10;
    public Status status;
}
