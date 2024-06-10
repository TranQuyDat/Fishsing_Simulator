using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    public GameManager gameMngr;
    public fishingRodController fishRodCtrl;

    public List<GameObject> listFish;
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
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        fishRodCtrl = gameMngr.fishingRodCtrl;
        curCountFish = listFish.Count;
    }
    private void Update()
    {
        
        spawnFish();
        randomFish();
        updateDisOfTLKFish();

    }
    public void randomFish()
    {
        if(theLuckyFish == null && listFishAroundHook.Count>0)
        {
            theLuckyFish = listFishAroundHook[Random.Range(0, listFishAroundHook.Count)];
            theLuckyFishAI = theLuckyFish.GetComponent<fishAI>();
            
            Vector3 pos = theLuckyFish.transform.position;
            pos.y = surfaceWater.position.y;
            maxdis = (theLuckyFish.transform.position - pos).magnitude;
            theLuckyFishAI.playAction(Action.eatBait, 8f, theLuckyFishAI.acFish == Action.checkBait || theLuckyFishAI.acFish == Action.eatBait);
            gameMngr.fishingRodCtrl.fish = theLuckyFishAI;
        }

        if(theLuckyFish != null && theLuckyFishAI.acFish==Action.idle)
        {
            Reset();
            fishRodCtrl.isfishbite = false;
        }
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
        theLuckyFish = null;
        theLuckyFishAI = null;
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
    public void destroyFish(GameObject fish)
    {
        Destroy(fish);
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
