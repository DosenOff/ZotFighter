using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobals : MonoBehaviour
{
    public int health = 100;
    public int direction = 1;

    UI ui;

    public void TakeDamage(int damage)
    {
        health -= damage;
        ui.UpdatePlayerHealth(health);
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
