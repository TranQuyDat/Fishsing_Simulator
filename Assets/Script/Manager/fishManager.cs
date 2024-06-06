using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    public GameManager gameMngr;
    public List<GameObject> listFishAroundHook;
    public GameObject theLuckyFish;
    public Transform surfaceWater;
    public fishAI theLuckyFishAI;
    public float cur_dis;
    public float dis;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        randomFish();
        updateDisOfTLKFish();

    }
    public void randomFish()
    {
        if(theLuckyFish == null && listFishAroundHook.Count>0)
        {
            theLuckyFish = listFishAroundHook[Random.Range(0, listFishAroundHook.Count)];
            theLuckyFishAI = theLuckyFish.GetComponent<fishAI>();
            Vector3 pos = gameMngr.fishingRodCtrl.rope1.startPos.position;
            cur_dis = (theLuckyFish.transform.position - pos).magnitude;
            theLuckyFishAI.playAction(Action.eatBait, 8f, theLuckyFishAI.acFish == Action.checkBait || theLuckyFishAI.acFish == Action.eatBait);
            gameMngr.fishingRodCtrl.fish = theLuckyFishAI;
        }
    }

    public void updateDisOfTLKFish()
    {
        if (theLuckyFish == null) return;
        Vector3 pos = new Vector3(theLuckyFish.transform.position.x, surfaceWater.position.y, theLuckyFish.transform.position.z);
        dis = (theLuckyFish.transform.position - pos).magnitude;
        if (cur_dis < dis)
        {
            cur_dis = dis;
        }
    }

    private void OnDrawGizmos()
    {
        if (theLuckyFish == null) return;
        Gizmos.color = Color.white;
        Vector3 pos = new Vector3(theLuckyFish.transform.position.x, surfaceWater.position.y, theLuckyFish.transform.position.z);
        Gizmos.DrawLine(theLuckyFish.transform.position, pos);
    }
}
