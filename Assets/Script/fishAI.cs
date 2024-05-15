using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAI : MonoBehaviour
{

    public float speed;
    public float timeChangeDir;
    public float olfaction;
    public Transform limitXYmin;
    public Transform limitXYmax;
    public GameObject head;
    public GameObject target;
    public GameObject food;
    public LayerMask maskFood;
    public bait[] likefood;
    public bool isMeetFood;
    public bool canMove = true;
    public Collider[] checkBait;
    public fishManager fishMngr;
    public Action acFish;
    Vector3 dir;
    float x = 3f;
    bool isAroundHook;
    private void Awake()
    {
        fishMngr = transform.parent.GetComponent<fishManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("changeDir", 0, timeChangeDir);
    }

    // Update is called once per frame
    void Update()
    {
        checkLikeFood();
        changeDir();
        eatEvent();
        //movement
        if (canMove && (target.transform.position - head.transform.position).magnitude >=0.5f )
            transform.transform.position += (target.transform.position - head.transform.position).normalized * speed * Time.deltaTime;

        followTarget();
       
    }
    public void followTarget()
    {
        head.transform.LookAt(target.transform.position);
        head.transform.Rotate(-90, 0, 0, Space.Self);
        head.transform.Rotate(0, 90, 0, Space.Self);


        if(!isMeetFood) updateTarget(target);
    }

    public void updateTarget(GameObject obj)
    {
        if (target.transform.position.z >= x / 2 && x > 0) x = -x;
        if (target.transform.position.z <= x / 2 && x < 0) x = -x;
        target.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y,
            obj.transform.position.z + x * Time.deltaTime);
    } 
    public void changeDir()
    {
        bool islimit = (target.transform.position.x <= limitXYmin.position.x || target.transform.position.y <= limitXYmin.position.y ||
                        target.transform.position.x >= limitXYmax.position.x || target.transform.position.y >= limitXYmax.position.y);

        if (!islimit || isMeetFood ) return;
        float randX = Random.Range(limitXYmin.position.x, limitXYmax.position.x);
        float randY = Random.Range(transform.position.y-2f, transform.position.y + 2f);

        Vector3 newPos = new Vector3(randX,randY, 0);
        dir = (newPos - target.transform.transform.position).normalized;
        target.transform.localPosition = target.transform.InverseTransformDirection(dir);
        timeChangeDir = Random.RandomRange(2f, 10f);
        CancelInvoke("changeDir");
        InvokeRepeating("changeDir", timeChangeDir, timeChangeDir);
    }

    public void checkLikeFood()
    {
        checkBait = Physics.OverlapSphere(head.transform.position, olfaction, maskFood);
        if (checkBait == null || checkBait.Length <= 0)
        {
            isMeetFood = false;
            return;
        }
        if (acFish == Action.checkBait || acFish == Action.eatBait || acFish == Action.ateBait) return;
        foreach(bait b in likefood)
        {
            if (checkBait[0].tag != b.ToString()) continue; // check like food(bait)

            isMeetFood = true;
            acFish = Action.checkBait;
            food = checkBait[0].gameObject;
            return;
        }
        
        
    }

    float timeEventEat;
    public void moveAroundfood(float orbitRadius, float orbitSpeed )
    {
        timeEventEat += 1 * Time.deltaTime;
        float numDir = 1;
        // hanh dong den gan moi && ktra moi( bang cach di quanh moi)
        if (timeEventEat > 3f)
        {
            timeEventEat = 0;
            numDir = Random.Range(0, 2)*2-1 ; // de thay doi huong quay
        }
        if (!isAroundHook)
        {
            isAroundHook = true;
            fishMngr.listFishAroundHook.Add(this.gameObject);// them vao list around hook
        }
        float eulerStart = 360f / (fishMngr.listFishAroundHook.IndexOf(this.gameObject) + 1);// goc bat dau quay cho moi con ca
        // Tính toán vị trí quay xung quanh vật thể mục tiêu
        float orbitAngle = (Time.time * orbitSpeed + eulerStart) * numDir;
        float x = Mathf.Cos(orbitAngle) * orbitRadius;
        float z = Mathf.Sin(orbitAngle) * orbitRadius;
        target.transform.position = new Vector3(x, 0, z) + food.transform.position;
    }
    public void fishEatBait()
    {
        acFish = Action.eatBait;
        if (acFish != Action.eatBait) return;
        target.transform.position = food.transform.position;
        acFish = Action.ateBait;


    }
    public void eatEvent()
    {
        if (!isMeetFood || 
           (!fishMngr.listFishAroundHook.Contains(this.gameObject) && fishMngr.listFishAroundHook.Count>=2))
        {
            food = null;
            return;
        }
        //fish around bait
        if (acFish == Action.checkBait)
        {
            moveAroundfood(2f, 1.5f);
            Invoke("fishEatBait", 8f);
            return;
        }
        //fish eat bait
        
    }

    #region gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(head.transform.position, olfaction);
        Gizmos.color = Color.black;
        if (target == null || head == null) return;
        
        Ray r = new Ray(head.transform.position, (target.transform.position - head.transform.position));
        Gizmos.DrawRay(r);

        Gizmos.DrawSphere(target.transform.position, 0.3f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(head.transform.position, head.transform.up * 3f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.right * 3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(head.transform.position, head.transform.forward * 3f);

        
    }

    #endregion
}
