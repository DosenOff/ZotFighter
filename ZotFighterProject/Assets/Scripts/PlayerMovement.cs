using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 20;

    PlayerGlobals globals;
    Vector2 velocity;

    public int jumpFactor = 5;
    [SerializeField]
    private bool isGrounded = false;
    private bool pressedSpace = false;

    private PlayerFeet feet;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        globals = GetComponent<PlayerGlobals>();
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponentInChildren<PlayerFeet>();
        anim = GetComponent<Animator>();

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
    }

    // starts movement of Player when move inputs are performed
    void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move started");
        Vector2 input = context.ReadValue<Vector2>();
        velocity.x = input.x * speed;
        globals.direction = System.Math.Sign(input.x);

        if (velocity.x > 0)
            anim.SetBool("WalkingR", true);
        else if (velocity.x < 0)
            anim.SetBool("WalkingL", true);
    }

    // stops movement of Player when input is canceled
    void OnCancelMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move stopped");
        velocity.x = 0;
        anim.SetBool("WalkingL", false);
        anim.SetBool("WalkingR", false);
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

    void Update()
    {
        pressedSpace = Input.GetKey(KeyCode.Space);
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
