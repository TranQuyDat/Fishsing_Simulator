using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class fishingRodController : MonoBehaviour
{
    public GameManager gameMngr;
    public PlayerController playerCTRL;
    public bool isfishbite;
    public bool wasCaughtFish;
    
    
    public Transform rodtip;
    public Transform hook;
    public Transform bait;

    public lineScript rope1;
    public lineScript rope2;
    
    public GameObject surFaceWaterObj;

    [Range(5,10)]public float castforce = 10f;
    public bool isReeling;
    public bool isCasting;
    public bool isfishing;
    public bool startACTionCasting;
    public Transform fishingPoint;
    public Transform dirCast;
    public fishAI fish;
    public bool isPull;
    public float pullforce =0.5f;

    private void Awake()
    {
        surFaceWaterObj = GameObject.FindGameObjectWithTag("water");
        gameMngr = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMngr!=null && gameMngr.fishingRodCtrl ==null )
        {
            gameMngr = playerCTRL.gameMngr;
            gameMngr.fishingRodCtrl = this;
            gameMngr.fishMngr.fishRodCtrl = this;
        }
        castLine();
        reelLine();
        pullrod();
        fishWasCaught();
        activeDeactive_Bait();
    }
    private void FixedUpdate()
    {
        fishWasBite();
    }
    private void OnEnable()
    {
        gameMngr.fishMngr.Reset();
        Reset();
        gameMngr.uiMngr.gamePlay.fishingUI.playAniFishingNormal();
    }

    public void importData(DataRod dataRod)
    {
        bait.tag = dataRod.tagBait;
        MeshFilter meshBai = bait.GetComponent<MeshFilter>();
        MeshRenderer matBait = bait.GetComponent<MeshRenderer>();
        meshBai.mesh = DataSave.GetDtFrPath<Mesh>(dataRod.meshBaitPath);
        matBait.sharedMaterial = DataSave.GetDtFrPath<Material>(dataRod.matBaitPath);
        bait.transform.localScale = dataRod.scale;
    }
    public DataRod exportData()
    {
        MeshFilter meshBai = bait.GetComponent<MeshFilter>();
        MeshRenderer matBait = bait.GetComponent<MeshRenderer>();
        DataRod dataRod = new DataRod(bait.tag, meshBai.sharedMesh, matBait.sharedMaterial,bait.transform.localScale);
        return dataRod;
    }
    public void Reset()
    {
        gameMngr.playerCtrl.changeAction(Action.fishing_idle);
        rope1.endPos.GetComponent<Rigidbody>().isKinematic = true;
        wasCaughtFish = false;
        isfishbite = false;
        isfishing = false;
        isCasting = false;
        isReeling = false;
        startACTionCasting = false;

        resetHookAndBuoys();
        rope1.snapHook = false;
        rope2.snapHook = false;
        rope2.snapRopTip = true;
        rope1.legthRope = 2;
        rope2.legthRope = 2;
        rope1.resetLine();
        rope2.resetLine();
        fish = null;
    }
    public void resetHookAndBuoys()
    {
        rope1.snapHook = true;
        rope2.snapHook = true;
        Vector3 dir = (fishingPoint.position - rodtip.position);
        rope1.endPos.position = rodtip.position+ dir * 0.5f;
        hook.position = rodtip.position+ dir * 1f;
    }
    public void activeDeactive_Bait()
    {
        if(bait.CompareTag("noneBait") && bait.gameObject.active)
        {
            bait.gameObject.SetActive(false);
            return;
        }
        if(!bait.CompareTag("noneBait") && !bait.gameObject.active)
        {
            bait.gameObject.SetActive(true);
        }
    }
    public void fishWasBite()
    {
        if (!isfishbite) return;
        hook.position = fish.head.transform.position;
        
    }
    public void castLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_cast) return;

        isfishing = false;
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
        if (surFaceWaterObj == null) surFaceWaterObj = GameObject.FindGameObjectWithTag("water"); ;
        if (rope1.endPos.position.y >= surFaceWaterObj.transform.position.y || !isCasting) return;
        
        gameMngr.playerCtrl.changeAction(Action.fishing_reel);

        

    }
    public void reelLine()
    {
        if (gameMngr.playerCtrl.cur_action != Action.fishing_reel) return;
        rope1.snapHook = true;
        rope1.endPos.GetComponent<Rigidbody>().isKinematic = true;
        isCasting = false;
        isReeling = true;
        isfishing = false;
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
            isfishing = true;
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
        wasCaughtFish = gameMngr.uiMngr.gamePlay.editImgUI.imgValue.fillAmount >= 0.99f;
        if (!wasCaughtFish || fish == null) return;
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
