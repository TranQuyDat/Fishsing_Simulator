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
    public bool activeCheckTrigger = true;

    public tag triggerTag;
    public area nextArea;
    public Action setAct = Action.idle;
    PlayerController playerctrl;
    // Start is called before the first frame update


    #region default Method

    private void Awake()
    {
        playerctrl = player.GetComponent<PlayerController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {

        if (activeCheckTrigger && playerctrl.inArea == nextArea)
        {
            imgIcon.SetActive(false);
            return;
        }
        if (activeCheckTrigger && other != null && other.CompareTag(triggerTag.ToString()))
        {
            imgIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if(activeCheckTrigger) imgIcon.SetActive(false);
    }

   
    public void movePlayerToTarget()
    {
        //Debug.Log("this ok");
        player.transform.position = target.transform.position;
        playerctrl.inArea = nextArea;
        float euler = playerctrl.faceRight;
        player.transform.eulerAngles = new Vector2(0, (isFaceRight) ? euler : euler + 180);
        playerctrl.cur_action = setAct;
    }
    public void movePlayerToTargetWithCamera()
    {
        //Debug.Log("this ok");
        changeCamera();
        float euler = (isFaceRight) ? playerctrl.faceRight : playerctrl.faceRight + 180;

        player.transform.position = target.transform.position;
        player.transform.eulerAngles = new Vector2(0, euler);
        playerctrl.inArea = nextArea;
        playerctrl.cur_action = setAct;
    }

    void changeCamera()
    {
        cameraAct.SetActive(true);
        cameraNotActive.SetActive(false);
        playerctrl.updateMainCamera(cameraAct.transform);
    }
}
