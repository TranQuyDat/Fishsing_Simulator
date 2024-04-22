using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject Player;
    public float speed;
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
        this.transform.position = new Vector3(Player.transform.position.x 
            ,Player.transform.position.y,-12);
    }

    public void btn_clickIcon()
    {

    }

}
