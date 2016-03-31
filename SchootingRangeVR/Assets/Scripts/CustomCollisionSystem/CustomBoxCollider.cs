using UnityEngine;
using System.Collections;

public class CustomBoxCollider : CustomCollider
{

    [SerializeField]
    private Vector3 size;

    public Vector3 Size { get { return size; } set { size = value; } }
    public float MaxX { get { return transform.position.x + Size.x / 2; } private set { ;} }
    public float MaxY { get { return transform.position.y + Size.y / 2; } private set { ;} }
    public float MaxZ { get { return transform.position.z + Size.z / 2; } private set { ;} }

    public float MinX { get { return transform.position.x - Size.x / 2; } private set { ;} }
    public float MinY { get { return transform.position.y - Size.y / 2; } private set { ;} }
    public float MinZ { get { return transform.position.z - Size.z / 2; } private set { ;} }


    protected override void CollisionWithSphere(CustomSphereCollider collider)
    {
        Vector3 closestPoint;
        closestPoint.x = Mathf.Max(this.MinX, Mathf.Min(collider.Position.x, this.MaxX));
        closestPoint.y = Mathf.Max(this.MinY, Mathf.Min(collider.Position.y, this.MaxY));
        closestPoint.z = Mathf.Max(this.MinZ, Mathf.Min(collider.Position.z, this.MaxZ));


        float distance = Mathf.Sqrt((closestPoint.x - collider.Position.x) * (closestPoint.x - collider.Position.x) +
                                 (closestPoint.y - collider.Position.y) * (closestPoint.y - collider.Position.y) +
                                 (closestPoint.z - collider.Position.z) * (closestPoint.z - collider.Position.z));

        if (distance < collider.Radius)
        {
            Debug.Log("Collision");
        }
    }

    protected override void CollisionWithBox(CustomBoxCollider collider)
    {
        bool collisionX = this.MaxX >= collider.MinX && this.MinX <= collider.MaxX;
        bool collisionY = this.MaxY >= collider.MinY && this.MinY <= collider.MaxY;
        bool collisionZ = this.MaxZ >= collider.MinZ && this.MinZ <= collider.MaxZ;
        if (collisionX && collisionY && collisionZ)
        {
            Debug.Log("collision");
        }
    }

    protected override void DrawCollider(bool enable)
    {
        if (enable)
        {
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
