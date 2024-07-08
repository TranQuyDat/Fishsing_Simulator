using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class EditImgUI 
{
    public Image imgValue;
    
    public void AddFillAmount(float v)
    {
        imgValue.fillAmount += v * Time.deltaTime;
    }
    public void setFillAmount(float v)
    {
        imgValue.fillAmount = Mathf.Round(v*100f)*0.01f ;
    }

}
