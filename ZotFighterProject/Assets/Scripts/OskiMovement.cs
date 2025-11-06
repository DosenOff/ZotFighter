using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OskiMovement : MonoBehaviour
{
    public int speed;
    public int aggressionDist;
    public int attackingDist;
    public int backingDist;
    public float attackCooldown;
    public float chanceDisengage;
    public float damageCooldown;
    public int attackDamage;
    public float hitDistance;

    EnemyGlobals globals;
    GameObject player;
    Vector3 velocity;
    int? decision;
    float backingTarget;
    float attackingTimer;
    float damageTimer;

    const int ATTACKING = 0;
    const int BACKING = 1;
    const int APPROACHING = 2;
    const int DAMAGED = 3;


    // Start is called before the first frame update
    void Start()
    {
        globals = GetComponent<EnemyGlobals>();
        player = GameObject.FindGameObjectWithTag("Player");

        globals.OnDamage += HandleDamage;
    }

    void HandleDamage()
    {
        Debug.Log("being damaged");
        decision = DAMAGED;
        damageTimer = damageCooldown;
        velocity = new Vector3();
    }

    // decides next move based on position of player
    // move towards player until a certain distance from player
    // move away until a certain distance from starting
    void DecideMove()
    {
        // determine the direction and distance of the player from the enemy
        float signedDist = player.transform.position.x - transform.position.x;
        int dirOfPlayer = System.Math.Sign(signedDist);
        float dist = System.Math.Abs(signedDist);

        int rand;
        if (dist < attackingDist) rand = Random.Range(0, 3);
        else rand = Random.Range(1, 3);
        Debug.Log("decided to " + rand);
        if (rand == 0)
        {
            decision = ATTACKING;
            attackingTimer = attackCooldown;
            Debug.Log("oski attacks");
            HandleHit();
        }
        else if (rand == 1)
        {
            decision = APPROACHING;
        }
        else
        {
            decision = BACKING;
            backingTarget = transform.position.x - dirOfPlayer * backingDist;
        }
    }

    // calculates hit with any objects in direction of Player, then acts on the object if it is an enemy
    void HandleHit()
    {
        float signedDist = player.transform.position.x - transform.position.x;
        int dirOfPlayer = System.Math.Sign(signedDist);

        // int layerMask = ~LayerMask.GetMask("Enemy");

        Debug.DrawLine(transform.position, transform.position + new Vector3(dirOfPlayer, 0, 0) * 1000, Color.red, 2.5f);
        RaycastHit2D[] hits = System.Array.Empty<RaycastHit2D>();
        GetComponent<Collider2D>().Raycast(new Vector2(dirOfPlayer, 0), hits, 1000);
        Debug.Log(hits.Length);
        if (hits.Length > 0)
        {
            RaycastHit2D hit = hits[0];
            GameObject hitObj = hit.transform.gameObject;
            Debug.Log($"Enemy attacking {hitObj.name}");
            PlayerGlobals hitGlobals = hitObj.GetComponent<PlayerGlobals>();
            if (hitGlobals)
            {
                Debug.Log("Enemy hit player");
                hitGlobals.TakeDamage(attackDamage);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (decision == null) DecideMove();

        float signedDist = player.transform.position.x - transform.position.x;
        int dirOfPlayer = System.Math.Sign(signedDist);
        float dist = System.Math.Abs(signedDist);

        if (decision == ATTACKING)
        {
            // Debug.Log("attacking, timer: " + attackingTimer);
            attackingTimer -= Time.deltaTime;

            if (attackingTimer < 0)
            {
                if (dist > attackingDist || Random.Range(0f, 1f) < chanceDisengage)
                {
                    DecideMove();
                }
                else
                {
                    attackingTimer = attackCooldown;
                    Debug.Log("oski attacks");
            HandleHit();
                }
            }
        }
        else if (decision == APPROACHING)
        {
            // Debug.Log("approaching, dist: " + dist);
            velocity = new Vector3(dirOfPlayer, 0, 0) * speed;

            if (dist < attackingDist)
            {
                velocity = new Vector3();
                decision = ATTACKING;
                attackingTimer = attackCooldown;
                Debug.Log("oski attacks");
                HandleHit();
            }
        }
        else if (decision == BACKING)
        {
            // Debug.Log($"backing to {backingTarget}, at: " + transform.position.x);
            velocity = new Vector3(-dirOfPlayer, 0, 0) * speed;

            if (dirOfPlayer * (transform.position.x - backingTarget) <= 0)
            {
                velocity = new Vector3();
                DecideMove();
            }
        }
        else if (decision == DAMAGED)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer < 0)
            {
                DecideMove();
            }
        }

        transform.position += velocity * Time.deltaTime;
    }
}
