using System.Collections;
using TMPro;
using UnityEngine;

public class ConnectionSliderScript : MonoBehaviour
{
    public bool isComplete = false;

    public bool isLoading = false;

    bool um = false;

    public void StartLoad()
    {
        if (!isComplete && !isLoading)
        {
            isLoading = true;
        }
    }

    IEnumerator LoadText()
    {
        if (!um)
        {
            um = true;
            transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "Connecting.";
            yield return new WaitForSeconds(0.3f);
            transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "Connecting..";
            yield return new WaitForSeconds(0.3f);
            transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "Connecting...";
            yield return new WaitForSeconds(0.3f);
            transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "Connecting";
            yield return new WaitForSeconds(0.1f);
            um = false;
        }
    }

    public void Reset()
    {
        isComplete = false;
        isLoading = false;
        transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value = 0;
        transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "";
        um = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            StartCoroutine(LoadText());
            transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value += Time.deltaTime / 5;
            if (transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value >= 1)
            {
                isComplete = true;
                isLoading = false;
                transform.Find("Connecting").GetComponent<TextMeshProUGUI>().text = "Connected!";
                GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().stage += 1;
                GameObject.Find("Hack Panel").GetComponent<HackPanelScript>().StatusUpdate();
            }
        }
    }
}
