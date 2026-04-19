using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{

public static ConsoleManager Instance { get; private set; }

public Camera consoleCamera;

public RenderTexture consoleRenderTexture;

public bool toggleSelected = false;
private bool isSelected = false;
private bool blockInput = false;
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
                inputField.text = "";
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

    void ParseCommand (string cmd)
    {
        string[] parts = cmd.Split(' ');
        string command = parts[0].ToLower();
        string[] args = parts.Skip(1).ToArray();

        consoleOutput.text += Environment.NewLine + "> " + cmd;

        switch(command)
        {
            case "help":
                consoleOutput.text += Environment.NewLine + "Available commands: help, clear, planets, date";
                Select();
                break;
            case "clear":
                consoleOutput.text = "";
                Select();
                break;
                
            case "planets":
            StartCoroutine(PlanetCommand());
            break;

            case "date":
            bool isPointedAtPlanet = false; //TODO: Replace with GameManager Satelite Dish check

                if(isPointedAtPlanet)
                {
                    //TODO: Open Dating Screen with GameManager.instance.selectedAlien.
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

    System.Collections.IEnumerator PlanetCommand()
    {
        blockInput = true;

        consoleOutput.text += Environment.NewLine + "[LOG] Fetching database of singles in your observable universe";
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
}
