using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demo : MonoBehaviour
{
    public Transform ob1;
    public Transform ob2;
    public void button()
    {
        ob1.position = ob2.position;
    }
}
