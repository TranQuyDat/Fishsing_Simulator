using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeScene : MonoBehaviour
{
    public GameObject imgIcon;
    public GameObject target;
    public GameObject player;
    public GameObject cameraAct;
    public GameObject cameraNotActive;
    public bool isFaceRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other != null && other.CompareTag("player"))
        {
            imgIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        imgIcon.SetActive(false);
    }

   
    public void movePlayerToTarget()
    {
        Debug.Log("this ok");
        player.transform.position = target.transform.position;
    }
    public void movePlayerToTarget(float eulerY)
    {
        Debug.Log("this ok");
        cameraAct.SetActive( true);
        cameraNotActive.SetActive( false);
        float euler = (isFaceRight) ? eulerY : eulerY+180;
        player.GetComponent<PlayerController>().eulerY = eulerY;
        player.transform.position = target.transform.position;
        player.transform.eulerAngles = new Vector2(0, euler);
    }
}
