using UnityEngine;

public class PlanetMovement : MonoBehaviour
{

public Transform orbitPivot;
public Material lineMaterial;
public float orbitSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawOrbitPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitPivot != null)
        {
            transform.RotateAround(orbitPivot.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }

    void DrawOrbitPath()
    {
        if (orbitPivot != null)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 100;
            lineRenderer.widthMultiplier = 0.1f;
            lineRenderer.material = lineMaterial;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            for (int i = 0; i < 100; i++)
            {
                float angle = i * Mathf.PI * 2 / 100;
                Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * Vector3.Distance(transform.position, orbitPivot.position);
                lineRenderer.SetPosition(i, orbitPivot.position + position);
            }
        }
    }
}
