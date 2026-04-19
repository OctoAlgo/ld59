using System;
using Unity.VisualScripting;
using UnityEngine;

public class ConsoleSit : MonoBehaviour
{

    public bool isSitting = false;
    public bool useSatelliteConsole = false;
    ConsoleManager conMan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

                if(useSatelliteConsole )
                {
                    conMan = SatelliteConsoleManager.Instance;
                }
                else
                {
                    conMan = ConsoleManager.Instance;
                }

        GameManager.Instance.OnConsoleEntered.AddListener(OnConsoleEntered);
        GameManager.Instance.OnConsoleExited.AddListener(OnConsoleExited);
    }

    // Update is called once per frame
    void Update()
    {
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

            Debug.Log("Player");
            if(Input.GetKeyDown(KeyCode.E) && !isSitting)
            {
                Debug.Log(useSatelliteConsole + " " + conMan.name);
                SitDown(conMan);
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && isSitting)
            {
                StandUp(conMan);
            }
        }
    }

}
