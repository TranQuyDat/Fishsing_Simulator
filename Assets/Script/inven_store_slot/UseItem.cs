using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public GameManager gameMngr;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    public void use(ItemData it)
    {
        switch (it.tyleItem)
        {
            case TyleItem.fish_Or_bait:
            case TyleItem.bait:
                addBait2Rod((baitData)it);
                break;
        }
    }
    public void addBait2Rod(baitData baitDT)
    {
        GameObject baitObj = gameMngr.fishingRodCtrl.hook.Find("bait").gameObject;
        MeshRenderer baitObjMeshR = baitObj.GetComponent<MeshRenderer>();
        MeshFilter baitObjMeshF = baitObj.GetComponent<MeshFilter>();

        baitObjMeshR.material = baitDT.mat;
        baitObjMeshF.sharedMesh = baitDT.meshren;
        baitObj.tag = baitDT.bait.ToString();
        baitObj.transform.localScale = baitDT.scale;

    }
}
