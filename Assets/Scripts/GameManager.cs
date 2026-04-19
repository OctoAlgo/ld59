using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Alien winningAlien;
    public Alien selectedAlien;
    [SerializeField]
    public List<Alien> aliens;
    public int planetCount = 10;

    public UnityEvent OnConsoleEntered; 
    public UnityEvent OnConsoleExited;

    public string selectedHashX;
    public string selectedHashY;

    public Camera playerCamera;


    void Awake()
    { 
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of GameManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateAliens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateAliens()
    {
        aliens = new List<Alien>();
        for(int i = 0; i < planetCount; i++)
        {
            Alien newAlien = new Alien(
                Names.GetRandomFirstName(),
                Names.GetRandomLastName(),
                Names.GetRandomLike(),
                Names.GetRandomDislike(),

                Random.ColorHSV(),
                null //TODO: Assign image
            );

            aliens.Add(newAlien);
        }

    }

    public void EnterConsole()
    {
        FreezePlayerInput();
        ConsoleManager.Instance.Select();
        OnConsoleEntered.Invoke();

        ConsoleManager.Instance.consoleCamera.forceIntoRenderTexture = false;
        ConsoleManager.Instance.consoleCamera.targetTexture = null;

        GameManager.Instance.playerCamera.enabled = false;
    }

    public void ExitConsole()
    {
        UnfreezePlayerInput();
        ConsoleManager.Instance.Deselect();
        OnConsoleExited.Invoke();

        ConsoleManager.Instance.consoleCamera.forceIntoRenderTexture = true;
        ConsoleManager.Instance.consoleCamera.targetTexture = ConsoleManager.Instance.consoleRenderTexture;

        GameManager.Instance.playerCamera.enabled = true;
    }

    public void OpenConsoleView()
    {
        
    }

    public void OpenPlanetView()
    {
        PlanetsManager.Instance.planetCamera.enabled = true;
        PlanetsManager.Instance.planetCamera.targetTexture = null;
        ConsoleManager.Instance.consoleCamera.targetTexture = ConsoleManager.Instance.consoleRenderTexture;

        GameManager.Instance.playerCamera.enabled = false;
    }

    public void FreezePlayerInput()
    {
        //TODO: Freeze player so he cant move or do other things while inputting text.

    }

    public void UnfreezePlayerInput()
    {
        //TODO: Unfreeze player input.
    }


}
