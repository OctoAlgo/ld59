using MixedSignals;
using UnityEngine;

public class PanelScipt : MonoBehaviour
{
    public GameObject panel;

    void StartPanel()
    {
        panel.SetActive(true);
        panel.GetComponent<HackPanelScript>().StartGame();
    }

    void ClosePanel()
    {
        panel.GetComponent<HackPanelScript>().Unload();
        panel.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        string tempHash = $"{GameManager.Instance.selectedHashX}:{GameManager.Instance.selectedHashY}";
        bool hashSame = GameManager.Instance.lastHash == tempHash;
        

        if(hashSame)
        {
            GameManager.Instance.infoText.gameObject.SetActive(true);
            GameManager.Instance.infoText.text = "You haven't set a new signal to point at.";
        }
        else if(!panel.activeSelf)
        {
            GameManager.Instance.infoText.gameObject.SetActive(true);
            GameManager.Instance.infoText.text = "Press 'E' to interact with the cracking terminal.";
        }
        else
        {
            GameManager.Instance.infoText.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(hashSame)
                {
                    // Why you even cracking ya dumbass
                }
                else
                {
                    GameManager.Instance.FreezePlayerInput();
                    StartPanel();
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanel();
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameManager.Instance.infoText.gameObject.SetActive(false);
        ClosePanel();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
