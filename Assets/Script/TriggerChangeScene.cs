using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeScene : MonoBehaviour
{
    public GameObject imgIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other != null && other.CompareTag("player"))
        {
            imgIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        imgIcon.SetActive(false);
    }
}
