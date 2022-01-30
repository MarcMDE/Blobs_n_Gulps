using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotManager : MonoBehaviour
{
    uint food;

    public uint Food { get { return food;  } }

    void Start()
    {
        food = 5;
    }
    
    public void AddFood()
    {
        food+=1;
        Debug.Log(name + " food: " + food);
    }

    public void GetFood()
    {
        food -= 1;
        Debug.Log(name + " food: " + food);
    }

    public bool UseFood(uint n)
    {
        if (food - n >= 0)
        {
            food -= n;
            Debug.Log("Depot food: " + food);
            return true;
        }

        return false;
    }


}
