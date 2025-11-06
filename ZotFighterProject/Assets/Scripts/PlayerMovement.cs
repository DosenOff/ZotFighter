using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 20;
    // public float GravConstant = 10;
    // public float groundDistFactor = 0.01f;

    PlayerGlobals globals;
    Vector2 velocity;
    // BoxCollider2D collider;

    // bool OnGround = false;

    // bool moving = false;

    public int jumpFactor = 5;
    [SerializeField]
    private bool isGrounded = false;
    private bool pressedSpace = false;

    private PlayerFeet feet;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        globals = GetComponent<PlayerGlobals>();
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponentInChildren<PlayerFeet>();

        // collider = GetComponent<BoxCollider2D>();

        // setup move input action
        var moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        
        moveAction.AddCompositeBinding("Dpad")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.performed += OnMove;
        moveAction.canceled += OnCancelMove;
        moveAction.Enable();

        // setup crouch input action
        var crouchAction = new InputAction("crouch", binding: "<Keyboard>/s");

        crouchAction.performed += OnCrouch;
        crouchAction.canceled += OnUnCrouch;
        crouchAction.Enable();

        // // setup jump input action
        // var jumpAction = new InputAction("jump", binding: "<Keyboard>/w");

        // jumpAction.performed += OnJump;
        // jumpAction.Enable();
    }

    // starts movement of Player when move inputs are performed
    void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move started");
        Vector2 input = context.ReadValue<Vector2>();
        velocity.x = input.x * speed;
        globals.direction = System.Math.Sign(input.x);
        // moving = true;
    }

    // stops movement of Player when input is canceled
    void OnCancelMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move stopped");
        velocity.x = 0;
        // moving = false;
    }

    // implement crouch animation
    void OnCrouch(InputAction.CallbackContext context)
    {
        // *temporary vertical scaling for crouch
        Vector3 scale = transform.localScale;
        scale.y *= 0.5f;
        transform.localScale = scale;

        // ensures player is on same y-level
        transform.localPosition = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
    }

    // implement uncrouch animation when input is canceled
    void OnUnCrouch(InputAction.CallbackContext context)
    {
        // *temporary vertical scaling for crouch
        Vector3 scale = transform.localScale;
        scale.y *= 2.0f;
        transform.localScale = scale;

        // ensures player is on same y-level
        transform.localPosition = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    // void OnJump(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Player: jumped");
    //     velocity.y = speed;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log($"Player: collided with obj with tag {collision.gameObject.tag}");
    // }

    void Update()
    {
        pressedSpace = Input.GetKey(KeyCode.Space);
        // Debug.Log($"velocity: {velocity.x}, {velocity.y}");
        //transform.position += velocity * Time.deltaTime;
        // if (!onGround) velocity.y -= GravConstant * Time.deltaTime;
        // if (moving) return;    
    }

    private void FixedUpdate()
    {
        Jump();
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }

    void Jump()
    {
        isGrounded = feet.touchingGround;
        if (isGrounded && pressedSpace)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpFactor);
            Debug.Log(rb.velocity);
        } 
    }
}
