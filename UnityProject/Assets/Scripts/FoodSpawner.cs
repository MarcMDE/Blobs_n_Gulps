using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    Spawner spawner;
    [SerializeField] GameObject prefab;
    [SerializeField] uint poolSize = 20;
    [SerializeField] float spawnTime = 2.5f;
    [SerializeField] Vector3 offset;
    [SerializeField] float dec = 0.1f;
    [SerializeField] float decTime = 15;
    [SerializeField] float minTime = 0.1f;

    float spawnCounter;

    void Start()
    {
        spawner = new Spawner(prefab, poolSize, transform);
        spawnCounter = spawnTime;
        StartCoroutine(DecTime());
        
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
                float x = Random.Range(-Globals.WorldSize / 2 + 5, Globals.WorldSize / 2 - 5);
                float z = Random.Range(-Globals.WorldSize / 3, Globals.WorldSize / 3);
                //Vector3 pos = Utils.RandomCircPosition(transform.position, radius);
                foodInst.transform.position = new Vector3(x, 0, z) + offset;
                foodInst.SetActive(true);
            }
            
        }
    }

    public Food GetNew()
    {
        GameObject foodInst = spawner.Spawn();
        foodInst.SetActive(true);
        return foodInst.GetComponent<Food>();
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

    IEnumerator DecTime()
    {
        yield return new WaitForSeconds(decTime);
        spawnTime -= dec;
        if (spawnTime < minTime) spawnTime = minTime;
        yield return null;
    }
}
