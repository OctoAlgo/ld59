using System.Collections;
using UnityEngine;

public class HackPanelScript : MonoBehaviour
{
    public GameObject WireGame;
    public GameObject UnscrambleGame;
    public GameObject LoadScreen;

    public int stage = 0;

    public void StartGame()
    {
        WireGame.SetActive(true);
        GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().LoadGame();
    }

    public void StatusUpdate()
    {
        if (stage == 1)
        {
            UnscrambleGame.SetActive(true);
            GameObject.Find("Unscramble Game Manager").GetComponent<UnscrambleGameManager>().LoadGame();
        }
        else if (stage == 2)
        {
            LoadScreen.SetActive(true);
            LoadScreen.GetComponent<ConnectionSliderScript>().Reset();
        }
        else if (stage == 3)
        {
            StartCoroutine(seconddelay());
        }
    }

    IEnumerator seconddelay()
    {
        yield return new WaitForSeconds(1f);
        Unload();
    }

    public void Unload()
    {
        WireGame.SetActive(false);
        UnscrambleGame.SetActive(false);
        LoadScreen.GetComponent<ConnectionSliderScript>().Reset();
        LoadScreen.SetActive(false);
        stage = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
