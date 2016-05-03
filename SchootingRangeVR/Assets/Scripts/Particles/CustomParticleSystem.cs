using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomParticleSystem : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int particlesQuantity = 10;
    [SerializeField]
    private float explosionPower = 10f;
    [SerializeField]
    private float gravityForce = 10f;
    [SerializeField]
    private float dragForce = 1;
    [SerializeField]
    private float duration = 5f;

    private bool working = false;
    private List<CustomParticle> particles = new List<CustomParticle>();
    private float timer = 0;

    private bool initialised = false;
    private bool destroyAfter = false;

    void FixedUpdate()
    {
        if(working)
        {
            timer += Time.deltaTime;

            if(timer > duration)
            {
                if(destroyAfter)
                {
                    Destroy(gameObject);
                }
                working = false;
                Restart();
            }
            else
            {
                foreach (var particle in particles)
                {
                    particle.obj.transform.Translate(particle.force * Time.deltaTime,Space.World);
                    particle.force = Vector3.Lerp(particle.force, -Vector3.up * gravityForce, Time.deltaTime * dragForce);
                }
            }


        }
    }

    private void Initialise()
    {
        for (int i = 0; i < particlesQuantity; i++)
        {
            GameObject newParticle = GameObject.Instantiate(prefab);
            newParticle.transform.SetParent(transform);
            newParticle.transform.localPosition = Vector3.zero;
            newParticle.transform.localRotation = Quaternion.identity;
            CustomParticle newCustomParticle = new CustomParticle();
            newCustomParticle.obj = newParticle;
            newCustomParticle.force = Vector3.zero;
            particles.Add(newCustomParticle);
        }

        initialised = true;
    }


    public void Run(bool destroyAfter=false)
    {
        this.destroyAfter = destroyAfter;

        if(!initialised)
        {
            Initialise();
        }

        Restart();

        working = true;

        foreach (var particle in particles)
        {
            particle.force = Random.insideUnitSphere * explosionPower;
            particle.obj.SetActive(true);
        }
    }

    private void Restart()
    {
        timer = 0;

        foreach (var particle in particles)
        {
            particle.force = Vector3.zero;
            particle.obj.transform.localPosition = Vector3.zero;
            particle.obj.transform.localRotation = Quaternion.identity;
            particle.obj.SetActive(false);
        }
    }


    private class CustomParticle
    {
        public GameObject obj;
        public Vector3 force;
    }

}
