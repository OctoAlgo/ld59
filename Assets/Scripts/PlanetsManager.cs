using System;
using UnityEngine;

namespace MixedSignals
{
    
public class PlanetsManager : MonoBehaviour
{
    public static PlanetsManager Instance;
    public GameObject sun;
    public GameObject planetPrefab;
    public Camera planetCamera;
    public GameObject planetsHolder;

    public SolarSystem currentSystem;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of PlanetsManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignalPlanet(Alien alien)
    {
        ConsoleManager.Instance.StartCoroutine(ConsoleManager.Instance.SelectPlanet(alien));
    }

    internal void PopulatePlanets()
    {

        if(planetsHolder.transform.childCount > 0)
        {
            foreach (Transform child in planetsHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        for(int i = 0; i < currentSystem.planets.Count; i++)
        {
            Planet planetData = currentSystem.planets[i];
            float orbitRadius = 4f + i * 2f;
            float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector3 spawnPos = sun.transform.position + new Vector3(Mathf.Cos(angle) * orbitRadius, 0, Mathf.Sin(angle)) * orbitRadius;

            GameObject tmp = Instantiate(planetPrefab, spawnPos, Quaternion.identity, planetsHolder.transform);
            PlanetMovement move = tmp.GetComponent<PlanetMovement>();

            move.planetData = planetData;
            move.Init();
        }
    }
}

}