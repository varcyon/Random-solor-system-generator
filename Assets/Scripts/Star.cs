using System.Collections.Generic;
using UnityEngine;


public class Star : CelestialBody, ISelectable
{
    [SerializeField] private GameObject star;
    private MeshRenderer MeshRenderer;

    [SerializeField] public StarClass starClass;
    [SerializeField] public int tempature;
    [SerializeField] public Color color;
    [SerializeField] private GameObject planet;
    [SerializeField] private List<GameObject> planets = new List<GameObject>();
    [SerializeField] public GameObject pauseButton;
    [SerializeField] public GameObject resumeButton;




    private List<string> starElements = new List<string>() {
        "Manganese",
        "Chromium",
        "Vanadium",
        "Titanium",
        "Scandium",
        "Calcium",
        "Potassium" ,
        "Argon",
        "Chlorine",
        "Sulfur",
        "Phosphorus",
        "Silicon" ,
        "Aluminum" ,
        "Magnesium",
        "Sodium",
        "Neon",
        "Fluorine",
        "Oxygen",
        "Nitrogen",
        "Carbon" ,
        "Boron" ,
        "Beryllium" ,
        "Lithium"
        };

    private void Awake()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        dataDisplay = GameObject.FindGameObjectWithTag("DataToolTipDisplay");

    }
    private void Start()
    {
        Generate();
    }

    public override void Generate()
    {
        isLoading = false;

        base.Generate();
        celestialType = CelestialType.Star;
        ///reset star
        star.transform.position = Vector3.zero;
        foreach (GameObject gameObject in planets)
        {
            Destroy(gameObject);
        }
        planets.Clear();
        /////end Reset

        //randomly sets total element amount of the object
        elementAmount = Random.Range(100000, 10000000);
        //adds element amounts
        AddElements();
        // randomly sets a Class of star
        starClass = (StarClass)Random.Range(0, 6);
        switch (starClass)
        {
            case StarClass.O:
                color = new Color(0, 0, 128, 1);//navy blue
                tempature = Random.Range(28000, 50000);
                diameter = Random.Range(200, 250);

                break;
            case StarClass.B:
                color = Color.blue;
                tempature = Random.Range(10000, 28000);
                diameter = Random.Range(150, 250);

                break;
            case StarClass.A:
                color = Color.cyan;
                tempature = Random.Range(7500, 10000);
                diameter = Random.Range(150, 200);

                break;
            case StarClass.F:
                color = Color.white;
                tempature = Random.Range(6000, 7500);
                diameter = Random.Range(150, 200);
                break;
            case StarClass.G:
                color = Color.yellow;
                tempature = Random.Range(5000, 6000);
                diameter = Random.Range(150, 200);

                break;
            case StarClass.K:
                color = new Color(255, 165, 0, 1); // Orange
                tempature = Random.Range(3500, 5000);
                diameter = Random.Range(150, 200);
                break;
            case StarClass.M:
                color = Color.red;
                tempature = Random.Range(2500, 3500);
                diameter = Random.Range(150, 200);
                break;
            default:
                break;
        }
        // sets the parameters from the star class
        MeshRenderer.material.color = color;

        star.transform.localScale = new Vector3(diameter, diameter, diameter);
        star.transform.position += new Vector3(0, diameter, 0);
        selectionCircle.transform.localScale = star.transform.localScale;
        sphereCollider.radius = diameter / 2;
        sphereCollider.center = new Vector3(0, diameter, 0);

        //Creates the planets for the star
        GeneratePlanets();
    }
    //adds amount of elements for certain ones based on the total element amount the object has
    private void AddElements()
    {
        foreach (string s in starElements)
        {
            Element e = elements.Find(x => x.eName == s);
            e.amount = Mathf.Round((float)(0.0045) * elementAmount);
        }
        Element he = elements.Find(x => x.eName == "Helium");
        he.amount = Mathf.Round((float)(72.00 / 100.00) * elementAmount);

        Element hy = elements.Find(x => x.eName == "Hydrogen");
        hy.amount = Mathf.Round((float)(72.00 / 100.00) * elementAmount);
    }

    public void GeneratePlanets()
    {// randomly generates the number of planets, orbits and orbit position.
        for (int i = 1; i < Random.Range(4, 11); i++)
        {
            Vector3 dist = new Vector3(diameter * (i + 1), star.transform.position.y, diameter * (i + 1));
            GameObject p = Instantiate(planet, dist, Quaternion.identity);
            p.GetComponent<PlanetaryBody>().designation = designation + "-" + (i + 1).ToString();
            p.GetComponent<OrbitRenderer>().orbit.xAxis = p.transform.position.x;
            p.GetComponent<OrbitRenderer>().orbit.zAxis = p.transform.position.z;
            p.GetComponent<OrbitMotion>().orbitPath.xAxis = p.transform.position.x;
            p.GetComponent<OrbitMotion>().orbitPath.zAxis = p.transform.position.z;
            p.GetComponent<OrbitMotion>().orbitTime += i;
            p.GetComponent<OrbitMotion>().orbitProgress = Random.Range(0f, 1f);
            p.transform.parent = this.transform;

            planets.Add(p);
            //adds planet to selectables for the click and drag selection
            UnitManager.UM.selectables.Add(p);
        }
    }
    //stops the planet orbits
    public void PauseOrbits()
    {
        foreach (GameObject planet in planets)
        {
            planet.GetComponent<OrbitMotion>().orbitActive = false;
        }
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
    }
    //resumes the orbits
    public void ResumeOrbits()
    {
        foreach (GameObject planet in planets)
        {
            planet.GetComponent<OrbitMotion>().orbitActive = true;
            StartCoroutine(planet.GetComponent<OrbitMotion>().OrbitAnimation());
        }
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);

    }
    //saves the current star system into an xml
    public void SaveSystem()
    {
        StarSystem starSystem = new StarSystem();
        starSystem.designation = designation;
        starSystem.diameter = diameter;
        starSystem.celestialType = celestialType;
        starSystem.elementAmount = elementAmount;
        starSystem.starClass = starClass;
        starSystem.tempature = tempature;
        starSystem.color = color;
        foreach (GameObject Planet in planets)
        {
            Planet planet = new Planet()
            {
                designation = Planet.GetComponent<PlanetaryBody>().designation,
                diameter = Planet.GetComponent<PlanetaryBody>().GetDiameter(),
                planetClass = Planet.GetComponent<PlanetaryBody>().planetClass,
                atmosphere = Planet.GetComponent<PlanetaryBody>().atmosphere,
                location = Planet.GetComponent<PlanetaryBody>().location,
                surface = Planet.GetComponent<PlanetaryBody>().surface,
                inhabited = Planet.GetComponent<PlanetaryBody>().inhabited,
                population = Planet.GetComponent<PlanetaryBody>().population,
                orbitProgress = Planet.GetComponent<OrbitMotion>().orbitProgress,
                color = Planet.GetComponent<PlanetaryBody>().color
            };
            starSystem.systemPlanets.Add(planet);
        }

        XMLOp.Serialize(starSystem, "StarSystem.xml");
    }
    //loads the currently saved star system
    public void LoadSystem()
    {
        isLoading = true;
        if (System.IO.File.Exists("StarSystem.xml"))
        {
            StarSystem starSystem = XMLOp.Deserialize<StarSystem>("StarSystem.xml");
            //sets the star
            designation = starSystem.designation;
            diameter = starSystem.diameter;
            celestialType = starSystem.celestialType;
            elementAmount = starSystem.elementAmount;
            starClass = starSystem.starClass;
            tempature = starSystem.tempature;
            color = starSystem.color;

            //resets the star and planets to .
            star.transform.position = Vector3.zero;
            foreach (GameObject gameObject in planets)
            {
                Destroy(gameObject);
            }
            planets.Clear();

            //sets the attributes of the star
            MeshRenderer.material.color = color;

            star.transform.localScale = new Vector3(diameter, diameter, diameter);
            star.transform.position += new Vector3(0, diameter, 0);
            selectionCircle.transform.localScale = star.transform.localScale;
            sphereCollider.radius = diameter / 2;
            sphereCollider.center = new Vector3(0, diameter, 0);
            //adds the amount of elements
            AddElements();

            //recreates the planets and orbits
            int i =0;
            foreach (Planet pl in starSystem.systemPlanets)
            {
                Vector3 dist = new Vector3(diameter * (i + 2), star.transform.position.y, diameter * (i + 2));
                GameObject p = Instantiate(planet, dist, Quaternion.identity);
                p.GetComponent<OrbitRenderer>().orbit.xAxis = p.transform.position.x;
                p.GetComponent<OrbitRenderer>().orbit.zAxis = p.transform.position.z;
                p.GetComponent<OrbitMotion>().orbitPath.xAxis = p.transform.position.x;
                p.GetComponent<OrbitMotion>().orbitPath.zAxis = p.transform.position.z;
                p.GetComponent<OrbitMotion>().orbitTime += i;
                p.GetComponent<OrbitMotion>().orbitProgress = starSystem.systemPlanets[i].orbitProgress;
                p.transform.parent = this.transform;

                planets.Add(p);
                i++;
            }
            //recreates the planets attributes
            foreach (GameObject p in planets)
            {
                p.GetComponent<PlanetaryBody>().designation = starSystem.systemPlanets[planets.IndexOf(p)].designation;
                p.GetComponent<PlanetaryBody>().diameter = starSystem.systemPlanets[planets.IndexOf(p)].diameter;
                p.GetComponent<PlanetaryBody>().planetClass = starSystem.systemPlanets[planets.IndexOf(p)].planetClass;
                p.GetComponent<PlanetaryBody>().atmosphere = starSystem.systemPlanets[planets.IndexOf(p)].atmosphere;
                p.GetComponent<PlanetaryBody>().location = starSystem.systemPlanets[planets.IndexOf(p)].location;
                p.GetComponent<PlanetaryBody>().surface = starSystem.systemPlanets[planets.IndexOf(p)].surface;
                p.GetComponent<PlanetaryBody>().inhabited = starSystem.systemPlanets[planets.IndexOf(p)].inhabited;
                p.GetComponent<PlanetaryBody>().population = starSystem.systemPlanets[planets.IndexOf(p)].population;
                p.GetComponent<PlanetaryBody>().color = starSystem.systemPlanets[planets.IndexOf(p)].color;
                //sets the planet back up
                p.GetComponent<PlanetaryBody>().SetPlanet();
            }

        }
    }
}

