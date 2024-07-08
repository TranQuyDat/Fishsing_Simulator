using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="itemdata",menuName ="data/itemdata" ,order =0)]

public class ItemData :ScriptableObject
{
    public TyleItem tyleItem;
    public string nameItem;
    public Sprite imgItem;
    public int price;
    [TextArea] public string detail;
    public Mesh meshren;
    public Material mat;
    public int maxNuminSlot=10;
    public Status status;
    
}
[System.Serializable]
public class ItemDataSave
{
    public TyleItem tyleItem;
    public string nameItem;
    public string imgItemPath;//path+_+name
    public int price;
    [TextArea] public string detail;
    public string meshrenPath;
    public string matPath;
    public int maxNuminSlot = 10;
    public Status status;
    public string pathItem;
    
}
