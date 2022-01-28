using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] uint poolSize = 20;

    Spawner spawner;
    [SerializeField] uint initialN;
    [SerializeField] float initialR;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        spawner = new Spawner(prefab, poolSize, transform);
        SpawnInitial(initialN, Vector3.zero, initialR);
    }

    void Update()
    {
        
    }

    public void SpawnInitial(uint n, Vector3 o, float r)
    {
        for (uint i=0; i<n; i++)
        {
            float t = Random.Range(0.0f, 2 * Mathf.PI);
            float rr = Random.Range(0.0f, r);
            Vector3 position = new Vector3(
                o.x + rr * Mathf.Cos(t),
                o.y,
                o.y + rr * Mathf.Sin(t));

            Spawn(position, Vector3.zero);
        }
    }

    public void TestSpawnInitial()
    {
        //Init();
        //SpawnInitial(initialN, Vector3.zero, initialR);
    }

    public void Spawn(Vector3 p, Vector3 r)
    {
        Debug.Log("Spawn");
        GameObject o = spawner.Spawn();
        o.transform.position = p;
        o.transform.rotation = Quaternion.Euler(r);
        o.SetActive(true);
    }
}
