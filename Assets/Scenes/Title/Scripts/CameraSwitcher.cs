using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Button changeCameraButton;
    [SerializeField] private Camera cameraToDisable;
    [SerializeField] private Camera cameraToEnable;
    [SerializeField] private Canvas[] canvasesToDisable;
    [SerializeField] private Canvas canvasToEnable;

    void Start()
    {
        if (changeCameraButton != null)
        {
            changeCameraButton.onClick.AddListener(SwitchCamera);
        }
    }

    private void SwitchCamera()
    {
        // Disable the canvases in the list
        foreach (Canvas canvas in canvasesToDisable)
        {
            if (canvas != null)
            {
                canvas.enabled = false;
            }
        }

        // Enable the main canvas
        if (canvasToEnable != null)
        {
            canvasToEnable.enabled = true;
        }

        // Disable the current camera
        if (cameraToDisable != null)
        {
            cameraToDisable.enabled = false;
        }

        // Enable the other camera
        if (cameraToEnable != null)
        {
            cameraToEnable.enabled = true;
        }
    }
}
