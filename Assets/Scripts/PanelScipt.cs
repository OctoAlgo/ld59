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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartPanel();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ClosePanel();
        }
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
