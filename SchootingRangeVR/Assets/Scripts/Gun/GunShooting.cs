using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour {

    [SerializeField]
    private float muzzleflashTime = 0.05f;

    [Header("References")]
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject muzzleFlash;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            audioSource.Play();
            ShowMuzzleFlash(muzzleflashTime);

            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,Mathf.Infinity))
            {
              
                TargetColliderInfo targetColliderInfo = hit.transform.GetComponent<TargetColliderInfo>();

                if(targetColliderInfo != null)
                {
                    targetColliderInfo.OnHit();
                }
            }

            //Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 1000, Color.red, 5);

        }     
    }

    private void ShowMuzzleFlash(float time)
    {
        StartCoroutine(ShowMuzzleFlashCoroutine(time));
    }

    private IEnumerator ShowMuzzleFlashCoroutine(float time)
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(time);
        muzzleFlash.SetActive(false);
    }
	
}
