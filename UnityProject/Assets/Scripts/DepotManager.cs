using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepotManager : MonoBehaviour
{
    [SerializeField] Text text;
    int food;

    public int Food { get { return food;  } }

    void Start()
    {
        food = 11;
    }
    
    public void AddFood()
    {
        food+=1;
    }

    private void Update()
    {
        text.text = food.ToString();
    }

    public void GetFood()
    {
        food -= 1;
    }

    public bool UseFood(int n)
    {
        if (food - n >= 0)
        {
            food -= n;
            return true;
        }

        return false;
    }


}
