using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demo : MonoBehaviour
{
    public GroupSlot iv;
    public Button btn;
    public itemData it;
    public void button()
    {
        iv.addItem(it);
    }
}
