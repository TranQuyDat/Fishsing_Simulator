using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class oceanEfect : MonoBehaviour
{
    public float radiusCheck;
    public LayerMask layerCheck;
    public Collider[] hit;


    //Default METHOD
    #region default method
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
        checkOcean();
    }
    #endregion




    //Public METHOD
    #region public method

    public void checkOcean()
    {
        hit = Physics.OverlapSphere(transform.position, radiusCheck, layerCheck);
        if (hit.Length > 0)
        {
            if (hit[0].CompareTag("UnderWater"))
            {
                RenderSettings.fogEndDistance = 50f;
                return;
            }
            if (hit[0].CompareTag("SurfaceWater"))
            {
                RenderSettings.fogEndDistance = 200f;
                return;
            }
        }
    }

    #endregion




    //Gizmos
    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radiusCheck);
    }
    #endregion
}
