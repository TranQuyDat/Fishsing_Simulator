using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishManager : MonoBehaviour
{
    public List<GameObject> listFishAroundHook;
    public GameObject theLuckyFish;

    private void Update()
    {
        randomFish();
    }
    public void randomFish()
    {
        if(theLuckyFish == null && listFishAroundHook.Count>0)
        {
            theLuckyFish = listFishAroundHook[Random.Range(0, listFishAroundHook.Count)];
            theLuckyFish.GetComponent<fishAI>().playAction(Action.eatBait, 8f);
        }
    }
}
