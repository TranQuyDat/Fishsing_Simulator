using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineScript : MonoBehaviour
{

    public Transform rodTip;
    public Transform hook;
    public int countPos=20;
    public float ropeLenght=5f;
    public float sagFactor = 0.1f;


    LineRenderer line;
    float k; //khoang cach giua 2 diem
    float x; // do dai day /2
    float y; // toa do cua mox cau
    float a;// do cong cua day
    private void Awake()
    {
        line = this.GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.positionCount = countPos;
        ropeLenght =  Vector3.Distance(rodTip.position, hook.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (line.positionCount != countPos)
        {
            line.positionCount = countPos;
        }

        physicLine();

    }

    public void physicLine()
    {
        a = ropeLenght * sagFactor;
        k = ropeLenght / (countPos - 1);
        for (int i =0; i< countPos; i++)
        {
            
            x = -ropeLenght / 2f + k * i;
            y = a * (Mathf.Exp(x / a) + Mathf.Exp(-x / a)) / 2f;
            Vector3 pointOnRope = Vector3.Lerp(rodTip.position, hook.position, (float)i / (countPos - 1)) + Vector3.up * y;

            line.SetPosition(i,pointOnRope);
        }
    }
}
