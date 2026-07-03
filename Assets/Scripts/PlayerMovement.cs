using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPossessable
{

    Rigidbody rb;
    public GameObject boatObject;
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    float moveHorizontal;
    float moveForward;

    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public float upMultiplier = 2f;
    bool isGrounded = true;
    public LayerMask groundLayer;
    float groundCheckTimer = 0f;
    float groundCheckDelay = 0.3f;
    float playerHeight;
    float raycastDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

    }

    void Update()
    {
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(moveHorizontal, 0, moveForward).normalized;
        Vector3 targetVelocity = movement * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // prevent sliding if not moving
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    public void Move(Vector2 input)
    {
        moveForward = input.y;
        moveHorizontal = input.x;
    }

    public void Jump()
    {
        if(!isGrounded) return;
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * upMultiplier * Time.fixedDeltaTime;
        }
    }

    public void Interact()
    {
        InputManager.instance.SwitchControlTo(boatObject);
    }

    public void OnPossess()
    {

    }

    public void OnUnpossess()
    {
        
    }
}