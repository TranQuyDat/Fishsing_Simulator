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
    #region default method
    private void Awake()
    {
        line = ropeTranform.GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        numpos = (int)(legthRope / gap);
        line.positionCount = numpos;
    }

    // Update is called once per frame
    void Update()
    {
        numpos = (int)(legthRope / gap);
        if (listNode.Count != numpos)
        {
            //destroyAllNode();
            line.positionCount = numpos;
            //Debug.Log(listNode.Count);
            if (listNode.Count < numpos)
            {
                destroyAllNode();
                spawnNode();
            }
            else if (listNode.Count > numpos) destroyNode();
        }
        if (listNode.Count > 0)
        {
            line.SetPositions(getallPosChild());
            ruleNode();
        }
    }
    #endregion





    #region public method
    public void ruleNode()
    {
        for (int i = 0; i < numpos ; i++)
        {
            if (snapHook && i == numpos-1 || snapRopTip && i == 0)
            {
                continue;
            }
            listNode[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            listNode[i].GetComponent<CharacterJoint>().connectedBody = listNode[i - 1].GetComponent<Rigidbody>();
        }
        if (snapHook)
        {

            listNode[listNode.Count - 1].GetComponent<CharacterJoint>().connectedBody = listNode[listNode.Count - 2].GetComponent<Rigidbody>();
            listNode[listNode.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            listNode[listNode.Count - 1].transform.position = hook.position;
            legthRope = (int)(rodTip.position - hook.position).magnitude;

        }
        else
        {
            listNode[listNode.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hook.transform.position = listNode[listNode.Count - 1].transform.position;
        }

        if (snapRopTip)
        {
            Destroy(listNode[0].GetComponent<CharacterJoint>());
            listNode[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            listNode[0].transform.position = rodTip.position;
        }
        
    }

    public void spawnNode()
    {
        Vector3 dir = (rodTip.position - hook.position).normalized;
        for (int i = 0 ; i < numpos ; i++)
        {
            GameObject node =  Instantiate(prefapLine,rodTip.position  - (dir * gap*i), Quaternion.identity, ropeTranform);
            listNode.Add(node);
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
        if (listNode.Count <= 0) return;
        for (int i = listNode.Count-1 ; i >=0 ;i--)
        {
            Destroy(listNode[i].gameObject);
        }
        listNode.Clear();
    }
    public void destroyNode()
    {
        if (listNode.Count <= 0) return;
        
        listNode[1].GetComponent<CharacterJoint>().connectedBody = null;
        listNode[1].transform.position = rodTip.position;
        Destroy(listNode[0]);
        listNode.RemoveAt(0);
        
    }
    #endregion
}
