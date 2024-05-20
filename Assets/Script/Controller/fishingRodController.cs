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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wasCaughtFish = imgvalue.fillAmount >= 1;
    }
}
