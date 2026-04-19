using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    public static PlanetsManager Instance;
    public GameObject planetPrefab;
    public Camera planetCamera;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of PlanetsManager detected. Destroying duplicate.");
            Destroy(gameObject);
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

    public void SignalPlanet(Alien alien)
    {
        ConsoleManager.Instance.StartCoroutine(ConsoleManager.Instance.SelectPlanet(alien));
    }
}
