using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float hitDistance = 5f;
    public int attackDamage = 5;
    public float attackCooldown = 1f;

    PlayerGlobals globals;
    bool attacking;
    float attackTimer;
    // bool attackType;

    // Start is called before the first frame update
    void Start()
    {
        globals = GetComponent<PlayerGlobals>();

        // setup attack input action
        var attackAction = new InputAction("move", binding: "<Keyboard>/space");
        attackAction.performed += OnAttack;
        attackAction.Enable();
    }

    // implements player attack on input, sets the attacking state of the player to True
    void OnAttack(InputAction.CallbackContext context)
    {
        if (attacking) return;
        Debug.Log(attacking);
        attacking = true;
        attackTimer = attackCooldown;
        HandleHit();
    }

    // calculates hit with any objects in direction of Player, then acts on the object if it is an enemy
    void HandleHit()
    {
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(globals.direction, 0), hitDistance);
        // if (hit)
        // {
        //     GameObject hitObj = hit.transform.gameObject;
        //     Debug.Log($"Player attacking {hitObj.name}");
        //     EnemyGlobals hitGlobals = hitObj.GetComponent<EnemyGlobals>();
        //     if (hitGlobals)
        //     {
        //         Debug.Log("Player hit an enemy");
        //         hitGlobals.TakeDamage(attackDamage);
        //     }
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacking)
        {
            // reduces attackTimer by time passed, sets attacking to false once timer is over
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attacking = false;
            }
        }
    }
}
