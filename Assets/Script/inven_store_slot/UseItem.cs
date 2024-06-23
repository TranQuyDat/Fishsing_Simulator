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
    public void use(itemData it)
    {
        switch (it.tyleItem)
        {
            case TyleItem.fish:
            case TyleItem.bait:
                addBait2Rod(it);
                break;
        }
    }
    public void addBait2Rod(itemData bait)
    {
        GameObject baitObj = gameMngr.fishingRodCtrl.hook.Find("bait").gameObject;
        MeshRenderer baitObjMeshR = baitObj.GetComponent<MeshRenderer>();
        MeshFilter baitObjMeshF = baitObj.GetComponent<MeshFilter>();

        baitObjMeshR.materials[0] = bait.mat;
        baitObjMeshF.mesh = bait.meshren;
        baitObj.tag = bait.tyleItem.ToString();

    }
}
