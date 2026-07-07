using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public CinemachineCamera cam;
    public GameObject initialTarget;
    private IPossessable activeTarget;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        if(initialTarget != null)
        {
            SwitchControlTo(initialTarget);
        }
    }

    public void SwitchControlTo(GameObject newTarget)
    {
        activeTarget?.OnUnpossess();
        activeTarget = newTarget.GetComponent<IPossessable>();
        cam.Follow = newTarget.transform;
        activeTarget?.OnPossess();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        activeTarget?.Move(input);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        activeTarget?.Jump();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            activeTarget?.Interact();
        }
    }
}