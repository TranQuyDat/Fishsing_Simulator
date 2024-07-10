using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameManager gameMngr;
    public Light sun;
    public Light moon;
    public Color dawnSkybox;
    public Color daySkybox;
    public Color eveningSkybox;
    public Color nightSkybox;
    public bool locktime = false;
    [Range(0, 24)]
    public float timeOfDay;
    [Range(0.0f,1)]
    public float speedTime = 0.1f;
    [Range(0, 360)]
    public float offset;
    public float speedCloud =5f;
    private void Awake()
    {
        if (!locktime) timeOfDay =  gameMngr.loadData.dataSave.timeIngame;
    }
    private void Update()
    {
        UpdateLighting(timeOfDay / 24f);
        if(!locktime)timeOfDay += speedTime* Time.deltaTime;
        if (timeOfDay >= 24)
        {
            timeOfDay = 0;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.skybox.SetColor("_Tint", GetCurrentSkybox(timePercent)) ;
         offset = (offset+ speedCloud * Time.deltaTime)%360;
        RenderSettings.skybox.SetFloat("_Rotation", offset);
        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 80f, 90f, 0));
        sun.intensity = GetLightIntensitySun(timePercent);

    }

    private Color GetCurrentSkybox(float timePercent)
    {
        if (timePercent < 0.25f)//0 -> 0.25 dawn
        {
            sun.gameObject.SetActive(true);
            if(moon != null) moon.gameObject.SetActive(false);
            Color currentColor = Color.Lerp(nightSkybox, dawnSkybox, timePercent / 0.25f);
            return currentColor;
        }
        else if (timePercent < 0.5f)//0.25 -> 0.5 day
        {
            Color currentColor = Color.Lerp(dawnSkybox, daySkybox, (timePercent-0.25f) / 0.125f);
            return currentColor;
        }
        else if (timePercent < 0.75f) //0.5 -> 0.75 evening
        {
            Color currentColor = Color.Lerp(daySkybox, eveningSkybox, (timePercent-0.5f) / 0.125f);
            return currentColor;
        }
        else //0.75 -> 1 night
        {
            sun.gameObject.SetActive(false);
            if (moon != null) moon.gameObject.SetActive(true);
            Color currentColor = Color.Lerp(eveningSkybox, nightSkybox, (timePercent-0.75f) / 0.125f);
            return currentColor;
        }
    }

    private float GetLightIntensitySun(float timePercent)
    {
        if (timePercent < 0.25f) //dawn
        {
            return Mathf.Lerp(1f, 6f, timePercent / 0.25f);
        }
        else if (timePercent < 0.5f) // day
        {
            return 6f;
        }
        else if (timePercent < 0.75f) // evening
        {
            return Mathf.Lerp(6f, 1f, (timePercent - 0.5f) / 0.25f);
        }
        else // night
        {
            return 1f;
        }
    }
}
