using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int speed = 20;
    // public int jumpFactor = 5;
    // public float GravConstant = 10;
    // public float groundDistFactor = 0.01f;

    Vector3 velocity;
    // BoxCollider2D collider;

    // bool onGround = false;

    // bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        // collider = GetComponent<BoxCollider2D>();

        // setup move input action
        var moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        
        moveAction.AddCompositeBinding("Dpad")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.performed += onMove;
        moveAction.canceled += onCancelMove;
        moveAction.Enable();

        // setup crouch input action
        var crouchAction = new InputAction("crouch", binding: "<Keyboard>/s");

        crouchAction.performed += onCrouch;
        crouchAction.canceled += onUnCrouch;
        crouchAction.Enable();

        // // setup jump input action
        // var jumpAction = new InputAction("jump", binding: "<Keyboard>/w");

        // jumpAction.performed += onJump;
        // jumpAction.Enable();
    }

    // starts movement of Player when move inputs are performed
    void onMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move started");
        Vector2 input = context.ReadValue<Vector2>();
        velocity.x = input.x * speed;
        // moving = true;
    }

    // stops movement of Player when input is canceled
    void onCancelMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move stopped");
        velocity.x = 0;
        // moving = false;
    }

    // implement crouch animation
    void onCrouch(InputAction.CallbackContext context)
    {
        // *temporary vertical scaling for crouch
        Vector3 scale = transform.localScale;
        scale.y *= 0.5f;
        transform.localScale = scale;
    }

    // implement uncrouch animation when input is canceled
    void onUnCrouch(InputAction.CallbackContext context)
    {
        // *temporary vertical scaling for crouch
        Vector3 scale = transform.localScale;
        scale.y *= 2.0f;
        transform.localScale = scale;
    }

    // adds vertical velocity for jump
    // void onJump(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Player: jumped");
    //     velocity.y = speed;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log($"Player: collided with obj with tag {collision.gameObject.tag}");
    // }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"velocity: {velocity.x}, {velocity.y}");
        transform.position += velocity * Time.deltaTime;
        // if (!onGround) velocity.y -= GravConstant * Time.deltaTime;
        // if (moving) return;
    }
}
