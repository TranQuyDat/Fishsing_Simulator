using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUiController : MonoBehaviour
{
    public GameObject ui_fishing;
    PlayerController playerCtrl;
    private void Awake()
    {
        playerCtrl = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
    }
    public void setActive_UiFishing(bool b)
    {
        ui_fishing.SetActive(b);
    }
    public void btn_fishingCast()
    {
        playerCtrl.cur_action = Action.fishing_cast;
    }

 
}
