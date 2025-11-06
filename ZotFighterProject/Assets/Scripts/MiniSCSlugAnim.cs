using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSCSlugAnim : MonoBehaviour
{
    public float randomMin;
    public float randomMax;

    [SerializeField]
    private float randomSpawn;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        randomSpawn = UnityEngine.Random.Range(randomMin, randomMax);
        anim.SetBool($"Spawn {randomSpawn}", true);
    }
}
