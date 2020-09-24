using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum PlanetaryLocation
{
    EcoSphereColdZone,
    HotZone,
    AllNonInterstellar,
    Ecosphere,
    ColdZone,
    ColdAndHotZone,
    Interstellar
}
public enum Atmosphere
{
    Negligiblel,
    Hydrogen,
    HeavyGasesMetalVapors,
    OxygenArgonCarbonDioxide,
    NitrogenOxygen,
    CarbonDioxideSulfides,
    FlurineMethaneAmmonia,
    PrimarilyVolcanicOutgassing,
    ToxicChemicalsThermionicRadiation
}

public enum ClassType
{
    H, J, K, L, M, N, R, T, Y//D,
}

public enum Surface
{
    Barren,
    HotAridLittleWater,
    Tenuous,
    RockyBarren,
    WaterAbundantLessThan80,
    HighTemp,
    TemperateTemp,
    GaseousHydrogen,
    Extream500kPlus
}
//class for saving planets
public class Planet 
{
    public string designation;
    public float diameter;
    public ClassType planetClass;
    public Atmosphere atmosphere;
    public PlanetaryLocation location;
    public Surface surface;
    public bool inhabited;
    public int population;
    public float orbitProgress;
    public Color color;
}
