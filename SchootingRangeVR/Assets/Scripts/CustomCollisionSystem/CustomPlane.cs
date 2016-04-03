using UnityEngine;
using System.Collections;

public class CustomPlane {

    public float A { get; set; }
    public float B { get; set; }
    public float C { get; set; }
    public float D { get; set; }

    public CustomPlane(float a,float b,float c, float d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }
    public CustomPlane(Vector3 abc, float d)
    {
        A = abc.x;
        B = abc.y;
        C = abc.z;
        D = d;
    }

    public CustomPlane(Vector3 normal, Vector3 point)
    {
        normal.Normalize();
        A = normal.x;
        B = normal.y;
        C = normal.z;
        D = (normal.x * point.x + normal.y * point.y + normal.z * point.z) * (-1);
    }

    public Vector3 GetProjectionOfPoint(Vector3 point)
    {
        float t = -(A * point.x + B * point.y + C * point.z + D) / (A * A + B * B + C * C);
        return new Vector3(point.x + A * t, point.y + B * t, point.z + C * t);
    }

    public Vector3 GetProjectionOfLine(Vector3 origin, Vector3 direction)
    {
        float t = -(A * origin.x + B * origin.y + C * origin.z + D) / (A * direction.x + B * direction.y + C * direction.z);
        return new Vector3(origin.x + direction.x * t, origin.y + direction.y * t, origin.z + direction.z * t);
    }

}
