using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public bool followZ;
    public float dis;
    public float height;
    public GameManager gameMngr;
    public Transform limitL;
    public Transform limitR;
    Vector3 PosCam;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
        if (gameMngr.playerCtrl != null && Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("posCamera");
            gameMngr.playerCtrl.updateMainCamera(this.transform);
        }
    }

    public void followPlayer()
    {
        if (Player == null) return;
        PosCam =(!followZ) 
            ? new Vector3(Player.transform.position.x+dis, Player.transform.position.y+ height, this.transform.position.z)
            : this.transform.position = new Vector3(Player.transform.position.x+dis, Player.transform.position.y+ height, Player.transform.position.z+dis) ;
        PosCam.x = Mathf.Clamp(PosCam.x, limitL.position.x+5, limitR.position.x-5);
        transform.position = PosCam;

    }



    public void setHeight(float h)
    {
        height = h;
    }


}
