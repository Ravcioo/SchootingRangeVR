using UnityEngine;
using System.Collections;

public class TargetColliderInfo : MonoBehaviour,RaycastHitable {

    [SerializeField]
    private int points = 1;

    private Target target;

    void Awake()
    {
        target = GetComponentInParent<Target>();
    }

    public void OnRaycastHit(CustomRayHit rayHit)
    {
        if(target.isActiveTarget)
        {
            ShootingRangeSystem.Instance.AddPoints(points);
            GetComponent<AudioSource>().Play();
            target.OnHit();
            ParticleSystemFactory.Instance.GetTargetParticleSystem(rayHit.HitPosition).Run(true);
        }
        
    }



}
