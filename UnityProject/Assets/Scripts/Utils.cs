using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 RandomCircPosition(Vector3 o, float r)
    {
        float t = Random.Range(0.0f, 2 * Mathf.PI);
        float rr = Random.Range(0.0f, r);
        Vector3 position = new Vector3(
            o.x + rr * Mathf.Cos(t),
            o.y,
            o.y + rr * Mathf.Sin(t));

        return position;
    }

    public static Vector3 RandomElPosition(Vector3 o, float rx, float ry)
    {
        float t = Random.Range(0.0f, 2 * Mathf.PI);
        float rrx = Random.Range(0.0f, rx);
        float rry = Random.Range(0.0f, ry);
        Vector3 position = new Vector3(
            o.x + rrx * Mathf.Cos(t),
            o.y,
            o.y + rry * Mathf.Sin(t));

        return position;
    }

    public static float SqrDist2D(Vector3 a, Vector3 b)
    {
        Vector3 diff = a - b;
        return diff.x * diff.x + diff.z * diff.z;
    }
}
