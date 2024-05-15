using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UiManager uiMngr;
    public BoatController boatCtrl;

    private void Update()
    {
        uiMngr.ShipUi.setActive_UiFishing(boatCtrl.isFishing);
    }
}
