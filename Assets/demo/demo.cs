using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Sợi dây có 2 điểm: đầu và cuối
    }

    void Update()
    {
        // Cập nhật vị trí của sợi dây theo vị trí của các điểm
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }
}
