using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public Material dawnSkybox;
    public Material daySkybox;
    public Material eveningSkybox;
    public Material nightSkybox;

    [Range(0, 24)]
    public float timeOfDay;
    [Range(0.1f,1)]
    public float speedTime = 0.1f;
    [Range(0, 360)]
    public float offset;
    public float speedCloud =5f;
    private void Update()
    {
        UpdateLighting(timeOfDay / 24f);
        timeOfDay += speedTime* Time.deltaTime;
        if (timeOfDay >= 24)
        {
            timeOfDay = 0;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.skybox = GetCurrentSkybox(timePercent);
         offset = (offset+ speedCloud * Time.deltaTime)%360;
        RenderSettings.skybox.SetFloat("_Rotation", offset);
        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        sun.intensity = GetLightIntensitySun(timePercent);

    }

    private Material GetCurrentSkybox(float timePercent)
    {
        if (timePercent < 0.25f)
        {
            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);
            return dawnSkybox;
        }
        else if (timePercent < 0.5f)
        {
            return daySkybox;
        }
        else if (timePercent < 0.75f)
        {
            return eveningSkybox;
        }
        else
        {
            sun.gameObject.SetActive(false);
            moon.gameObject.SetActive(true);
            return nightSkybox;
        }
    }

    private float GetLightIntensitySun(float timePercent)
    {
        if (timePercent < 0.25f)
        {
            return Mathf.Lerp(1f, 6f, timePercent / 0.25f);
        }
        else if (timePercent < 0.5f)
        {
            return 6f;
        }
        else if (timePercent < 0.75f)
        {
            return Mathf.Lerp(6f, 1f, (timePercent - 0.5f) / 0.25f);
        }
        else
        {
            return 1f;
        }
    }
}
