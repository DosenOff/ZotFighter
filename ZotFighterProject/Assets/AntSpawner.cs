using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public GameObject Ant;
    public float waitMin = 15f;
    public float waitMax = 20f;
    public Vector3 antVelocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            spawnAnt();
            yield return new WaitForSeconds(Random.Range(waitMin, waitMax));
        }
    }
    
    void spawnAnt()
    {
        GameObject newObj = Instantiate(Ant, transform.position, transform.rotation);

        Ant ant = newObj.GetComponent<Ant>();
        if (ant != null)
        {
            ant.Initialize(antVelocity);
        }
    }
}
