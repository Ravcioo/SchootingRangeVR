using UnityEngine;
using System.Collections;

public class CustomBoxCollider : CustomCollider
{
    [SerializeField]
    private bool useAsPlane;
    [SerializeField]
    private Vector3 size;

    public Vector3 Size { get { return size; } set { size = value; } }
    public float MaxX { get { return transform.position.x + Size.x / 2; } private set { ;} }
    public float MaxY { get { return transform.position.y + Size.y / 2; } private set { ;} }
    public float MaxZ { get { return transform.position.z + Size.z / 2; } private set { ;} }

    public float MinX { get { return transform.position.x - Size.x / 2; } private set { ;} }
    public float MinY { get { return transform.position.y - Size.y / 2; } private set { ;} }
    public float MinZ { get { return transform.position.z - Size.z / 2; } private set { ;} }


    public override bool Ray(Vector3 origin, Vector3 to, ref CustomRayHit outHit)
    {
        Vector3 rayDirection = new Vector3((to.x - origin.x), (to.y - origin.y), (to.z - origin.z));
        rayDirection.Normalize();
        CustomPlane plane = null;
        if (useAsPlane)
        {
            plane = new CustomPlane(this.transform.forward, this.Position);
        }
        else
        {
            Debug.LogError("Boxes are currently can be raycasting only if are using as plane");
        }

        Vector3 crossPointOnPlane = plane.GetProjectionOfLine(origin, rayDirection);
        Vector3 originToCrossPointVector = origin - crossPointOnPlane;
        bool isBoxBesideRay = CheckIfIsBeside(originToCrossPointVector, rayDirection);

        if (!isBoxBesideRay && CollisionWithPoint(crossPointOnPlane))
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

            // Debug.Log("Ray");

            return true;
        }

        return false;
    }

    private bool CheckIfIsBeside(Vector3 offset, Vector3 rayDirection)
    {
        bool isSphereBesideRay = true;
        if (!Mathf.Approximately(offset.x, 0.0f))
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
    protected override bool CollisionWithPoint(Vector3 point) {
        bool intersect = (point.x >= this.MinX && point.x <= this.MaxX) &&
                         (point.y >= this.MinY && point.y <= this.MaxY) &&
                         (point.z >= this.MinZ && point.z <= this.MaxZ);
      
        return (intersect) ;
    }
    protected override bool CollisionWithSphere(CustomSphereCollider collider)
    {
        Vector3 closestPoint;
        closestPoint.x = Mathf.Max(this.MinX, Mathf.Min(collider.Position.x, this.MaxX));
        closestPoint.y = Mathf.Max(this.MinY, Mathf.Min(collider.Position.y, this.MaxY));
        closestPoint.z = Mathf.Max(this.MinZ, Mathf.Min(collider.Position.z, this.MaxZ));


        float distance = Mathf.Sqrt((closestPoint.x - collider.Position.x) * (closestPoint.x - collider.Position.x) +
                                 (closestPoint.y - collider.Position.y) * (closestPoint.y - collider.Position.y) +
                                 (closestPoint.z - collider.Position.z) * (closestPoint.z - collider.Position.z));

        return (distance < collider.Radius) ;
    }

    protected override bool CollisionWithBox(CustomBoxCollider collider)
    {
        bool collisionX = this.MaxX >= collider.MinX && this.MinX <= collider.MaxX;
        bool collisionY = this.MaxY >= collider.MinY && this.MinY <= collider.MaxY;
        bool collisionZ = this.MaxZ >= collider.MinZ && this.MinZ <= collider.MaxZ;
        return (collisionX && collisionY && collisionZ) ;
    }

    protected override void DrawCollider(bool enable)
    {
        if (enable)
        {
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
