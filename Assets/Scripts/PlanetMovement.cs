using UnityEngine;

public class PlanetMovement : MonoBehaviour
{

public Transform orbitPivot;
public Material lineMaterial;
public float orbitSpeed = 10f;

public LineRenderer lineRenderer;

public bool isSelected = false;

public GameObject selectionIndicator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(selectionIndicator != null)
        selectionIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitPivot != null)
        {
            transform.RotateAround(orbitPivot.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }

    }

}
