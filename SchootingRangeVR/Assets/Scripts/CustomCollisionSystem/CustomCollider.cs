using UnityEngine;
using System.Collections;

public abstract class CustomCollider : MonoBehaviour {


    [SerializeField]
    private bool drawCollider = false;

    public Vector3 Position { get { return transform.position; } private set{;} }
    void Awake()
    {
        CollisionManager.Instance.AddCollider(this);
    }

    public bool CheckCollision(CustomCollider collider)
    {

        if (collider is CustomBoxCollider)
        {
            return CollisionWithBox(collider as CustomBoxCollider);
        }
        if (collider is CustomSphereCollider)
        {
            return CollisionWithSphere(collider as CustomSphereCollider);
        }
        return false;
    }
    public bool CheckCollision(Vector3 point)
    {
        return CollisionWithPoint(point);
    }

    public abstract bool Ray(Vector3 from, Vector3 to, out CustomRayHit outHit);
    protected abstract bool CollisionWithPoint(Vector3 point);

    protected abstract bool CollisionWithBox(CustomBoxCollider collider);

    protected abstract bool CollisionWithSphere(CustomSphereCollider collider);

    protected abstract void DrawCollider(bool enable);

    void OnDrawGizmosSelected()
    {
        DrawCollider(drawCollider);
    }

}
