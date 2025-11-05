using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float hitDistance = 5f;

    PlayerGlobals globals;
    bool attacking;
    // bool attackType;

    // Start is called before the first frame update
    void Start()
    {
        globals = GetComponent<PlayerGlobals>();

        // setup attack input action
        var attackAction = new InputAction("move", binding: "<Keyboard>/space");
        attackAction.performed += onAttack;
        attackAction.Enable();
    }

    void onAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Player: attacked");
        Debug.Log(globals.health);
        Debug.Log(globals.direction);
        attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(globals.direction, 0), hitDistance);
        if (hit)
        {
            string objName = hit.transform.gameObject.name;
            Debug.Log($"Player hit {objName} at distance {hit.distance}"); 
            if (attacking) Debug.Log($"Player attacking {objName}");
        }
    }
}
