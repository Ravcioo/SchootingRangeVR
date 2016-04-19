using UnityEngine;
using System.Collections;

public class CustomRayHit {

    public bool IsHit { get; set; }
    public Vector3 HitPosition { get; set; }
    public GameObject HitObject{ get; set; }
    public CustomCollider HitCollider { get; set; }
    public float Distance { get; set; }
    public CustomRayHit()
    {
        IsHit = false;
        Distance = -1;
    }
}
