using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Notifycation : MonoBehaviour
{
    public GameManager gameMngr;
    public TextMeshProUGUI txt_notify;
    public float time;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    public void setUp(string txt)
    {
        txt_notify.text = txt;
    }

    public void showNotify()
    {
        this.gameObject.SetActive(true);
        gameMngr.soundMngr.playSFX(SoundType.sfx_notify);
        StartCoroutine(wait2Hide());
    }
    public void setUpAndShow(string txt,float t = 3f)
    {
        time = t;
        txt_notify.text = txt;
        this.gameObject.SetActive(true);
        gameMngr.soundMngr.playSFX(SoundType.sfx_notify);
        StartCoroutine(wait2Hide());
    }
    IEnumerator wait2Hide()
    {
        yield return new WaitForSecondsRealtime(time);
        this.gameObject.SetActive(false);
    }

}
