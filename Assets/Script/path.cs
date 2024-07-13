using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pathdata",menuName ="data/pathdata")]
public class path : ScriptableObject
{
    public List<StringPair> listPath;   
}

[System.Serializable]
public class StringPair
{
    public UnityEngine.Object key;
    public string value;
    public StringPair(UnityEngine.Object k,string v)
    {
        this.key =k;
        this.value = v;
    }
}
