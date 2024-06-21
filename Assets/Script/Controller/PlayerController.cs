using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public values
    public float speedMove;
    public Animator ani;
    public float axis;
    public bool canMove;
    public Rigidbody rb;
    public GameObject dirobj;
    public Transform mainCamera;
    public area inArea;
    public Action cur_action;
    public GameObject targetNPC;
    public float faceRight;
    public float faceLeft;
    public Vector3 boxSize = new Vector3(1,1,1);
    public LayerMask maskNPC;
    public GameObject instructionBTN;
    #endregion


    #region Default Method
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ani.SetBool("isrun", false);
        cur_action = Action.idle;
        faceRight = (mainCamera.rotation.ToEuler() * Mathf.Rad2Deg).y - 90;
        faceLeft = faceRight + 180;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log((dirobj.transform.position - rb.position).normalized);

        movement();
        flip();
        checkNPC();
    }
    #endregion


    #region public method
    public void movement()
    {
        if (!canMove) return;
        if (axis != 0)
        {
            cur_action = Action.running;
        }
        else
        {
            cur_action = Action.idle;
        }

        axis = Input.GetAxis("Horizontal");
        if (axis != 0)
        {
            Vector3 dir = transform.InverseTransformDirection((dirobj.transform.position - rb.position).normalized);
            
            rb.transform.Translate(dir * speedMove * Time.deltaTime);// movement
        }
    }

    public void flip()
    {
        if (axis == 0) return;
        if (axis > 0 )
        {
            transform.eulerAngles = new Vector3(0,faceRight,0) ;
        }
        else if(axis < 0 )
        {
            transform.eulerAngles = new Vector3(0, faceLeft, 0);
        }
    }

    public void setCanMove(bool canmove)
    {
        canMove = canmove;
    }
    public void changeAction(Action ac)
    {
        this.cur_action = ac;
    }
    public void updateMainCamera( Transform newCamera )
    {
        mainCamera = newCamera;
        faceRight = (newCamera.rotation.ToEuler() *Mathf.Rad2Deg).y - 90;
        faceLeft = faceRight + 180;
    }

    public void updateTranform(Transform p,area a)
    {
        if (inArea == a)
        {
            rb.transform.SetParent(p);
            return;
        }
        rb.transform.SetParent(null);
    }
    public Collider[] npcs;
    public void checkNPC()
    {
        npcs = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity, maskNPC);
        if (npcs.Length > 0)
        {
            NPC npc = npcs[0].GetComponent<NPC>();
            targetNPC = npcs[0].gameObject;
            Vector3 offset = npcs[0].bounds.max;
            Vector3 pos = new Vector3(npcs[0].transform.position.x,offset.y+1f, npcs[0].transform.position.z);
            //di chuyen den npc
            instructionBTN.transform.position = pos ;
            //hien thi instructionBTN
            instructionBTN.SetActive(true);
            npc.showOption = true;
            if (npc.dia.gameObject.active)
            {
                instructionBTN.SetActive(false);
            }
        }
        else
        {
            // an icon
            instructionBTN.SetActive(false);
        }

    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
