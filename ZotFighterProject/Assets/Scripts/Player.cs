using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int speed = 20;

    Vector3 horizontal_velocity;

    // bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        var moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        
        moveAction.AddCompositeBinding("Dpad")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.performed += onMove;
        moveAction.canceled += onCancelMove;
        moveAction.Enable();
    }

    // starts movement of Player when move inputs are performed
    void onMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move started");
        Vector2 input = context.ReadValue<Vector2>();
        Debug.Log($"Player: y-input: {input.y}");
        horizontal_velocity = new Vector3(input.x, 0, 0);

        if (input.y < 0) onCrouch();
        // moving = true;
    }

    // stops movement of Player when input is canceled
    void onCancelMove(InputAction.CallbackContext context)
    {
        Debug.Log("Player: move stopped");
        horizontal_velocity = new Vector3();
        // moving = false;
    }

    // implement crouch animation
    void onCrouch()
    {
        // *temporary vertical scaling for crouch
        // transform.localScale.y *= 0.5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += horizontal_velocity * Time.deltaTime * speed;
        // if (moving) return;
    }
}
