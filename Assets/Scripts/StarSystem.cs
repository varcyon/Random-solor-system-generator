using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum CelestialType
{
    Star, Planet
}
public enum StarClass
{
    O, B, A, F, G, K, M
}

//class for saving the star
public class StarSystem 
{
    public string designation;
    public float diameter;
    public  int tempature;
    public float elementAmount;
    public StarClass starClass;
    public CelestialType celestialType;
    public Color color;
    public List<Planet> systemPlanets = new List<Planet>();
}
