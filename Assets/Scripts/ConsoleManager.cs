using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ConsoleManager : MonoBehaviour
{

public static ConsoleManager Instance { get; private set; }

public Camera consoleCamera;

public RenderTexture consoleRenderTexture;

public bool toggleSelected = false;
internal bool isSelected = false;
internal bool blockInput = false;
public TMPro.TMP_InputField inputField;
public TMPro.TMP_Text consoleOutput;


int selectedSolarSystem;

    void Awake()    
    { 
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of ConsoleManager detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Placeholder. This should be called when opening the PC.
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Select();
        }

        if(isSelected)
        {
            if(Input.GetKeyDown(KeyCode.Return) && !blockInput)
            {
                ParseCommand(inputField.text);
                inputField.text = "<mspace=20px>";
            }
        }
    }

    public void Select()
    {
        isSelected = true;
        inputField.ActivateInputField();
        inputField.Select();
    }

    public void Deselect()
    {
        isSelected = false;
        inputField.DeactivateInputField();
    }

    void ToggleSelected()
    {
        isSelected = !isSelected;

        if(isSelected)
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }


    // consoleOutput.text += Environment.NewLine + "";
    // For copying


    void ParseCommand (string cmd)
    {
        string[] parts = cmd.Split(' ');
        string command = parts[0].ToLower();
        string[] args = parts.Skip(1).ToArray();

        consoleOutput.text += Environment.NewLine + "> " + cmd;

        switch(command)
        {
            case "help":
                consoleOutput.text += Environment.NewLine + "Available commands: help, clear, systems, planets, date";
                Select();
                break;
            case "clear":
                consoleOutput.text = "";
                Select();
                break;
                
            case "systems":
            StartCoroutine(ListSystems());
            Select();
            break;

            case "planets":
            HandlePlanetCommand(args);
            //StartCoroutine(PlanetCommand());
            break;

            case "date":
                //TODO: Replace with GameManager Satelite Dish check

                Alien tmpAlien = GameManager.Instance.GetAlienByHash(GameManager.Instance.selectedHashX,GameManager.Instance.selectedHashY); 

                    if(tmpAlien != null)
                    {
                        StartCoroutine(DateEstablished(tmpAlien));
                        // Do shit before, console fancies etc.
                    }
                    else
                    {
                        StartCoroutine(FailedDate());
                    }
                    break;

                default:
                    consoleOutput.text += Environment.NewLine + "Unknown command: '" + command + "'. Type 'help' for a list of commands.";
                    Select();
                    break;
        }
    }


    private IEnumerator ListSystems()
    {
        var systems = GameManager.Instance.systems;
        consoleOutput.text += Environment.NewLine + "[LOG] Scanning for systems with hot singles in your observable universe";
        yield return new WaitForSeconds(.1f); // Simulate delay
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.5f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.8f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(2f);
        
        consoleOutput.text += Environment.NewLine + $"[LOG] Found and parsed {systems.Count} systems:";
        for (int i = 0; i < systems.Count; i++)
        {
            consoleOutput.text += Environment.NewLine + $"  [{i + 1}] {systems[i].name}";
            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(.6f);
        consoleOutput.text += Environment.NewLine + $"[LOG] Use 'planets <systemID>' for more info about the planets.";
    }

    private void HandlePlanetCommand(string[] args)
    {
        if(args.Length == 0)
        {
            consoleOutput.text += Environment.NewLine + "[ERR] No system specified. Usage: planets <system>";
            Select();
            return;
        }

        if(!int.TryParse(args[0], out int hit))
        {
            consoleOutput.text += Environment.NewLine + $"[ERR] {hit} is not a system index (number). Usage: planets <system>";
            Select();
            return;
        }

        var systems = GameManager.Instance.systems;
        if(hit < 1 || hit > systems.Count)
        {
            consoleOutput.text += Environment.NewLine + $"[ERR] No system at index {hit}. Please try again. Usage: planets <system>";
            Select();
            return;
        }

        PlanetsManager.Instance.currentSystem = systems[hit - 1];
        StartCoroutine(PlanetCommand());
    }

    System.Collections.IEnumerator PlanetCommand()
    {
        blockInput = true;

        consoleOutput.text += Environment.NewLine + $"[LOG] Fetching database of singles on System '{PlanetsManager.Instance.currentSystem.name}'";
        yield return new WaitForSeconds(.1f); // Simulate delay
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.5f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.8f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(2f);
        consoleOutput.text += Environment.NewLine + "[LOG] Connection established. Launching Planet View...";
        yield return new WaitForSeconds(1f);
        //consoleOutput.text += Environment.NewLine + "> Lol. Not implemented yet. Get fucked.";
        //TODO Planet View

        SolarSystem currentSystem = PlanetsManager.Instance.currentSystem;

        // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        PlanetsManager.Instance.PopulatePlanets();


        GameManager.Instance.OpenPlanetView();
        ConsoleManager.Instance.Deselect();

        blockInput = false;
        Select();
    }

    System.Collections.IEnumerator FailedDate()
    {
        blockInput = true;

        consoleOutput.text += Environment.NewLine + $"[LOG] Establishing signal at hash {GameManager.Instance.selectedHashX}:{GameManager.Instance.selectedHashY}";
        yield return new WaitForSeconds(.2f); // Simulate delay
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.2f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.2f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(2f);
        consoleOutput.text += Environment.NewLine + $"[ERR] Connection failed. No signals caught at hash {GameManager.Instance.selectedHashX}:{GameManager.Instance.selectedHashY}";
        yield return new WaitForSeconds(1f);
        consoleOutput.text += Environment.NewLine + "[LOG] Check the satellite dish alignment and try again.";

        blockInput = false;
        Select();
    }

    public IEnumerator SelectPlanet(Alien alien)
    {
        PlanetsManager.Instance.planetCamera.enabled = false;
        ConsoleManager.Instance.consoleCamera.enabled = true;
        GameManager.Instance.playerCamera.enabled = false;
        ConsoleManager.Instance.consoleCamera.targetTexture = null;

        consoleOutput.text += Environment.NewLine + $"[LOG] Intercepted SIGNAL at hash '{alien.hashX}:{alien.hashY}'";
        consoleOutput.text += Environment.NewLine + $"[LOG] NAME {alien.GetFullName()}";
        yield return new WaitForSeconds(.3f); // Simulate delay
        consoleOutput.text += Environment.NewLine + $"[LOG] LIKES: {alien.likes}";
        yield return new WaitForSeconds(.3f); // Simulate delay
        consoleOutput.text += Environment.NewLine + $"[LOG] DISLIKES: {alien.dislikes}";
        yield return new WaitForSeconds(.3f); // Simulate delay
        consoleOutput.text += Environment.NewLine + $"[LOG] If you wish to interact with this specimen, align your satellite dish to the corresponding hash and type 'date'.";
    }
    private IEnumerator DateEstablished(Alien alien)
    {

        consoleOutput.text += Environment.NewLine + $"[LOG] Pinging '{alien.hashX}:{alien.hashY}'";
        yield return new WaitForSeconds(.2f); // Simulate delay
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.2f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.2f);
        consoleOutput.text += ".";
        yield return new WaitForSeconds(.3f); // Simulate delay
        if(alien.signalCloudy)
        {
            consoleOutput.text += Environment.NewLine + $"[LOG] Connection refused by host '{alien.firstName}'.";
            consoleOutput.text += Environment.NewLine + $"[LOG] It seem they are busy, or not in the mood. Try again later.";
            yield return new WaitForSeconds(.6f); // Simulate delay
        }
        else
        {
            consoleOutput.text += Environment.NewLine + $"[LOG] Established a connection with host '{alien.firstName}'.";
            consoleOutput.text += Environment.NewLine + $"[LOG] Opening environment 'DATE.unity'";
            yield return new WaitForSeconds(.6f); // Simulate delay

            PlanetsManager.Instance.planetCamera.enabled = false;
            ConsoleManager.Instance.consoleCamera.enabled = true;
            GameManager.Instance.playerCamera.enabled = true;
            ConsoleManager.Instance.consoleCamera.targetTexture = ConsoleManager.Instance.consoleRenderTexture;
            GameManager.Instance.UnfreezePlayerInput();

            DatingManager.Instance.Init(Names.GetRandomQuestion(), alien);
            DatingManager.Instance.Show();
        }
    }
}
