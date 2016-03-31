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

    public void CheckCollision(CustomCollider collider)
    {

        if (collider is CustomBoxCollider)
        {
            CollisionWithBox(collider as CustomBoxCollider);
        }
        if (collider is CustomSphereCollider)
        {
            CollisionWithSphere(collider as CustomSphereCollider);
        }
    }

    protected abstract void CollisionWithBox(CustomBoxCollider collider);

    protected abstract void CollisionWithSphere(CustomSphereCollider collider);

    protected abstract void DrawCollider(bool enable);

    void OnDrawGizmosSelected()
    {
        DrawCollider(drawCollider);
    }

}
