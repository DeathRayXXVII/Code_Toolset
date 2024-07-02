using UnityEngine;
using Cinemachine;

public class CameraRecenter : MonoBehaviour
{
    private CinemachineFreeLook camera;
    private bool recenterButtonPressed = false;

    void Start()
    {
        camera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (Input.GetAxis("CameraRecenter") == 1)
        {
            camera.m_RecenterToTargetHeading.m_enabled = true;

            // Reset the recentering state after a delay (adjust the time as needed)
            Invoke("ResetRecenterState", 0.5f);
        }
        else if (recenterButtonPressed)
        {
            // This block will execute after the delay if the button was pressed
            camera.m_RecenterToTargetHeading.m_enabled = false;
            recenterButtonPressed = false;
        }
    }

    void ResetRecenterState()
    {
        recenterButtonPressed = true;
    }
}