using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "settingData", menuName = "data/settingData")]
public class SettingData : ScriptableObject
{
    public float volume;
    public float soundFx;

    public void setVLandFX(float vl , float fx)
    {
        volume = vl;
        soundFx = fx;
    }
}
