using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DrawParent 
{
    public abstract void Draw();
}
[System.Serializable]
public class DrawRec : DrawParent
{
    public Transform obj1;
    public Transform obj2;

    public override void Draw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(obj1.position, new Vector3(obj1.position.x, obj2.position.y));
        Gizmos.DrawLine(obj1.position, new Vector3(obj2.position.x, obj1.position.y));

        Gizmos.DrawLine(obj2.position, new Vector3(obj2.position.x, obj1.position.y));
        Gizmos.DrawLine(obj2.position, new Vector3(obj1.position.x, obj2.position.y));
    }

}
[System.Serializable]
public class DrawDir : DrawParent
{
    public Transform obj;
    public bool showForward;
    public bool showUp;
    public bool showRight;
    public override void Draw()
    {
        Gizmos.color = Color.blue;
        if(showForward) Gizmos.DrawRay(new Ray(obj.position, obj.forward));
        if(showUp) Gizmos.DrawRay(new Ray(obj.position, obj.up));
        if(showRight) Gizmos.DrawRay(new Ray(obj.position, obj.right));
    }
}
public class drawGizmos : MonoBehaviour
{
    public GizmosTyle tyle;
    public DrawRec[] drawRec;
    public DrawDir[] drawDir;
    public bool canDraw;
    public DrawParent[] curDraw;
    public void switchdraw()
    {
        curDraw = tyle switch
        {
            GizmosTyle.drawDir => drawDir,
            GizmosTyle.drawRec => drawRec,
            _=>null,
        };
    }
    private void OnDrawGizmos()
    {
        switchdraw();
        if (!canDraw || curDraw == null) return;
        foreach(DrawParent d in curDraw)
        {
            d.Draw();
        }
        

    }


}
