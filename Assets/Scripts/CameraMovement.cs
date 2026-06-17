using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraMovement : MonoBehaviour
{
    public CinemachineCamera cam;
    public float cameraSpeed = 5f;
    public float baseFov = 60f;
    public float zoomFov;
    float currentFov;
    float targetFov;
    bool isZoomed = false;

    void Start()
    {
        cam = GetComponent<CinemachineCamera>();

        cam.Lens.FieldOfView = baseFov;
        isZoomed = false;
        currentFov = baseFov;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            isZoomed = !isZoomed; // toggle zoom state
        }

        // if player pressed Q and is zoomed out; zoom in. Otherwise zoom out
        targetFov = isZoomed ? zoomFov : baseFov;
        currentFov = Mathf.Lerp(currentFov, targetFov, Time.deltaTime * cameraSpeed);
        cam.Lens.FieldOfView = currentFov;
    }
}
