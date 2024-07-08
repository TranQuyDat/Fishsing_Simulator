using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    public GameManager gameMngr;
    public fishingRodController fishRodCtrl;

    public List<GameObject> listFish;
    public List<GameObject> listFishLikeBait;
    public List<GameObject> listFishAroundHook;
    public GameObject theLuckyFish;
    public fishAI theLuckyFishAI;

    public List<GameObject> listPrefapFish;
    public List<Transform> listPosSpawn;

    public Transform surfaceWater;
    public Transform Minlimit;
    public Transform Maxlimit;

    public float maxdis;
    public float dis;
    public int maxCountFish;
    public int curCountFish;
    public float timeRandomfish1 = 3;
    public float timeRandomfish2 = 5;

    float timeDF1;
    float timeDF2;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        fishRodCtrl = gameMngr.fishingRodCtrl;
        curCountFish = listFish.Count;
    }

    private void Start()
    {
        timeDF1 = timeRandomfish1;
        timeDF2 = timeRandomfish2;
    }
    private void Update()
    {
        spawnFish();
        randomFish();
        updateDisOfTLKFish();
        if (theLuckyFish != null && theLuckyFishAI.acFish == Action.eatBait && listFishAroundHook.Count > 0)
        {
            listFishAroundHook.Remove(theLuckyFish);
            GameObject obj = listFishAroundHook[0];
            listFishAroundHook.Clear();
            obj.GetComponent<fishAI>().food = null;
        }

        
    }
    public void randomFish()
    {
        if (theLuckyFish != null || listFishLikeBait.Count <= 0 || !fishRodCtrl.isfishing) return;
        if(listFishAroundHook.Count <= 0) timeRandomfish1 -= 1 * Time.deltaTime;
        if (listFishAroundHook.Count >= 2) timeRandomfish2 -= 1 * Time.deltaTime;

        // random fish in list fish  like bait => 2 fish add to listFishAroundHook (timeRandomfish1)

        if (timeRandomfish1 <= 0 && listFishAroundHook.Count < 2)
        {
            GameObject fish1 = listFishLikeBait[Random.Range(0, listFishLikeBait.Count)];
            listFishAroundHook.Add(fish1);
            fish1.GetComponent<fishAI>().food = fishRodCtrl.bait.gameObject;
            listFishLikeBait.Remove(fish1);

            GameObject fish2 = listFishLikeBait[Random.Range(0, listFishLikeBait.Count)];
            listFishAroundHook.Add(fish2);
            fish2.GetComponent<fishAI>().food = fishRodCtrl.bait.gameObject;
            listFishLikeBait.Remove(fish2);
            timeRandomfish1 = timeDF1;
        }

        //random fish in list fish around hook => 1 fish add to the lucky fish; (timeRandomfish2)

        if (timeRandomfish2 <= 0)
        {
            theLuckyFish = listFishAroundHook[Random.Range(0, listFishAroundHook.Count)];

            //setUp the lucky fish
            theLuckyFishAI = theLuckyFish.GetComponent<fishAI>();
            Vector3 pos = theLuckyFish.transform.position;
            pos.y = surfaceWater.position.y;
            maxdis = (theLuckyFish.transform.position - pos).magnitude;
            theLuckyFishAI.playAction(Action.eatBait, 8f, theLuckyFishAI.acFish == Action.checkBait || theLuckyFishAI.acFish == Action.eatBait);
            gameMngr.fishingRodCtrl.fish = theLuckyFishAI;
            timeRandomfish2 = timeDF2;
            
        }

    }



    public void StopFishing()
    {
        gameMngr.uiMngr.gamePlay.btnStopFishing();
    }

    public void updateDisOfTLKFish()
    {
        if (theLuckyFish == null) return;

        Vector3 pos = theLuckyFish.transform.position;
        pos.y = surfaceWater.position.y;
        dis = (theLuckyFish.transform.position - pos).magnitude;
        if (maxdis < dis)
        {
            maxdis = dis;
        }
    }



    public void Reset()
    {
        listFishAroundHook.Clear();
        listFishLikeBait.Clear();
        theLuckyFish = null;
        theLuckyFishAI = null;
        dis = 0;
        maxdis = 0;
    }



    public void spawnFish()
    {
        if (curCountFish != listFish.Count)
            curCountFish = listFish.Count;

        if (curCountFish < maxCountFish)
        {
            int ranPrefap = Random.Range(0, listPrefapFish.Count);
            int ranPos = Random.Range(0, listPosSpawn.Count);
            GameObject fish = Instantiate(listPrefapFish[ranPrefap], listPosSpawn[ranPos].position, Quaternion.identity, transform);
            fishAI fAI = fish.GetComponent<fishAI>();
            fAI.setDefault();

            listFish.Add(fish);
        }
    }
 

    #region gizmos
    private void OnDrawGizmos()
    {
        if (theLuckyFish == null) return;
        Gizmos.color = Color.white;

        Vector3 pos = theLuckyFish.transform.position;
        pos.y = surfaceWater.position.y;
        Gizmos.DrawLine(theLuckyFish.transform.position, pos);
    }
    #endregion
}
