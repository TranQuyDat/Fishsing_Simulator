using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    public GameManager gameMngr;
    public List<GameObject> listFishAroundHook;
    public GameObject theLuckyFish;
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
            cur_dis = (theLuckyFishAI.head.transform.position - theLuckyFishAI.food.transform.parent.position).magnitude;
            theLuckyFishAI.playAction(Action.eatBait, 8f, theLuckyFishAI.acFish == Action.checkBait || theLuckyFishAI.acFish == Action.eatBait);
        }
    }

    public void updateDisOfTLKFish()
    {
        if (theLuckyFish == null) return;
        dis = (theLuckyFishAI.head.transform.position - theLuckyFishAI.food.transform.parent.position).magnitude;
        if (cur_dis < dis)
        {
            cur_dis = dis;
        }
    }
}
