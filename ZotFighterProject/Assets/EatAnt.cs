using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EatAnt : MonoBehaviour
{
    PlayerGlobals playerGlobals;

    public int healthGain = 10;

    private GameObject currentAnt = null; //ant currently being collided with

    // Start is called before the first frame update
    void Start()
    {
        playerGlobals = GetComponent<PlayerGlobals>();

        var eatAction = new InputAction("eat", binding: "<Keyboard>/e");
        eatAction.performed += OnEat;
        eatAction.canceled += OnUnEat;
        eatAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEat(InputAction.CallbackContext context)
    {
        if (currentAnt != null)
        {
            Destroy(currentAnt);
            currentAnt = null;
            playerGlobals.health += healthGain;
        }
    }

    void OnUnEat(InputAction.CallbackContext context)
    {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ant"))
        {
            currentAnt = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ant"))
        {
            currentAnt = null;
        }
    }
}
