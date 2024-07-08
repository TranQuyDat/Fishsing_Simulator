using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class boxdialog : MonoBehaviour
{
    public List<char> listChar;
    public int countWord;
    public int maxCountWord=10;
    public dialog dia;
    public TextMeshProUGUI txtbox;

    public float timedelay;
    public float oldtimedelay;
    public string lastWord="";
    private void Awake()
    {
        countWord = 0;
        timedelay = dia.timedelay;
        oldtimedelay = timedelay;
    }

    void Update()
    {
        if (countWord >= maxCountWord && Input.GetKeyDown(KeyCode.Mouse0))
        {
            listChar.InsertRange(0, lastWord.ToCharArray());
            txtbox.text = "";
            countWord = 0;
        }
        skipTimedelaytxt();
    }
    public void startText(string txtmain)
    {
        if(IsInvoking("nextChar"))CancelInvoke("nextChar");
        countWord = 0;
        lastWord = "";
        listChar = txtmain.ToCharArray().ToList(); //Debug.Log(listChar);
        txtbox.text = "";
        InvokeRepeating("nextChar", 0f, timedelay);
    }
    public void nextChar()
    {
        if (listChar == null || listChar.Count <= 0) // dieu kien de endText
        {
            endText();
            return;
        }
        if (countWord >= this.maxCountWord) return;
        countWord += 1;
        txtbox.text += listChar[0];
        if (listChar[0] != ' ') lastWord += listChar[0];
        else lastWord = "";
        listChar.RemoveAt(0);

    }
    public void endText()
    {
        CancelInvoke("nextChar");
    }

    public void skipTimedelaytxt()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timedelay = 0.1f;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            timedelay = dia.timedelay;
        }

        if (oldtimedelay != timedelay)
        {
            CancelInvoke("nextChar");
            oldtimedelay = timedelay;
            InvokeRepeating("nextChar", 0.1f, timedelay);
        }
    }
}