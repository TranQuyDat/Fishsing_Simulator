using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameManager gameMngr;
    public bool istalking;
    public Sprite avt;
    public dialog dia;
    public bool showOption = false;
    public GroupSlotData shopdata;
    public GroupSlot st;
    [TextArea] public string aboutNPC;
    public PlayerController playerCTRL;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        activeOption();
        if(playerCTRL !=null && this.gameObject != playerCTRL.targetNPC)
        {
            showOption = false;
        }
    }
  
    public void activeOption()
    {
        if (!showOption || !gameMngr.instructionBTN.active) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            //print(this);
            dia.gameObject.SetActive(true);
            dia.setTxt_content(aboutNPC);
            dia.npc = this;
        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
    }
}
