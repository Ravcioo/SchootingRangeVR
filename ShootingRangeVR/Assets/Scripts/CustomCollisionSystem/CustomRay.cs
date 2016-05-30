using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CustomRay
{
    public Vector3 Origin { get; private set; }
    public Vector3 Direction { get; private set; }

    public CustomRay(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }
    
}
