using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DrawParent 
{
    public Color color;
    public abstract void Draw();
}
[System.Serializable]
public class DrawRec : DrawParent
{
    public Transform obj1;
    public Transform obj2;
    public bool onlyObj1;
    public Vector3 boxsize = new Vector3(1,1,1);
    public override void Draw()
    {
        
        Gizmos.color = color;
        drawRecWithObj1();
        drawRecWith2Obj();
    }
    public void drawRecWithObj1()
    {
        if (!onlyObj1) return;
        Gizmos.DrawWireCube(obj1.position, boxsize);
    }
    public void drawRecWith2Obj()
    {
        if (obj1 == null || obj2 == null || onlyObj1) return;
        Gizmos.DrawLine(obj1.position, new Vector3(obj1.position.x, obj2.position.y, obj1.position.z));
        Gizmos.DrawLine(obj1.position, new Vector3(obj2.position.x, obj1.position.y, obj1.position.z));

        Gizmos.DrawLine(obj2.position, new Vector3(obj2.position.x, obj1.position.y, obj2.position.z));
        Gizmos.DrawLine(obj2.position, new Vector3(obj1.position.x, obj2.position.y,obj2.position.z));
    }
}
[System.Serializable]
public class DrawLine : DrawParent
{
    public Transform point1;
    public Transform point2;
    public override void Draw()
    {
        if (point1 == null || point2 == null) return;
        Gizmos.color = color;
        Gizmos.DrawLine(point1.position, point2.position);
    }
}
[System.Serializable]
public class DrawDir : DrawParent
{
    public Transform obj;
    public Transform target;
    [Tooltip("need fill target")] public bool showDirToTarget;
    public bool showForward;
    public bool showUp;
    public bool showRight;
    public override void Draw()
    {
        if (obj == null) return;
        
        Gizmos.color = color;
        if (showForward) Gizmos.DrawRay(new Ray(obj.position, obj.forward));
        if (showUp) Gizmos.DrawRay(new Ray(obj.position, obj.up));
        if (showRight) Gizmos.DrawRay(new Ray(obj.position, obj.right));
        
        if (target == null) return;
        if (showDirToTarget)
        {
            Vector3 dir = (target.position - obj.position).normalized;
            Gizmos.DrawRay(new Ray(obj.position,dir));
        }
    }
}
[System.Serializable]
public class Draw
{
    public GizmosTyle tyle;
    public DrawRec[] drawRec;
    public DrawDir[] drawDir;
    public DrawLine[] drawLine;
    public bool canDraw;
    public DrawParent[] curDraw;
    public DrawParent[] getCurDraw()
    {
        curDraw = tyle switch
        {
            GizmosTyle.drawDir => drawDir,
            GizmosTyle.drawRec => drawRec,
            GizmosTyle.drawLine => drawLine,
            _ => null,
        };
        return curDraw;
    }
}
public class drawGizmos : MonoBehaviour
{
    public Draw[] draws;
    
   
    private void OnDrawGizmos()
    {
        if (draws.Length <= 0 || draws == null) return;
        foreach(Draw d in draws)
        {
            if (!d.canDraw) continue;
            foreach (DrawParent dr in d.getCurDraw())
            {
                dr.Draw();
            }
        }
        

        

    }


}

