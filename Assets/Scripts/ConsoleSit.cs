using System;
using Unity.VisualScripting;
using UnityEngine;
namespace MixedSignals
{
    
public class ConsoleSit : MonoBehaviour
{

    public bool isSitting = false;
    public bool playerTouch;
    public bool useSatelliteConsole = false;
    ConsoleManager conMan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
            Init();
                    GameManager.Instance.OnConsoleEntered.AddListener(OnConsoleEntered);
            GameManager.Instance.OnConsoleExited.AddListener(OnConsoleExited);
        }

    void Init()
        {
            
        if(useSatelliteConsole )
        {
            conMan = SatelliteConsoleManager.Instance;
        }
        else
        {
            conMan = ConsoleManager.Instance;
        }
        }

    // Update is called once per frame
    void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && isSitting)
            {
                StandUp(conMan);
            }

            if(playerTouch && !isSitting && Input.GetKeyDown(KeyCode.E))
            {
                Init();
                SitDown(conMan);
            }
        }

    void SitDown(ConsoleManager conMan)
    {
        isSitting = true;
        //ConsoleManager conMan = useSatelliteConsole ? SatelliteConsoleManager.Instance : ConsoleManager.Instance;
        GameManager.Instance.EnterConsole(conMan);
    }
    
    void StandUp(ConsoleManager conMan)
    {
        isSitting = false;
        //ConsoleManager conMan = useSatelliteConsole ? SatelliteConsoleManager.Instance : ConsoleManager.Instance;
        GameManager.Instance.ExitConsole(conMan);
    }


    private void OnConsoleExited()
    {
        //throw new NotImplementedException();
    }

    private void OnConsoleEntered()
    {
        //throw new NotImplementedException();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
            {
                playerTouch = true;
            }
    }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                playerTouch = false;
            }
        }

    }

}