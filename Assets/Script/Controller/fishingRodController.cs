using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class fishingRodController : MonoBehaviour
{
    public GameManager gameMngr;
    public bool isfishbite;
    public bool wasCaughtFish;
    
    public Image imgvalue;

    public Transform rodtip;
    public Transform hook;
    public Transform bait;

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
        wasCaughtFish = false;
        isfishbite = false;
        rope1.snapHook = false;
        rope2.snapHook = false;
        rope2.snapRopTip = true;
        rope1.legthRope = 2;
        rope2.legthRope = 2;
        rope1.resetLine();
        rope2.resetLine();
    }
    // Update is called once per frame
    void Update()
    {

        castLine();
        reelLine();
        pullrod();
        fishWasCaught();
    }

    public void Reset()
    {
        wasCaughtFish = false;
        isfishbite = false;
        rope1.snapHook = false;
        rope2.snapHook = false;
        rope2.snapRopTip = true;
        rope1.legthRope = 2;
        rope2.legthRope = 2;
        rope1.resetLine();
        rope2.resetLine();
    }
    private void FixedUpdate()
    {
        fishWasBite();
    }
    public void fishWasBite()
    {
        if (!isfishbite) return;
        hook.position = fish.head.transform.position;
        
    }
    public void castLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_cast) return;
        if (!startACTionCasting)
        {
            rope1.snapHook = false;
            rope1.legthRope = 2;
            rope2.legthRope = 2;
        }
        if (startACTionCasting && !isCasting)
        {
            rope1.snapHook = true;
            rope1.endPos.GetComponent<Rigidbody>().useGravity = true;
            rope1.endPos.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 dir = dirCast.forward + dirCast.up;
            rope1.endPos.GetComponent<Rigidbody>().AddForce(dir*castforce, ForceMode.Impulse);
                isCasting = true;
            return;
        }
        if (rope1.endPos.position.y >= surFaceWaterObj.transform.position.y || !isCasting) return;
        
        gameMngr.playerCtrl.changeAction(Action.fishing_reel);

        

    }
    public void reelLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_reel) return;
        rope1.snapHook = true;
        rope1.endPos.GetComponent<Rigidbody>().isKinematic = true;
        if (!isReeling) rope1.resetLine();
        isCasting = false;
        isReeling = true;

        if (isReeling)
        {
            rope2.legthRope = 5f;
            Vector3 pos = fishingPoint.position;
            pos.y = surFaceWaterObj.transform.position.y-0.5f;
            if (Vector3.Distance(pos, rope1.endPos.position) > 0.5f)
            {
                Vector3 dir = (pos - rope1.endPos.position).normalized;
                rope1.endPos.position += dir * 3f * Time.deltaTime;
                return;
            }

            rope1.resetLine();
            rope2.resetLine();
            isReeling = false;
            gameMngr.playerCtrl.changeAction(Action.fishing_idle);
        }
    }

    public void pullrod()
    {
        if (fish == null || !isPull || wasCaughtFish)
        {
            return;
        }

            Vector3 dir = (rope1.startPos.position - fish.head.transform.position).normalized;
        fish.rb.isKinematic = false;
        fish.rb.AddForce(dir * 10f, ForceMode.Impulse);

        
        if (fish.fishMngr.dis < fish.maxDisPull - 2 || 
            fish.transform.position.y>surFaceWaterObj.transform.position.y)
        {
            if(fish.maxDisPull > fish.minDisPull) 
                fish.maxDisPull -= 1;

            fish.rb.isKinematic = true;
            isPull = false;
        }

        if(fish.maxDisPull <= fish.minDisPull)
        {
            isPull = false;
        }
        
        
    }

    public void fishWasCaught()
    {
        wasCaughtFish = imgvalue.fillAmount >= 0.99f;
        if (!wasCaughtFish) return;
        #region set Default Rod
        if (isfishbite) 
        {
            isfishbite = false;
            rope1.snapHook = false;
            rope2.snapHook = false;
            rope2.snapRopTip = true;
            rope1.legthRope = 2;
            rope2.legthRope = 2;
            rope1.resetLine();
            rope2.resetLine();
        }
        #endregion
        fish.rb.isKinematic = true;
        fish.head.transform.position = hook.position;
        fish.target.transform.position = rodtip.position;

    }
}
