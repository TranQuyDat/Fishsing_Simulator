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

    [Range(5,10)]public float castforce = 10f;
    public bool isReeling;
    public bool isCasting;
    public bool startACTionCasting;
    public Transform fishingPoint;
    public Transform dirCast;
    public fishAI fish;
    public bool isPull;
    public float pullforce =0.5f;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        rope1.snapHook = false;
        rope2.snapHook = false;
        rope2.snapRopTip = true;
        rope1.legthRope = 3;
        rope2.legthRope = 3;
        rope1.resetLine();
        rope2.resetLine();
    }
    // Update is called once per frame
    void Update()
    {
        wasCaughtFish = imgvalue.fillAmount >= 1;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isPull = true;
            
        }
        pullrod();
        castLine();
        reelLine();
        Rigidbody rb = rope2.startPos.GetComponent<Rigidbody>();
    }

    public void castLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_cast) return;
        if (!startACTionCasting)
        {
            rope1.snapHook = false;
            rope1.legthRope = 3;
        }
        if (startACTionCasting && !isCasting)
        {
            rope1.snapHook = true;
            rope1.endPost.GetComponent<Rigidbody>().useGravity = true;
            rope1.endPost.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 dir = dirCast.forward + dirCast.up;
            rope1.endPost.GetComponent<Rigidbody>().AddForce(dir*castforce, ForceMode.Impulse);
                isCasting = true;
            return;
        }
        if (rope1.endPost.position.y >= surFaceWaterObj.transform.position.y || !isCasting) return;
        
        gameMngr.playerCtrl.changeAction(Action.fishing_reel);

        

    }
    public void reelLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_reel) return;
        rope1.snapHook = true;
        rope1.endPost.GetComponent<Rigidbody>().isKinematic = true;
        if (!isReeling) rope1.resetLine();
        isCasting = false;
        isReeling = true;

        if (isReeling)
        {
            if (Vector3.Distance(fishingPoint.position, rope1.endPost.position) > 0.5f)
            {
                Vector3 dir = (fishingPoint.position - rope1.endPost.position).normalized;
                rope1.endPost.position += dir * 3f * Time.deltaTime;
                return;
            }

            isReeling = false;
            gameMngr.playerCtrl.changeAction(Action.fishing_idle);
        }
    }

    public void pullrod()
    {
        if (fish == null || !isPull || wasCaughtFish ) return;

        fish.maxDisPull -= 1;
        Vector3 dir = (rope1.startPos.position - fish.head.transform.position).normalized;
        rope2.endPost.position = fish.head.transform.position;
        fish.target.transform.localPosition = fish.target.transform.InverseTransformDirection(dir);
        fish.rb.isKinematic = false;
        fish.rb.AddForce(dir * 10f, ForceMode.Impulse);

        
        isPull = false;
        StartCoroutine(resetPull());
    }
    IEnumerator resetPull()
    {
        yield return new WaitForSeconds(0.1f);
        if (fish.maxDisPull >= 10)  fish.maxDisPull += 1;
    }
}
