using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFoodAction : BaseAction
{
    enum Steps { SEARCH, GET, GO_TO_DEPOT, STORE, END };
    [SerializeField] float arriveRadius = 2.0f;
    [SerializeField] Vector3 carryOffset = new Vector3(0, 0, 1);

    FoodSpawner foodSpawner;
    DepotManager depotManager;
    
    Food food;
    Steps step;

    public override void OnStart()
    {
        arriveRadius *= arriveRadius;
        foodSpawner = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
        depotManager = GameObject.Find("Depot").GetComponent<DepotManager>();
    }

    public override void Init()
    {
        // TODO: return bool? -> food can be null
        food = foodSpawner.GetAvailable();
        food.Select();
        step = Steps.SEARCH;
    }

    public override bool Update()
    {
        if (food == null || step == Steps.END) 
            return true;

        switch (step)
        {
            case Steps.SEARCH:
                if (MoveTowards(food.transform.position, arriveRadius))
                {
                    step = Steps.GET;
                }
                break;
            case Steps.GET:
                // TODO: Interact anim
                food.transform.parent = transform;
                food.transform.localPosition = carryOffset;
                step = Steps.GO_TO_DEPOT;
                break;
            case Steps.GO_TO_DEPOT:
                if (MoveTowards(depotManager.transform.position, arriveRadius))
                {
                    step = Steps.STORE;
                }
                break;
            case Steps.STORE:
                // TODO: Interact
                depotManager.AddFood();
                food.transform.parent = foodSpawner.transform;
                food.gameObject.SetActive(false);
                step = Steps.END;
                break;
            default:
                break;
        }

        return false;
    }

    public override void End()
    {
        food.Reset();
        food = null;
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawSphere(target, 0.3f);
    }
}
