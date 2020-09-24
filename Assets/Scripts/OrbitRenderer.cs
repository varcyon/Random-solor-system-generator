using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]


///I got help with the line render and planet orbits from this tutorial
///https://www.youtube.com/watch?v=mQKGRoV_jBc
///
public class OrbitRenderer : MonoBehaviour
{   
    [Range(5,50)]
    [SerializeField] private int segments = 50;
    [SerializeField] public float lineWidth = 20;
    public Orbit orbit;
    LineRenderer orbitLine;
    private void Awake()
    {
        orbitLine = GetComponent<LineRenderer>();
    }

    private void setupOrbit()
    {
        orbitLine.widthMultiplier = lineWidth;
        orbitLine.positionCount = segments;
        Vector3[] points = new Vector3[segments];
        for (int i = 0; i < orbitLine.positionCount; i++)
        {
            Vector2 position = orbit.Evalute((float)i / (float)segments);
            points[i] = new Vector3(position.x,0f,position.y);
        }
        orbitLine.SetPositions(points);

    }

    private void Update()
    {
        setupOrbit();
    }

}
