using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fishingRodController : MonoBehaviour
{
    public bool isfishbite;
    public bool wasCaughtFish;
    public GameManager gameMngr;
    public Image imgvalue;
    public lineScript rope1;
    public lineScript rope2;
    public GameObject surFaceWaterObj;

    public float castforce = 10f;
    public bool isReeling;
    public bool isCasting;
    public bool startACTionCasting;
    public Transform fishingPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wasCaughtFish = imgvalue.fillAmount >= 1;
        if(rope1.endPost.position.y <= surFaceWaterObj.transform.position.y)
        {
            rope1.endPost.GetComponent<Rigidbody>().isKinematic = true;
        }
        castLine();
    }

    public void castLine()
    {
        if(isCasting) rope1.legthRope = 3;
        if (startACTionCasting && isCasting)
        {
            rope1.endPost.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 dir =  rope1.endPost.forward;
            rope1.endPost.GetComponent<Rigidbody>().AddForce(dir*castforce, ForceMode.Impulse);
                isCasting = false;
            return;
        }

        
        if (isReeling)
        {
            rope1.endPost.GetComponent<Rigidbody>().isKinematic = true;
            isCasting = false;
            if (Vector3.Distance(fishingPoint.position, rope1.endPost.position) > 1f)
            {
                Vector3 dir = (fishingPoint.position - rope1.endPost.position).normalized;
                rope1.endPost.position += dir * 3f * Time.deltaTime;
            }

            isReeling = false;
        }
    }

}
