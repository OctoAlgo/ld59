using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float maxRotationX;
    [SerializeField] private float maxRotationY;

    private Vector3 startingRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position 
        Vector3 mousePos = Input.mousePosition;
        float normalizedX = mousePos.x / Screen.width;
        float normalizedY = mousePos.y / Screen.height;

        // calculate rotation based on all the parameters
        float rotationY = (normalizedX - 0.5f) * mouseSensitivity * maxRotationY;
        float rotationX = -(normalizedY - 0.5f) * mouseSensitivity * maxRotationX;

        // apply rotation + starting rotation
        transform.localRotation = Quaternion.Euler(
            startingRotation.x + rotationX,
            startingRotation.y + rotationY,
            startingRotation.z
        );
    }
}
