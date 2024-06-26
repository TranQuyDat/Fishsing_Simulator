using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnload : MonoBehaviour
{
    public string objectid;
    private void Awake()
    {
        objectid = name;
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroyOnload>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroyOnload>()[i].objectid == objectid &&
                Object.FindObjectsOfType<DontDestroyOnload>()[i] != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
