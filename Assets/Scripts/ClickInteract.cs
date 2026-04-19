using UnityEngine;

public class ClickInteract : MonoBehaviour
{

public LayerMask interactableLayer;
public PlanetMovement currentPlanet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = PlanetsManager.Instance.planetCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
            {
                if(hit.collider.gameObject.GetComponent<PlanetMovement>() == null) return;

                hit.collider.gameObject.GetComponent<PlanetMovement>().isSelected = true;

                if (currentPlanet != null)
                {
                    currentPlanet.isSelected = false;
                    currentPlanet.selectionIndicator.SetActive(false);
                }
                currentPlanet = hit.collider.gameObject.GetComponent<PlanetMovement>();
                currentPlanet.UpdatePlanetInfo();
                currentPlanet.selectionIndicator.SetActive(true);
            }
        }
    }
}
