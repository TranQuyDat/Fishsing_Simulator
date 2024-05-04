using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class lineScript : MonoBehaviour
{
    public GameObject prefapLine;
    public Transform rodTip;
    public Transform hook;
    public Transform ropeTranform;
    [Range(3,10)] public int legthRope;
    public float gap;
    public bool snapRopTip = true;
    public bool snapHook = true;
    int numpos;
    int cur_numpos;
    LineRenderer line;

   public List<GameObject> listNode;
    private void Awake()
    {
        line = ropeTranform.GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        legthRope = Mathf.RoundToInt((hook.position - rodTip.position).magnitude);
        numpos = (int)(legthRope / gap);
        cur_numpos = numpos;
        line.positionCount = numpos;
        physicLine();
    }

    // Update is called once per frame
    void Update()
    {
        legthRope = Mathf.RoundToInt((hook.position - rodTip.position).magnitude);
        numpos = (int)(legthRope / gap);
        if (cur_numpos != numpos)
        {
            cur_numpos = numpos;
            destroyAllNode();
            line.positionCount = numpos;
            //Debug.Log(listNode.Count);
            if(listNode.Count <= 0 ) physicLine();
        }
        if(listNode.Count > 0) line.SetPositions(getallPosChild());
        if (snapHook && listNode.Count>1)
        {
            listNode[listNode.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            listNode[listNode.Count - 1].transform.position = hook.position;
        }
    }

    public void physicLine()
    {
        Vector3 dir = (rodTip.position - hook.position).normalized;
        for(int i =0;i < numpos; i++)
        {
            GameObject node =  Instantiate(prefapLine,rodTip.position - (dir * (gap * i)), Quaternion.identity, ropeTranform);
            listNode.Add(node);
            if (i == 0)
            {
                Destroy(node.GetComponent<CharacterJoint>());
                if (snapRopTip)
                {
                    node.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                continue;
            }
            
            //Debug.Log(ropeTranform.GetChild(i - 1).name);
            node.GetComponent<CharacterJoint>().connectedBody = listNode[i - 1].GetComponent<Rigidbody>();
        }
        
    }

    public Vector3[] getallPosChild()
    {
        Vector3[] listChild = new Vector3[listNode.Count];
        int i = 0;
        foreach (GameObject c in listNode)
        {
            listChild.SetValue(c.transform.position, i);
            i++;
        }
        return listChild;
    }
    public void destroyAllNode()
    {
        for (int i = listNode.Count-1 ; i >=0 ;i--)
        {
            Destroy(listNode[i].gameObject);
        }
        listNode.Clear();
    }
}
