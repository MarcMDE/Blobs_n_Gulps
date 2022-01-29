using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    Spawner spawner;
    [SerializeField] GameObject prefab;
    [SerializeField] uint poolSize = 20;
    [SerializeField] float radius = Globals.WorldSize/2;
    [SerializeField] float spawnTime = 1.0f;

    float spawnCounter;

    void Start()
    {
        spawner = new Spawner(prefab, poolSize, transform);
        spawnCounter = 0;
    }


    void Update()
    {
        if (spawnCounter < spawnTime)
            spawnCounter += Time.deltaTime;
        else
        {
            spawnCounter = 0;

            GameObject foodInst = spawner.Spawn();

            if (foodInst != null)
            {
                Vector3 pos = Utils.RandomCircPosition(transform.position, radius);
                foodInst.transform.position = pos;
                foodInst.SetActive(true);
            }
        }
    }

    public Food GetAvailable()
    {
        uint i = 0;
        Food f;
        do
        {
            f = null;
            GameObject fObject = spawner.Get(i);
            if (fObject.activeInHierarchy)
            {
                f = fObject.GetComponent<Food>();
            }
            i++;

        } while (i < spawner.PoolSize && (f == null || f != null && f.Selected));

        return f;
    }
}
