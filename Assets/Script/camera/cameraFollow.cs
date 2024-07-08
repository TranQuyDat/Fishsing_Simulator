using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public bool followZ;
    public float dis = 5;
    public float height;
    public GameManager gameMngr;
    public Transform limitL;
    public Transform limitR;
    Vector3 PosCam;
    public bool isCameraUp;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
        if (gameMngr.playerCtrl != null && target == null)
        {
            target = GameObject.FindGameObjectWithTag("posCamera");
            gameMngr.playerCtrl.updateMainCamera(this.transform);
        }
        if (target.CompareTag("posCamera"))
        {
            isCameraUp = true;
        }
        else if (target.CompareTag("hook"))
        {
            isCameraUp = false;
        }
    }

    public void followPlayer()
    {
        if (target == null) return;
        PosCam =(!followZ) 
            ? new Vector3(target.transform.position.x, target.transform.position.y+ height, this.transform.position.z)
            : this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+ height, target.transform.position.z+dis) ;
        PosCam.x = Mathf.Clamp(PosCam.x, limitL.position.x+2.5f, limitR.position.x-2.5f);
        PosCam.z = target.transform.position.z - dis;

        transform.position = PosCam;
        
    }


    public void changeTarget(GameObject newTarget)
    {
        target = newTarget;
    }


}
