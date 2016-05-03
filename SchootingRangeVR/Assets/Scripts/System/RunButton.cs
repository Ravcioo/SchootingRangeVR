using UnityEngine;
using System.Collections;

public class RunButton : MonoBehaviour,RaycastHitable {

    public void OnRaycastHit(CustomRayHit rayHit)
    {
        ShootingRangeSystem.Instance.RunButton();
        GetComponent<CustomParticleSystem>().Run();
    }
}
