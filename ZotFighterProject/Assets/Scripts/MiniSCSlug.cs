using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSCSlug : MonoBehaviour
{
    public Tuple<float, float> randomRange;

    [SerializeField]
    private float randomSpawn;
    [SerializeField]
    private int health = 1;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();   
        randomSpawn = UnityEngine.Random.Range(randomRange.Item1, randomRange.Item2);
        anim.SetBool($"Spawn {randomSpawn}", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
