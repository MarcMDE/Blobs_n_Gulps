using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotManager : MonoBehaviour
{
    int food;

    public int Food { get { return food;  } }

    void Start()
    {
        food = 5;
    }
    
    public void AddFood()
    {
        food+=1;
        //Debug.Log(name + " food: " + food);
    }

    public void GetFood()
    {
        food -= 1;
        //Debug.Log(name + " food: " + food);
    }

    public bool UseFood(int n)
    {
        if (food - n >= 0)
        {
            food -= n;
            //Debug.Log("Depot food: " + food);
            return true;
        }

        return false;
    }


}
