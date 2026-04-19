using System;
using UnityEngine;

public class ConsoleSit : MonoBehaviour
{

    public bool isSitting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnConsoleEntered.AddListener(OnConsoleEntered);
        GameManager.Instance.OnConsoleExited.AddListener(OnConsoleExited);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && !isSitting)
        {
            SitDown();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isSitting)
        {
            StandUp();
        }
    }

    void SitDown()
    {
        isSitting = true;
        GameManager.Instance.EnterConsole();
    }

    void StandUp()
    {
        isSitting = false;
        GameManager.Instance.ExitConsole();

    }


    private void OnConsoleExited()
    {
        //throw new NotImplementedException();
    }

    private void OnConsoleEntered()
    {
        //throw new NotImplementedException();
    }


}
