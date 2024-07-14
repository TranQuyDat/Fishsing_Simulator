using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeScene : MonoBehaviour
{
    public GameManager gameMngr;
    public GameObject imgIcon;
    public GameObject target;
    public GameObject cameraAct;
    public GameObject cameraNotActive;

    public bool isFaceRight;
    public bool onlyEnableImgIcon = true;
    public bool onlyOpenGlbMap = false;

    public tag triggerTag;
    public area nextArea;
    public Action setAct = Action.idle;
    PlayerController playerctrl;

    #region default Method

    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        playerctrl = gameMngr.playerCtrl;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMngr ==null) gameMngr = FindObjectOfType<GameManager>();
        if (gameMngr.playerCtrl !=null && playerctrl == null)
        {
            playerctrl = gameMngr.playerCtrl;
        }
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {

        if (onlyEnableImgIcon && playerctrl.inArea == nextArea)
        {
            imgIcon.SetActive(false);
            return;
        }
        if (onlyEnableImgIcon && other != null && other.CompareTag(triggerTag.ToString()))
        {
            imgIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if(onlyEnableImgIcon) imgIcon.SetActive(false);
    }

   
    public void movePlayerToTarget(bool playerCanMove)
    {
        //Debug.Log("this ok");
        gameMngr.soundMngr.playSFX(SoundType.sfx_click);
        playerctrl.transform.position = target.transform.position;
        playerctrl.inArea = nextArea;
        float euler = (isFaceRight) ? playerctrl.faceRight: playerctrl.faceLeft;
        playerctrl.gameObject.transform.eulerAngles = new Vector2(0, euler);
        playerctrl.cur_action = setAct;
        playerctrl.canMove = playerCanMove;
    }
    public void movePlayerToTargetWithCamera()
    {
        //Debug.Log("this ok");
        changeCamera();

        float euler = (isFaceRight) ? playerctrl.faceRight : playerctrl.faceLeft;

        playerctrl.gameObject.transform.position = target.transform.position;
        playerctrl.gameObject.transform.eulerAngles = new Vector2(0, euler);
        playerctrl.inArea = nextArea;
        playerctrl.cur_action = setAct;
    }

    void changeCamera()
    {
        cameraAct.SetActive(true);
        cameraNotActive.SetActive(false);
        playerctrl.updateMainCamera(cameraAct.transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!onlyOpenGlbMap) return;
        if (other.CompareTag(triggerTag.ToString()))
        {
            gameMngr.globalMap.gameObject.SetActive(true);
        }
    }
}
