using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;

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

            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 1000, Color.red, 5);
        }

        
    }
	
}
