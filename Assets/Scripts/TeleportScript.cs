using TMPro;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public GameObject zone1;
    public GameObject zone2;
    public GameObject text;

    public string promptText1;
    public string promptText2;

    int cooldown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldown++;
        if (zone1.GetComponent<ZoneScript>().playerInZone && cooldown > 60)
        {
            text.SetActive(true);
            GameObject.Find("TeleportText").GetComponent<TMP_Text>().text = promptText1;
            if (Input.GetKeyDown(KeyCode.E))
            {
                cooldown = 0;
                GameObject.FindGameObjectWithTag("Player").transform.position = zone2.transform.position;
                return;
            }
        }
        else if (zone2.GetComponent<ZoneScript>().playerInZone && cooldown > 60)
        {
            text.SetActive(true);
            GameObject.Find("TeleportText").GetComponent<TMP_Text>().text = promptText2;
            if (Input.GetKeyDown(KeyCode.E) && cooldown > 30)
            {
                cooldown = 0;
                GameObject.FindGameObjectWithTag("Player").transform.position = zone1.transform.position;
                return;
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
