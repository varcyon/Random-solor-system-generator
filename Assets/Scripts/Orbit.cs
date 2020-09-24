using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
///I got help with the line render and planet orbits from this tutorial
///https://www.youtube.com/watch?v=mQKGRoV_jBc
///
public class Orbit 
{
    public float xAxis;
    public float zAxis;

    public Orbit (float xAxis, float zAxis)
    {
        this.xAxis = xAxis;
        this.zAxis = zAxis;
    }

    public Vector2 Evalute(float t)
    {
        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Sin(angle) * xAxis;
        float z = Mathf.Cos(angle) * zAxis;
        return new Vector2(x, z);
    }
}
