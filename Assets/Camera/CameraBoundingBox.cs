using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundingBox : MonoBehaviour
{
    PolygonCollider2D boundingBox;
    [SerializeField] CompositeCollider2D hubCompositeCollider;

    public void BakeBoundingBox() {
        boundingBox = GetComponent<PolygonCollider2D>();
        boundingBox.pathCount = 1;
        List<Vector2> path = new List<Vector2>();
        Vector2 point;
        point = new Vector2(hubCompositeCollider.bounds.min.x, hubCompositeCollider.bounds.min.y);
        path.Add(point);
        point = new Vector2(hubCompositeCollider.bounds.min.x, hubCompositeCollider.bounds.max.y);
        path.Add(point);
        point = new Vector2(hubCompositeCollider.bounds.max.x, hubCompositeCollider.bounds.max.y);
        path.Add(point);
        point = new Vector2(hubCompositeCollider.bounds.max.x, hubCompositeCollider.bounds.min.y);
        path.Add(point);
        boundingBox.SetPath(0, path);
    }

}
