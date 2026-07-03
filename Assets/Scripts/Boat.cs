using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boat : MonoBehaviour, IPossessable
{
    private Rigidbody rigidBody;
    private Vector2 moveInput;
    public GameObject playerObject;
    public float acceleration = 1f;
    public float angularAcceleration = 1f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(moveInput.y > 0)
        {
            rigidBody.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }

        if(moveInput.x != 0)
        {
            rigidBody.AddTorque(transform.up * moveInput.x * angularAcceleration, ForceMode.Acceleration);
        }
    }

    public void Move(Vector2 input)
    {
        moveInput = input;
    }

    public void Jump()
    {    
    }

    public void Interact()
    {
        InputManager.instance.SwitchControlTo(playerObject);
    }

    public void OnPossess()
    {
        
    }

    public void OnUnpossess()
    {
        
    }
}
