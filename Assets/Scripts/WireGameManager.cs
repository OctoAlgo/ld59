using UnityEngine;
using System.Collections.Generic;

public class WireGameManager : MonoBehaviour
{
    public bool isComplete = false;

    public GameObject WireStart0;
    public GameObject WireStart1;
    public GameObject WireStart2;
    public GameObject WireStart3;
    public GameObject WireStart4;
    List<GameObject> wireStarts = new List<GameObject>();

    int[] wireStartOrder = new int[5] { 0, 1, 2, 3, 4 };

    public GameObject WireEnd0;
    public GameObject WireEnd1;
    public GameObject WireEnd2;
    public GameObject WireEnd3;
    public GameObject WireEnd4;
    List<GameObject> wireEnds = new List<GameObject>();

    int[] wireEndOrder = new int[5] { 0, 1, 2, 3, 4 };

    public GameObject selectedWire;

    void RandomizeOrder()
    {
        for (int i = 0; i < wireStartOrder.Length; i++)
        {
            int temp = wireStartOrder[i];
            int randomIndex = UnityEngine.Random.Range(i, wireStartOrder.Length);
            wireStartOrder[i] = wireStartOrder[randomIndex];
            wireStartOrder[randomIndex] = temp;
        }
        for (int i = 0; i < wireEndOrder.Length; i++)
        {
            int temp = wireEndOrder[i];
            int randomIndex = UnityEngine.Random.Range(i, wireEndOrder.Length);
            wireEndOrder[i] = wireEndOrder[randomIndex];
            wireEndOrder[randomIndex] = temp;
        }
    }

    void InstatiateWires()
    {
        for (int i = 0; i < wireStartOrder.Length; i++)
        {
            GameObject newWireStart = Instantiate(wireStarts[wireStartOrder[i]], GameObject.Find("Wire Starts").transform);
            newWireStart.transform.localPosition = new Vector3(-99, 100 - (i * 50), 0);
        }
        for (int i = 0; i < wireEndOrder.Length; i++)
        {
            GameObject newWireEnd = Instantiate(wireEnds[wireEndOrder[i]], GameObject.Find("Wire Ends").transform);
            newWireEnd.transform.localPosition = new Vector3(99, 100 - (i * 50), 0);
        }
    }

    public void CheckCompleted()
    {
        int count = 0;
        foreach (Transform child in GameObject.Find("Wire Starts").transform)
        {
            if (child.GetComponent<WireScript>().connected)
            {
                count++;
            }
        }
        if (count == 5)
        {
            isComplete = true;
            GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().stage += 1;
            GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().StatusUpdate();
        }
    }

    void Reset()
    {
        foreach (Transform child in GameObject.Find("Wire Starts").transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in GameObject.Find("Wire Ends").transform)
        {
            Destroy(child.gameObject);
        }
        isComplete = false;
    }

    public void LoadGame()
    {
        Reset();
        wireStarts.Add(WireStart0);
        wireStarts.Add(WireStart1);
        wireStarts.Add(WireStart2);
        wireStarts.Add(WireStart3);
        wireStarts.Add(WireStart4);
        wireEnds.Add(WireEnd0);
        wireEnds.Add(WireEnd1);
        wireEnds.Add(WireEnd2);
        wireEnds.Add(WireEnd3);
        wireEnds.Add(WireEnd4);
        RandomizeOrder();
        InstatiateWires();
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
