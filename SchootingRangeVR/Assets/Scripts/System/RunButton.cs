using UnityEngine;
using System.Collections;

public class RunButton : MonoBehaviour,RaycastHitable {

    public void OnRaycastHit()
    {
        ShootingRangeSystem.Instance.RunButton();
    }
}
