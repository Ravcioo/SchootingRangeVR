using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour {

    [SerializeField]
    private float muzzleflashTime = 0.05f;

    [Header("References")]
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject viewFinder;


    private GameObject muzzleFlash;
    private AudioSource audioSource;
    private bool isGunEquip = false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CustomRay customRay = new CustomRay(mainCamera.transform.position, mainCamera.transform.forward);
        CustomRayHit customRayHit;

        if (CollisionManager.Instance.Raycast(customRay, out customRayHit))
        {
            viewFinder.transform.position = customRayHit.HitPosition - mainCamera.transform.forward.normalized;


            if (Input.GetButtonDown("Fire1"))
            {
                ShotAudio();
                ShowMuzzleFlash(muzzleflashTime);

                TargetColliderInfo targetColliderInfo = customRayHit.HitObject.GetComponent<TargetColliderInfo>();
                if (targetColliderInfo != null)
                {
                    Debug.Log(targetColliderInfo.name);
                    targetColliderInfo.OnHit();
                }
            }
        }

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 1000, Color.red, 5);
    } 


    private void ShowMuzzleFlash(float time)
    {
        StartCoroutine(ShowMuzzleFlashCoroutine(time));
    }

    private IEnumerator ShowMuzzleFlashCoroutine(float time)
    {
        if (muzzleFlash!=null)
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(time);
            muzzleFlash.SetActive(false);
        }
        else
        {
            yield return null;
        }

    }

    public void ShotAudio()
    {
        if (isGunEquip)
        {
            audioSource.Play();
        }
    }
    public void EquipGun(GameObject muzzle)
    {
        muzzleFlash = muzzle;
        isGunEquip = true;
    }

    public void PutDownGun()
    {
        muzzleFlash = null;
        isGunEquip = false;
    }
	
}
