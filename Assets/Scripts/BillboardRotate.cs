using UnityEngine;

namespace MixedSignals
{
    public class BillboardRotate : MonoBehaviour
{

public Transform planetRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlanetsManager.Instance.planetCamera.transform.position, Vector3.up);

        // Make the billboard face the camera directly by adjusting its rotation, using the camera's forward vector
        transform.rotation = Quaternion.LookRotation(transform.position - PlanetsManager.Instance.planetCamera.transform.position);

    }   
}

}