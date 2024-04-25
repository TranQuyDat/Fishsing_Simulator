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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    public void followPlayer()
    {
        this.transform.position =(!followZ) 
            ? new Vector3(Player.transform.position.x+dis, Player.transform.position.y+ height, this.transform.position.z)
            : this.transform.position = new Vector3(Player.transform.position.x+dis, Player.transform.position.y+ height, Player.transform.position.z+dis) ;


    }

    public void btn_clickIcon()
    {

    }

}
