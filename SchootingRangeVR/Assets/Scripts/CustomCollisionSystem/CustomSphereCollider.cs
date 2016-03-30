using UnityEngine;
using System.Collections;

public class CustomSphereCollider : MonoBehaviour
{

    [SerializeField]
    private bool drawCollider = false;
    public float Radius { get { return GetMaxScale(); } set { SetLossyScaleTo(value); } }
    void Awake()
    {
        CollisionManager.Instance.AddCollider(this);
    }

    private float GetMaxScale()
    {
        return Mathf.Max(transform.lossyScale.x / 2, transform.lossyScale.y / 2, transform.lossyScale.z / 2);
    }
    private void SetLossyScaleTo(float value)
    {
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(value*2, value*2, value*2);
        transform.parent = parent;
    }

    public void CheckCollision(CustomSphereCollider collider)
    {

        if (Vector3.Distance(transform.position, collider.transform.position) < Radius + collider.Radius)
        {
            Debug.Log("Collision");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (drawCollider)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, GetMaxScale());
        }
    }



}
