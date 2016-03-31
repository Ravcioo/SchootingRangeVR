using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CollisionManager : MonoBehaviour
{

    private static List<CustomCollider> colliders;
    public static CollisionManager Instance = null;                                     

   
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }



    public void AddCollider(CustomCollider collider)
    {
        if (colliders == null)
        {
            colliders = new List<CustomCollider>();
        }
        colliders.Add(collider);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < colliders.Count - 1; i++)
        {
            for (int j = i + 1; j < colliders.Count; j++)
            {
                colliders[i].CheckCollision(colliders[j]);
            }
        }
    }



}
    