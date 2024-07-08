using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody rb;
    public float radius;
    public float speed;
    public List<GameObject> listOption;
    public LayerMask layercheck;
    PlayerController playerCtrl;

    public bool isdriveShip;
    public bool isFishing;
    public Transform posSit;
    public Transform posFishing;
    GameManager gameMngr;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (gameMngr.playerCtrl == null || gameMngr.uiMngr.stopCtrlWhenUIsActive) return;
        else if (playerCtrl == null) playerCtrl = gameMngr.playerCtrl;
        activeOption();
        playerCtrl.updateTranform(transform,area.ship);
        movement();
    }

    public void activeOption()
    {
        foreach(GameObject obj in listOption)
        {
            if (playerCtrl.inArea != area.ship)
            {
                obj.SetActive(false);
                isFishing = false;
                isdriveShip = false;
                continue;
            }
            bool isplayer = Physics.CheckSphere(obj.transform.position, radius, layercheck);
            if(isplayer) check_drive_or_fishing(obj);
            obj.SetActive(!isplayer);
            
        }

    }


    void check_drive_or_fishing(GameObject obj)
    {
        isFishing = obj.CompareTag("posFishing");
        isdriveShip = obj.CompareTag("posSit");
    }

    public void movement()
    {
        if (!isdriveShip) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
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
