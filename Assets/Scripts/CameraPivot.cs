using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivot : MonoBehaviour
{

public Transform target;


private Vector3 currentScreenPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Mouse Clicked");

        }
    }

}
