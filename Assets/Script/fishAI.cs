using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAI : MonoBehaviour
{

    public float speed;
    public float timeChangeDir;
    public Transform limitXYmin;
    public Transform limitXYmax;
    public GameObject head;
    public GameObject target;
    Vector3 dir;

    float x = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("changeDir", 0, timeChangeDir);
    }

    // Update is called once per frame
    void Update()
    {
        bool islimit = (target.transform.position.x <= limitXYmin.position.x || target.transform.position.y <= limitXYmin.position.y ||
                        target.transform.position.x >= limitXYmax.position.x || target.transform.position.y >= limitXYmax.position.y);
        if (islimit)
        {
            changeDir();
        }
        
        if((target.transform.position - head.transform.position).magnitude >=2f)
            transform.transform.position += (target.transform.position - head.transform.position).normalized * speed * Time.deltaTime;

        followTarget();
       
    }
    public void followTarget()
    {
        head.transform.LookAt(target.transform.position);
        head.transform.Rotate(-90, 0, 0, Space.Self);
        head.transform.Rotate(0, 90, 0, Space.Self);

        target.transform.localPosition = dir;
        if (target.transform.position.z >= x/2 && x>0) x = -x;
        if(target.transform.position.z <= x/2 && x<0) x = -x;
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,
            target.transform.position.z + x *Time.deltaTime );
    }

    public void changeDir()
    {
        float randX = Random.Range(limitXYmin.position.x, limitXYmax.position.x);
        float randY = Random.Range(limitXYmin.position.y, limitXYmax.position.y);
        while(randY/randX > 0.5f)
        {
            randX = Random.Range(limitXYmin.position.x, limitXYmax.position.x);
            randY = Random.Range(limitXYmin.position.y, limitXYmax.position.y);
        }
        Vector3 newPos = new Vector3(randX,randY, 0);
        dir = (newPos - target.transform.transform.position).normalized;
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Ray r = new Ray(head.transform.position, (target.transform.position-head.transform.position));
        Gizmos.DrawRay(r);
        Gizmos.DrawSphere(target.transform.position, 0.3f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(head.transform.position, head.transform.up * 3f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.right * 3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(head.transform.position, head.transform.forward * 3f);

        
    }
}
