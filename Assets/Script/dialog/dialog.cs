using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class dialog : MonoBehaviour
{
    public boxdialog boxDialog;
    public float timedelay;
    public Image avatar;
    public TextMeshProUGUI name;
    public NPC npc;
    //public playerInfo player;
    [TextArea(0,500)] public string txt_content;
    public GameObject iv;
    private void Start()
    {
        
    }
    private void Update()
    {
        setDialogValue(npc);
    }
    public void setTxt_content( string txt)
    {
        txt_content = txt;
    }
    public void talk()
    {
        boxDialog.GetComponent<boxdialog>().startText( txt_content);
    }
    public void btn_close()
    {
        this.gameObject.SetActive(false);
        npc = null;
    }

    public void btn_shop()
    {
        npc.st.importShopData(npc.shopdata);
        npc.st.transform.parent.gameObject.SetActive(true);
        npc.st.gameObject.SetActive(true);
        if (npc.st.canOpenIven) iv.SetActive(true);
    }



    public void setDialogValue(NPC npc)
    {
        if (npc == null ) return;
        name.text = npc.name;
        avatar.sprite = npc.avt;
    }
    //public void setDialogValue(PlayerInfo npc)
    //{

    //}
}