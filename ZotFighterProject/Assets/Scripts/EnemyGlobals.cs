using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobals : MonoBehaviour
{
    public int health = 100;
    public int direction = -1;

    UI ui;

    // apply damage to enemy health, then update ui
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) OnDeath();
        ui.UpdateEnemyHealth(health);
        Debug.Log($"Enemy took {damage} damage. Health: {health}");
    }

    // handle death
    public void OnDeath()
    {
        // TODO: handle death
        Debug.Log("enemy died");
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
