using UnityEngine;


public class PlanetaryBody : CelestialBody, ISelectable
{


    [SerializeField] public GameObject planetaryObj;
    [SerializeField] public ClassType planetClass;
    [SerializeField] public Atmosphere atmosphere;
    [SerializeField] public PlanetaryLocation location;
    [SerializeField] public Surface surface;
    [SerializeField] public bool inhabited;
    [SerializeField] public int population;
    public MeshRenderer MeshRenderer;
    [SerializeField] public Color color;

    private void Awake()
    {
    }

    private void Start()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();

        Generate();

    }

    public override void Generate()
    {
        dataDisplay = GameObject.FindGameObjectWithTag("DataToolTipDisplay");

        base.Generate();
        celestialType = CelestialType.Planet;
        //checks to see if we are loading save data or generating it
        if (!isLoading)
        {
            planetClass = (ClassType)Random.Range(0, 9);
            switch (planetClass)
            {
                /*  case ClassType.D:
                      atmosphere = Atmosphere.Negligiblel;
                      diameter = Random.Range(10,100);
                      location = PlanetaryLocation.AllNonInterstellar;
                      surface = Surface.Barren;
                      color = Color.gray;
                      break;
                      */
                case ClassType.H:
                    atmosphere = Atmosphere.HeavyGasesMetalVapors;
                    diameter = Random.Range(10, 20);
                    location = PlanetaryLocation.AllNonInterstellar;
                    surface = Surface.HotAridLittleWater;
                    color = Color.magenta;
                    break;
                case ClassType.J:
                    atmosphere = Atmosphere.FlurineMethaneAmmonia;
                    diameter = Random.Range(50, 60);
                    location = PlanetaryLocation.ColdAndHotZone;
                    surface = Surface.Tenuous;
                    color = Color.cyan;
                    break;
                case ClassType.K:
                    atmosphere = Atmosphere.CarbonDioxideSulfides;
                    diameter = Random.Range(15, 25);
                    location = PlanetaryLocation.Ecosphere;
                    surface = Surface.Barren;
                    color = Color.red;
                    break;
                case ClassType.L:
                    atmosphere = Atmosphere.OxygenArgonCarbonDioxide;
                    diameter = Random.Range(20, 25);
                    location = PlanetaryLocation.Ecosphere;
                    surface = Surface.RockyBarren;
                    color = Color.red;
                    break;
                case ClassType.M:
                    atmosphere = Atmosphere.NitrogenOxygen;
                    diameter = Random.Range(15, 20);
                    location = PlanetaryLocation.Ecosphere;
                    surface = Surface.WaterAbundantLessThan80;
                    color = Color.blue;
                    break;
                case ClassType.N:
                    atmosphere = Atmosphere.CarbonDioxideSulfides;
                    diameter = Random.Range(15, 20);
                    location = PlanetaryLocation.Ecosphere;
                    surface = Surface.HighTemp;
                    color = Color.green;
                    break;
                case ClassType.R:
                    atmosphere = Atmosphere.PrimarilyVolcanicOutgassing;
                    diameter = Random.Range(10, 25);
                    location = PlanetaryLocation.Interstellar;
                    surface = Surface.TemperateTemp;
                    color = Color.yellow;
                    break;
                case ClassType.T:
                    atmosphere = Atmosphere.Hydrogen;
                    diameter = Random.Range(50, 70);
                    location = PlanetaryLocation.ColdZone;
                    surface = Surface.GaseousHydrogen;
                    color = Color.blue;
                    break;
                case ClassType.Y:
                    atmosphere = Atmosphere.ToxicChemicalsThermionicRadiation;
                    diameter = Random.Range(15, 20);
                    location = PlanetaryLocation.AllNonInterstellar;
                    surface = Surface.Extream500kPlus;
                    color = new Color(255, 165, 0, 1); // Orange
                    break;
                default:
                    break;
            }
        }
        //sets the planet
        SetPlanet();
    }

    public void SetPlanet()
    {
        MeshRenderer.material.color = color;

        planetaryObj.transform.localScale = new Vector3(diameter, diameter, diameter);
        selectionCircle.transform.localScale = planetaryObj.transform.localScale;
        sphereCollider.radius = diameter / 2;
    }
}