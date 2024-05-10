using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawGizmos : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(obj1.position, new Vector3(obj1.position.x, obj2.position.y));
        Gizmos.DrawLine(obj1.position, new Vector3(obj2.position.x, obj1.position.y));

        Gizmos.DrawLine(obj2.position, new Vector3(obj2.position.x, obj1.position.y));
        Gizmos.DrawLine(obj2.position, new Vector3(obj1.position.x, obj2.position.y));
    }
}
