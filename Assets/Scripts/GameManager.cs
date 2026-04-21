using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace MixedSignals
{
    
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Alien selectedAlien;
    public Alien trueLove;
    public int minPlanets;
    public int maxPlanetsExclusive;

    [SerializeField]

    public List<SolarSystem> systems;
    public List<AlienImagePair> imagePairs;
    public AlienImagePair legallyDistinctDoorManImagePair;
    public int systemCount = 5;

    public UnityEvent OnConsoleEntered; 
    public UnityEvent OnConsoleExited;

    public string selectedHashX;
    public string selectedHashY;
    public bool playerFrozen;

    public Camera playerCamera;
    public SatelliteMove sat;
    public TextMeshProUGUI infoText;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public bool cursorLocked;
    internal bool crackedAndSpun;
    public string lastHash;

        void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple GameManagers. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("Console", LoadSceneMode.Additive);
        SceneManager.LoadScene("PlanetSelection", LoadSceneMode.Additive);
        SceneManager.LoadScene("SatelliteConsole", LoadSceneMode.Additive);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastHash = "0000:0000";
        cursorLocked = true;
        systems = GalaxyGenerator.GenerateGalaxy(systemCount, minPlanets, maxPlanetsExclusive);
        DebugLogGalaxy();
    }

    // Update is called once per frame
    void Update()
    {
        if(cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
    }

    void DebugLogGalaxy()
{
    foreach (var sys in systems)
    {
        Debug.Log($"System: {sys.name} ({sys.planets.Count} planets)");
        foreach (var p in sys.planets)
        {
            var a = p.loveInterest;
            Debug.Log($"  {p.name} — {a.GetFullName()} likes {a.likes}, hates {a.dislikes}");
        }
    }
}

    public void EnterConsole(ConsoleManager consoleManager)
    {
        FreezePlayerInput();

        Instance.playerCamera.enabled = false;
        SatelliteConsoleManager.Instance.consoleCamera.enabled = false;
        ConsoleManager.Instance.consoleCamera.enabled = false;
        PlanetsManager.Instance.planetCamera.enabled = false;

        consoleManager.Select();
        consoleManager.blockInput = false;
        OnConsoleEntered.Invoke();

        consoleManager.consoleCamera.forceIntoRenderTexture = false;
        consoleManager.consoleCamera.targetTexture = null;
        consoleManager.consoleCamera.enabled = true;
    }

    public void ExitConsole(ConsoleManager consoleManager)
    {
        UnfreezePlayerInput();

        Instance.playerCamera.enabled = false;
        SatelliteConsoleManager.Instance.consoleCamera.enabled = false;
        ConsoleManager.Instance.consoleCamera.enabled = false;
        PlanetsManager.Instance.planetCamera.enabled = false;

        consoleManager.Deselect();
        consoleManager.blockInput = true;
        OnConsoleExited.Invoke();

        consoleManager.consoleCamera.forceIntoRenderTexture = true;
        consoleManager.consoleCamera.targetTexture = consoleManager.consoleRenderTexture;
        consoleManager.consoleCamera.enabled = false;
        Instance.playerCamera.enabled = true;
    }

    public void OpenConsoleView()
    {
        
    }

    public void OpenPlanetView()
    {
        PlanetsManager.Instance.planetCamera.enabled = true;
        PlanetsManager.Instance.planetCamera.targetTexture = null;
        ConsoleManager.Instance.consoleCamera.targetTexture = ConsoleManager.Instance.consoleRenderTexture;
        SatelliteConsoleManager.Instance.consoleCamera.targetTexture = SatelliteConsoleManager.Instance.consoleRenderTexture;

        Instance.playerCamera.enabled = false;
    }

    public void FreezePlayerInput()
    {
        playerFrozen = true;
        cursorLocked = false;
        //TODO: Freeze player so he cant move or do other things while inputting text.

    }

    public void UnfreezePlayerInput()
    {
        playerFrozen = false;
        cursorLocked = true;
        //TODO: Unfreeze player input.
    }

    public static AlienImagePair GetRandomImagePair(List<AlienImagePair> pairs)
    {
        return pairs[Random.Range(0, pairs.Count)];
    }

    public Alien GetAlienByHash(string hashX, string hashY)
    {
        return AllAliens.FirstOrDefault(a => a.hashX == hashX && a.hashY == hashY);
    }

    internal void DateEnds(Alien guaranteedCloudy)
    {
        var all = AllAliens.ToList();

        foreach (var a in all) a.signalCloudy = false;

        all.Remove(guaranteedCloudy);

        for (int i = all.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (all[i], all[j]) = (all[j], all[i]);
        }

        int totalHalf = (all.Count + 1) / 2;
        int randomCount = Mathf.Max(0, totalHalf - 1);

        for (int i = 0; i < randomCount; i++)
            all[i].signalCloudy = true;

        if (guaranteedCloudy != null)
            guaranteedCloudy.signalCloudy = true;
    }

    public IEnumerable<Planet> AllPlanets => systems.SelectMany(s => s.planets);

    public IEnumerable<Alien> AllAliens => systems.SelectMany(s => s.planets).Select(p => p.loveInterest).Where(a => a != null);


    // Dunno if we ever need these, but this is cool af
    // EDIT: I needed them :)

}

public static class GalaxyGenerator
{

    public static Alien GenerateAlien() 
    {

        var values = Enum.GetValues(typeof(Alien.AlienType));
        Alien.AlienType randomType = (Alien.AlienType)values.GetValue(Random.Range(0, values.Length)); // 5 = number of enum values
        Alien tmp;

        if(randomType == Alien.AlienType.LEGALLYDISTINCTDOORMAN)
            {
                tmp = new Alien(
                Names.GetRandomFirstName(),
                Names.GetRandomLastName(),
                Names.GetRandomLike(),
                Names.GetRandomDislike(),
                randomType,
                Random.ColorHSV(),
                (GameManager.Instance.legallyDistinctDoorManImagePair)
            );
            }
            else
            {
                tmp = new Alien(
                Names.GetRandomFirstName(),
                Names.GetRandomLastName(),
                Names.GetRandomLike(),
                Names.GetRandomDislike(),
                randomType,
                Random.ColorHSV(),
                GameManager.GetRandomImagePair(GameManager.Instance.imagePairs)
            );
            }
        return tmp;
    }

    public static Planet GeneratePlanet()
    {
        var planet = new Planet(
            Names.GetRandomPlanet(),
            Color.HSVToRGB(Random.value, Random.Range(0.5f, 1f), Random.Range(0.6f, 1f)),
            Random.Range(0.8f, 5f),   // size variation
            Random.Range(5f, 15f)        // orbit speed variation
        );
        planet.loveInterest = GenerateAlien();
        return planet;
    }
    public static SolarSystem GenerateSystem(int minPlanets, int maxPlanetsExclusive)
    {
        var system = new SolarSystem(Names.GetRandomSolarSystem());
        int planetCount = Random.Range(minPlanets, maxPlanetsExclusive); // 2-5 planets?
        for (int i = 0; i < planetCount; i++)
        {
            system.planets.Add(GeneratePlanet());
        }
        return system;
    }

    public static List<SolarSystem> GenerateGalaxy(int systemCount, int minPlanets, int maxPlanetsExclusive)
    {
        var galaxy = new List<SolarSystem>();
        for (int i = 0; i < systemCount; i++)
        {
            galaxy.Add(GenerateSystem(minPlanets, maxPlanetsExclusive));
        }
        return galaxy;
    }
}
}