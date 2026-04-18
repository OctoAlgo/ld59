using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarPopulator : MonoBehaviour
{

public int starCount = 8000;
public float starFieldSize = 100f;
public float minStarSize = 0.1f;
public float maxStarSize = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            star.transform.position = new Vector3(Random.Range(-starFieldSize, starFieldSize), Random.Range(-starFieldSize, starFieldSize), Random.Range(-starFieldSize, starFieldSize));
            star.transform.localScale = Vector3.one * Random.Range(minStarSize, maxStarSize);
            star.GetComponent<Renderer>().material.color = Color.white;
            star.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {

    }

}
