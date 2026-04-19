using UnityEngine;

public class WireEndScript : MonoBehaviour
{
    public int type = 0;

    public void getPressed()
    {
        if (GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire != null)
        {
            if (GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire.GetComponent<WireScript>().type == type)
            {
                GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire.GetComponent<WireScript>().GetConnected(gameObject);
                GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire = null;
            }
            else
            {
                GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire = null;
            }
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
