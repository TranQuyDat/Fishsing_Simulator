using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="itemdata",menuName ="data/itemdata")]
public class itemData :ScriptableObject
{
    public TyleItem tyleItem;
    public string nameItem;
    public Sprite imgItem;
    public float price;
    [TextArea] public string detail;
    public int maxNuminSlot=10;
}
