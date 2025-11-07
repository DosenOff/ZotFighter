using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniSCSlug : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    [SerializeField]
    private Transform player;

    public int speed;
    public GameObject slugImageL;
    public GameObject slugImageR;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lastPosition = new Vector2(transform.position.x, transform.position.y);

        transform.position = new Vector2(
            Mathf.MoveTowards(transform.position.x, player.position.x, speed * Time.deltaTime),
            transform.position.y);

        Vector2 velocity = (Vector2)transform.position - lastPosition;

        FlipImage(velocity);
    }

    private void FlipImage(Vector2 velocity)
    {
        if (velocity.x > 0f)
        {
            slugImageL.SetActive(false);
            slugImageR.SetActive(true);
        }
        else if (velocity.x < 0f)
        {
            slugImageR.SetActive(false);
            slugImageL.SetActive(true);
        }
    }
}
