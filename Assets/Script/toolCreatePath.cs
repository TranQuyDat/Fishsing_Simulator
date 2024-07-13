using UnityEngine;
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class toolCreatePath :MonoBehaviour
{
    public path pathdata;
    public List<UnityEngine.Object> listResources;
    public bool get;
    private void Update()
    {
        if (get)
        {
            getPath();
        }
    }
    public void getPath()
    {
        foreach(UnityEngine.Object obj in listResources)
        {
            StringPair pair = new(obj, AssetDatabase.GetAssetPath(obj));
            pathdata.listPath.Add(pair);
        }
        get = false;
    }

}
#endif