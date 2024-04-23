using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMove;
    public float radius;
    public Animator ani;
    public float axis;
    public bool canMove;
    public Rigidbody rb;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ani.SetBool("isrun", false);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        flip();
    }

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
        rb.position = new Vector3(transform.position.x + axis/2 * speedMove , transform.position.y,transform.position.z);
    }

    public void flip()
    {
        
        if (axis > 0)
        {
            float y = -Mathf.Abs(transform.rotation.ToEuler().y * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0,y,0) ;
        }
        else if(axis < 0)
        {
            float y = Mathf.Abs(transform.rotation.ToEuler().y * Mathf.Rad2Deg);
            transform.eulerAngles = new Vector3(0, y, 0);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
