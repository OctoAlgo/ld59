using System;
using System.Linq;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{

public static ConsoleManager Instance { get; private set; }

public bool toggleSelected = false;
private bool isSelected = false;
public TMPro.TMP_InputField inputField;
public TMPro.TMP_Text consoleOutput;

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
            ToggleSelected();
        }

        if(isSelected)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                consoleOutput.text += Environment.NewLine + "> " + inputField.text;
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

        switch(command)
        {
            case "help":
                consoleOutput.text += Environment.NewLine + "Available commands: help, clear";
                break;
            case "clear":
                consoleOutput.text = "";
                break;
            default:
                consoleOutput.text += Environment.NewLine + "Unknown command: " + command;
                break;
        }
    }
}
