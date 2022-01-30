using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveFoodAction : BaseAction
{
    enum Steps { SEARCH, GET, GO_TO_DEPOT, STORE, END };
    [SerializeField] float arriveRadius = 2.0f;
    [SerializeField] float dropRadius = 5;

    FoodSpawner foodSpawner;
    DepotManager depotManager;

    Food food;
    Steps step;

    protected override void OnStart()
    {
        arriveRadius *= arriveRadius;
        dropRadius *= dropRadius;

        foodSpawner = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
        depotManager = GameObject.Find(Globals.NAMES[1-(int)faction] + "Depot").GetComponent<DepotManager>();
    }

    public override void Init()
    {
        // TODO: return bool? -> food can be null
        food = foodSpawner.GetAvailable();
        if (food == null)
            step = Steps.END;
        else
        {
            food.Select();
            step = Steps.SEARCH;
        }
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
                food.transform.localPosition = Globals.CARRY_OFFSET;
                food.Take();
                step = Steps.GO_TO_DEPOT;
                break;
            case Steps.GO_TO_DEPOT:
                if (MoveTowards(depotManager.transform.position, dropRadius))
                {
                    step = Steps.STORE;
                    showMood = true;
                    StartCoroutine(ResetMood());
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
        if (food != null)
        {
            food.Reset();
            food.transform.parent = foodSpawner.transform.parent;
            food = null;
        }

        navMeshAgent.destination = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawSphere(target, 0.3f);
    }
}

