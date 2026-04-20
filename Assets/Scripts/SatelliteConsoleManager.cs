using System;
using System.Collections;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace MixedSignals
{
    
public class SatelliteConsoleManager : ConsoleManager
{
    public static new SatelliteConsoleManager Instance { get; private set; }

    void Awake()    
    { 
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of SatelliteConsoleManager detected. Destroying duplicate.");
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
        if(Input.GetKeyDown(KeyCode.Tab) )
            {
                Select();
            }

        //Debug.Log(blockInput + " " + isSelected);
        if(isSelected)
        {
            if(Input.GetKeyDown(KeyCode.Return) && !blockInput)
            {
                blockInput = true;
                ParseCommand(inputField.text);
                inputField.text = "<mspace=20px>";
                blockInput = false;
            }
        }


    }   

    void ParseCommand(string cmd)
    {
        Debug.Log("Parsing command: " + cmd);
        consoleOutput.text += Environment.NewLine + "> " + cmd;
        
        string[] parts = cmd.Trim().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) { Select(); return; }
        
        string command = parts[0].ToLower();
        
        switch (command)
        {
            case "help":
                consoleOutput.text += Environment.NewLine + "Available commands: help, clear, point";
                Select();
                break;
            case "clear":
                consoleOutput.text = "";
                Select();
                break;
            case "point":
                HandlePoint(parts);
                break;
            default:
                consoleOutput.text += Environment.NewLine + "Unknown command. Type 'help' for a list of commands.";
                Select();
                break;
        }
    }


    void HandlePoint(string[] parts)
    {
        if (parts.Length != 3)
        {
            consoleOutput.text += Environment.NewLine + "Usage: point [hashX] [hashY]";
            Select();
            return;
        }
        
        string hashX = parts[1].ToUpper();
        string hashY = parts[2].ToUpper();
        
        StartCoroutine(PointCommand(hashX, hashY));
    }
    
        IEnumerator PointCommand(string hashX, string hashY)
        {
            GameManager.Instance.crackedAndSpun = false;

            blockInput = true;
            consoleOutput.text += Environment.NewLine + $"[LOG] Preparing to point at '{hashX}:{hashY}'";
            yield return new WaitForSeconds(.4f); // Simulate delay
            consoleOutput.text += ".";
            yield return new WaitForSeconds(.4f); // Simulate delay
            consoleOutput.text += ".";
            yield return new WaitForSeconds(.4f); // Simulate delay
            consoleOutput.text += ".";
            yield return new WaitForSeconds(.6f); // Simulate delay
            consoleOutput.text += Environment.NewLine + $"[ERR] Auto-Crack binaries not found. Manual crack via terminal required.";
            yield return new WaitForSeconds(.2f); // Simulate delay
            consoleOutput.text += Environment.NewLine + $"[LOG] Hash Coordinates reconstructed. Move scheduled. Please perform manual crack.";

            GameManager.Instance.selectedHashX = hashX;
            GameManager.Instance.selectedHashY = hashY;
            
            blockInput = false;
        }
}

}