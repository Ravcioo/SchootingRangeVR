using UnityEngine;
using System.Collections;

public class CustomSphereCollider : CustomCollider
{

    [SerializeField]
    private float radius;
    public float Radius { get { return radius; } set { radius = value; } }



    protected override void CollisionWithSphere(CustomSphereCollider collider)
    {
        if (Vector3.Distance(transform.position, collider.transform.position) < Radius + collider.Radius)
        {
            Debug.Log("Collision");
        }
    }

    protected override void CollisionWithBox(CustomBoxCollider collider)
    {
        Vector3 closestPoint;
        closestPoint.x = Mathf.Max(collider.MinX, Mathf.Min(this.Position.x, collider.MaxX));
        closestPoint.y = Mathf.Max(collider.MinY, Mathf.Min(this.Position.y, collider.MaxY));
        closestPoint.z = Mathf.Max(collider.MinZ, Mathf.Min(this.Position.z, collider.MaxZ));


        float distance = Mathf.Sqrt((closestPoint.x - this.Position.x) * (closestPoint.x - this.Position.x) +
                                 (closestPoint.y - this.Position.y) * (closestPoint.y - this.Position.y) +
                                 (closestPoint.z - this.Position.z) * (closestPoint.z - this.Position.z));

        if (distance < this.Radius)
        {
            Debug.Log("Collision");
        }
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
