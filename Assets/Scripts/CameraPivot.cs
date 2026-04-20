using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivot : MonoBehaviour
{

public Transform target;
public float rotationSpeed = 5f;
public float zoomSpeed = .3f;

public float zoomValue;
public float minZoom = 5f;
public float maxZoom = 50f;

public float mouseYLimit = 80f;

private Vector3 currentScreenPos;

    void Awake()
    {
        ResetZoom();
    }

    // Update is called once per frame
    void Update()
    {

        // If held mouse
        if (Input.GetKey(KeyCode.Mouse0))
        {
            /*
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * rotationSpeed);
            */

            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.RotateAround(target.position, Vector3.up, mouseX);

            float currentXAngle = transform.eulerAngles.x;
            float desiredXAngle = currentXAngle - mouseY;
            if (desiredXAngle > 180f) desiredXAngle -= 360f;
            desiredXAngle = Mathf.Clamp(desiredXAngle, -mouseYLimit, mouseYLimit);
            float angleToRotate = desiredXAngle - currentXAngle;
            transform.RotateAround(target.position, transform.right, angleToRotate);    

        }

        // Scroll
        zoomValue -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 10f;
        zoomValue = Mathf.Clamp(zoomValue, 0f, 1f);

        float targetDistance = Mathf.Lerp(minZoom, maxZoom, zoomValue);

        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * targetDistance;

    }



    void ResetZoom()
    {
        zoomValue = .75f;
    }


}
