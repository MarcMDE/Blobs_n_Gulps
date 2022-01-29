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
}
