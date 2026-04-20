using UnityEngine;
using UnityEngine.UI;

public class WireScript : MonoBehaviour
{
    public int type = 0;
    public bool connected = false;

    public GameObject wireEnd;

    public void GetPicked()
    {
        if (!connected)
        {
            GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire = this.transform.gameObject;
        }
    }
    public void GetUnPicked()
    {
        GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().selectedWire = null;
    }

    void ExtendWire()
    {

    }
    public void GetConnected(GameObject WireEnd)
    {
        connected = true;
        wireEnd = WireEnd;

        Vector3 startpos = transform.localPosition + new Vector3(16, 0, 0);
        Vector3 endpos = wireEnd.transform.localPosition;

        if (wireEnd != null)
        {
            transform.Find("Wire").GetComponent<Image>().enabled = true;
            transform.Find("Wire").transform.localPosition = ( endpos - (startpos - new Vector3(16, 0, 0)) ) / 2;
            transform.Find("Wire").transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(endpos.y - startpos.y, endpos.x - startpos.x) * Mathf.Rad2Deg);
            transform.Find("Wire").transform.localScale = new Vector3(Vector3.Distance(startpos, endpos) / 20, 1, 1);
        }

        GameObject.Find("Wire Game Manager").GetComponent<WireGameManager>().CheckCompleted();
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
