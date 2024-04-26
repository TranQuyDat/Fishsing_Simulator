using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public values
    public float speedMove;
    public float radius;
    public Animator ani;
    public float axis;
    public bool canMove;
    public Rigidbody rb;
    public GameObject dirobj;
    public float eulerY;
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
        eulerY = transform.rotation.ToEuler().y * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log((dirobj.transform.position - rb.position).normalized);

        movement();
        flip();
    }
    #endregion


    #region public method
    public void movement()
    {
        if (!canMove)
        {
            ani.SetBool("isrun", false);
            return;
        }
        if (axis != 0) ani.SetBool("isrun", true);
        else ani.SetBool("isrun", false);

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
        float right = eulerY ;
        float left =right+180 ;
        Vector3 dir = transform.TransformDirection(rb.transform.right).normalized;
        Debug.Log(Vector3.SignedAngle(transform.right, dir,Vector3.up));
        if (axis > 0 )
        {
            transform.eulerAngles = new Vector3(0,right,0) ;
        }
        else if(axis < 0 )
        {
            transform.eulerAngles = new Vector3(0, left, 0);
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
