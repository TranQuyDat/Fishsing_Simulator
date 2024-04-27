using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float radius;
    public List<GameObject> listOption;
    public LayerMask layercheck;

    PlayerController playerCtrl;
    private void Awake()
    {
        playerCtrl = FindObjectOfType<PlayerController>();

    }
    private void Update()
    {
        checkPlayer();
    }

    public void checkPlayer()
    {
        foreach(GameObject obj in listOption)
        {
            if (playerCtrl.inArea != area.ship)
            {
                obj.SetActive(false);
                continue;
            }
            bool isplayer = Physics.CheckSphere(obj.transform.position, radius, layercheck);
            obj.SetActive(!isplayer);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (listOption.Count <= 0) return;
        Gizmos.color = Color.red;
        foreach(GameObject obj in listOption)
        {
            Gizmos.DrawWireSphere(obj.transform.position, radius);
        }
    }
}
