using UnityEngine;
using System.Collections;

public class ParticleSystemFactory : Singleton<ParticleSystemFactory> {

    [SerializeField]
    private GameObject targetParticleSystem;

    public CustomParticleSystem GetTargetParticleSystem(Vector3 position)
    {
        return GetParticleSystem(targetParticleSystem,position);
    }

    private CustomParticleSystem GetParticleSystem(GameObject prefab, Vector3 position)
    {
        GameObject newSystem = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        newSystem.SetActive(true);
        return newSystem.GetComponent<CustomParticleSystem>();
    }

	
}
