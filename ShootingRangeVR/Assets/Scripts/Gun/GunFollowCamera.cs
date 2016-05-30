using UnityEngine;
using System.Collections;

public class GunFollowCamera : MonoBehaviour {

    [SerializeField]
    private Camera targetCamera;

    Vector3 rotationOffset;
    Rigidbody rigBody;

    void Start()
    {
        rotationOffset = targetCamera.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
        rigBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 targetRotation = targetCamera.transform.rotation.eulerAngles;
        targetRotation.x *= -1;
        targetRotation += rotationOffset;

        rigBody.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), 100 * Time.deltaTime);
    }
}
