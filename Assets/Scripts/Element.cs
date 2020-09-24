using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum ElementClass
{
    PoorMetal,
    NonMetal,
    Metalloid,
    AlakiMetal,
    AlkalineEarth,
    Transition,
    RareEarth,
    Radioactive,
    Liquid,
    Gas
}
public class Element { 
    public string eName;
    public double amount;
    public ElementClass elementClass;

    public Element()
    {
        eName = "Brian-ite";
        amount = 42;
        elementClass = ElementClass.Gas;
    }

    public Element(ElementClass e, string n, double a )
    {
        elementClass = e;
        eName = n;
        amount = a;
    }

}
