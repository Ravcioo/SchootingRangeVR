using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunSwitcher : MonoBehaviour
{

    [SerializeField]
    private List<GunSwitchCollider> guns;
    [SerializeField]
    private GameObject mainCamera;

    private GunSwitchCollider equipedGun = null;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CustomRay customRay = new CustomRay(mainCamera.transform.position, mainCamera.transform.forward);
            CustomRayHit customRayHit;

            if (CollisionManager.Instance.Raycast(customRay, out customRayHit))
            {
                foreach (var item in guns)
                {
                    if (item.collider == customRayHit.HitCollider)
                    {
                        SwichGunTo(item);
                    }
                }

            }
        }
    }

    private void SwichGunTo(GunSwitchCollider item)
    {
        if (equipedGun != null)
        {
            equipedGun.gunObject.PutDown();
            equipedGun.collider.gameObject.SetActive(true);

        }
        item.gunObject.Equip();
        item.collider.gameObject.SetActive(false);
        equipedGun = item;
    }

    [System.Serializable]
    private class GunSwitchCollider
    {
        public Gun gunObject;
        public CustomCollider collider;
    }
}
