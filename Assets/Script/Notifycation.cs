using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Notifycation : MonoBehaviour
{
    public TextMeshProUGUI txt_notify;
    public float time;
    public void setUp(string txt)
    {
        txt_notify.text = txt;
    }

    public void showNotify()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(wait2Hide());
    }
    public void setUpAndShow(string txt)
    {
        txt_notify.text = txt;
        this.gameObject.SetActive(true);
        StartCoroutine(wait2Hide());
    }
    IEnumerator wait2Hide()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }

}
