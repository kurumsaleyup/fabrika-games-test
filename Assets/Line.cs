using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Rigidbody rigidBody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;
    public List<CapsuleCollider> listCapsuleColliders = new List<CapsuleCollider>();

    float pointsMinDistance = 0.1f;

    float capsuleColliderRadius;

    public void AddPoint(Vector2 newPoint)
    {

        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        pointsCount++;

        listCapsuleColliders.Add(this.gameObject.AddComponent<CapsuleCollider>());
        listCapsuleColliders[listCapsuleColliders.Count - 1].center = newPoint;
        listCapsuleColliders[listCapsuleColliders.Count - 1].radius = capsuleColliderRadius;
        listCapsuleColliders[listCapsuleColliders.Count - 1].direction = 0;
        listCapsuleColliders[listCapsuleColliders.Count - 1].height = capsuleColliderRadius;

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

    }


    public void ResetLine()
    {
        pointsCount = 0;
        lineRenderer.positionCount = pointsCount;
        for (int i = listCapsuleColliders.Count - 1; i >= 0; i--)
        {
            Destroy( listCapsuleColliders[i] );
            points.RemoveAt(i);
        }
        listCapsuleColliders.Clear();
        this.transform.position = Vector3.zero;////bunu yazdımmmm
        ResetCylinder();
    }

    public void ResetCylinder()
    {
        this.transform.GetChild(0).position = Vector3.zero;
        this.transform.GetChild(1).position = Vector3.zero;
    }

    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }


    /// <summary>
    /// buraları biz yazdık arrayın uçları falan filan

    public Vector2 GetFirstPoint()
    {
        return (Vector2)lineRenderer.GetPosition(0);
    }

    /// </summary>
    /// <returns></returns>
    /// 
    public void UsePhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        capsuleColliderRadius = width / 2f;
    }

}
