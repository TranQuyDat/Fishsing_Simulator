using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool istalking;
    public Sprite avt;
    public dialog dia;
    public bool showOption = false;
    public ShopData shopdata;
    public GroupSlot st;
    [TextArea] public string aboutNPC;
    public PlayerController playerCTRL;
    private void Awake()
    {
        playerCTRL = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        activeOption();
        if(this.gameObject != playerCTRL.gameObject)
        {
            showOption = false;
        }
    }
  
    public void activeOption()
    {
        if (!showOption) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            print(this);
            dia.gameObject.SetActive(true);
            dia.setTxt_content(aboutNPC);
            showOption = true;
            dia.npc = this;
        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
    }
}
