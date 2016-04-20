using UnityEngine;
using System.Collections;

public class CustomSphereCollider : CustomCollider
{
    [SerializeField]
    private bool useAsCircle;
    [SerializeField]
    private float radius;
    public float Radius { get { return radius; } set { radius = value; } }


    public override bool Ray(Vector3 origin, Vector3 to, ref CustomRayHit outHit)
    {
        Vector3 rayDirection = new Vector3((to.x - origin.x), (to.y - origin.y), (to.z - origin.z));
        rayDirection.Normalize();
        CustomPlane  plane;
        Vector3 crossPointOnPlane;
        if (!useAsCircle)
        {
            plane = new CustomPlane(rayDirection, this.Position);
            crossPointOnPlane = plane.GetProjectionOfPoint(origin);
        }
        else
	    {
            plane = new CustomPlane(this.transform.forward, this.Position);
            crossPointOnPlane = plane.GetProjectionOfLine(origin, rayDirection  );
	    }
        
        Vector3 originToCrossPointVector = origin - crossPointOnPlane;
        bool isSphereBesideRay = CheckIfIsBeside(originToCrossPointVector,rayDirection);
        float originToSphere = Vector3.Distance(origin,this.Position);  
        float crossPointOnPlaneOffset = Vector3.Distance(this.Position, crossPointOnPlane);
        if ((originToSphere < Radius) ||
            (crossPointOnPlaneOffset < Radius && Vector3.Distance(crossPointOnPlane, origin) < Vector3.Distance(origin, to) && !isSphereBesideRay))
        {
            float distance = originToCrossPointVector.magnitude;
            if (outHit.Distance <= 0 || (outHit.Distance > 0 && distance < outHit.Distance))
            {
                outHit.IsHit = true;
                outHit.HitPosition = crossPointOnPlane;
                outHit.HitCollider = this;
                outHit.HitObject = this.gameObject;
                outHit.Distance = distance;
            }

            return true;
           
        }

        return false;
    }

    private bool CheckIfIsBeside(Vector3 offset, Vector3 rayDirection)
    {
        bool isSphereBesideRay = true;
        if (!Mathf.Approximately(offset.x,0.0f))
        {
            isSphereBesideRay &= Mathf.Sign(offset.x) == Mathf.Sign(rayDirection.x);
        }
        if (!Mathf.Approximately(offset.y, 0.0f))
        {
            isSphereBesideRay &= Mathf.Sign(offset.y) == Mathf.Sign(rayDirection.y);
        }
        if (!Mathf.Approximately(offset.z, 0.0f))
        {
            isSphereBesideRay &= Mathf.Sign(offset.z) == Mathf.Sign(rayDirection.z);
        }
        return isSphereBesideRay;
    }

    protected override bool CollisionWithPoint(Vector3 point)
    {
        float distance = Mathf.Sqrt((point.x - this.Position.x) * (point.x - this.Position.x) +
                          (point.y - this.Position.y) * (point.y - this.Position.y) +
                          (point.z - this.Position.z) * (point.z - this.Position.z));

        return (distance < this.Radius);
    }

    protected override bool CollisionWithSphere(CustomSphereCollider collider)
    {
        return (Vector3.Distance(transform.position, collider.transform.position) < Radius + collider.Radius);
    }

    protected override bool CollisionWithBox(CustomBoxCollider collider)
    {
        Vector3 closestPoint;
        closestPoint.x = Mathf.Max(collider.MinX, Mathf.Min(this.Position.x, collider.MaxX));
        closestPoint.y = Mathf.Max(collider.MinY, Mathf.Min(this.Position.y, collider.MaxY));
        closestPoint.z = Mathf.Max(collider.MinZ, Mathf.Min(this.Position.z, collider.MaxZ));


        float distance = Mathf.Sqrt((closestPoint.x - this.Position.x) * (closestPoint.x - this.Position.x) +
                                 (closestPoint.y - this.Position.y) * (closestPoint.y - this.Position.y) +
                                 (closestPoint.z - this.Position.z) * (closestPoint.z - this.Position.z));

        return (distance < this.Radius) ;
    }

    protected override void DrawCollider(bool enable)
    {
        if (enable)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }



}
