using UnityEngine;
using System.Collections;

public class TargetColliderInfo : MonoBehaviour {

    [SerializeField]
    private int points = 1;

    public void OnHit()
    {
        ShootingRangeSystem.Instance.AddPoints(points);
        GetComponent<AudioSource>().Play();
    }



}
