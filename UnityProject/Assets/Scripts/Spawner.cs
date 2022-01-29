using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    GameObject o;
    uint poolSize = 0;
    Transform parent;
    GameObject[] objects;

    public uint PoolSize { get { return poolSize; } }

    public Spawner(GameObject o, uint poolSize, Transform parent)
    {
        this.poolSize = poolSize;
        this.o = o;
        this.parent = parent;
        objects = new GameObject[poolSize];

        Init();
    }

    private void Init()
    {
        for (uint i=0; i<poolSize; i++)
        {
            GameObject inst = GameObject.Instantiate<GameObject>(o, parent);
            inst.SetActive(false);
            objects[i] = inst;
        }
    }
    
    public GameObject Spawn()
    {
        uint i = 0; 

        while (i < poolSize && objects[i].activeInHierarchy)
        {
            i++;
        }

        if (i < poolSize)
            return objects[i];

        Debug.LogWarning("No objects available");
        return null;
    }

    public GameObject Get(uint i)
    {
        return objects[i];
    }
}
