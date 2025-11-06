using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSCSlugAnim : MonoBehaviour
{
    public GameObject miniSlugObject;
    public GameObject miniSlugAnimObject;

    public int randomMin;
    public int randomMax;

    [SerializeField]
    private int randomSpawn;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        randomSpawn = UnityEngine.Random.Range(randomMin, randomMax);
        anim.SetTrigger($"Spawn {randomSpawn}");
    }

    public void SlugActive()
    {
        miniSlugAnimObject.SetActive(false);
        miniSlugObject.SetActive(true);
    }
}
