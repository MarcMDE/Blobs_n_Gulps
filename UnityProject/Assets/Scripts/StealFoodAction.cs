using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealFoodAction : BaseAction
{
    enum Steps { GO_TO_ENEMY_DEPOT, STEAL, GO_TO_DEPOT, STORE, END };
    [SerializeField] float arriveRadius = 2.0f;

    FoodSpawner foodSpawner;
    DepotManager depotManager;
    DepotManager enemyDepotManager;

    Food food;
    Steps step;

    protected override void OnStart()
    {
        arriveRadius *= arriveRadius;
        foodSpawner = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
        depotManager = GameObject.Find(Globals.NAMES[(int)faction] + "Depot").GetComponent<DepotManager>();
        enemyDepotManager = GameObject.Find(Globals.NAMES[1-(int)faction] + "Depot").GetComponent<DepotManager>();
    }

    public override void Init()
    {
        // TODO: return bool? -> food can be null
        if (enemyDepotManager.Food > 0)
            step = Steps.GO_TO_ENEMY_DEPOT;
        else
            step = Steps.END;
    }

    public override bool Update()
    {
        if (step == Steps.END)
            return true;

        switch (step)
        {
            case Steps.GO_TO_ENEMY_DEPOT:
                if (MoveTowards(enemyDepotManager.transform.position, arriveRadius))
                {
                    step = Steps.STEAL;
                }
                break;
            case Steps.STEAL:
                // TODO: Interact anim

                if (enemyDepotManager.Food > 0)
                {
                    enemyDepotManager.GetFood();
                    food = foodSpawner.GetNew();

                    food.Select();
                    food.Take();

                    food.transform.parent = transform;
                    food.transform.localPosition = Globals.CARRY_OFFSET;
                }
                else
                    step = Steps.END;

                
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
        if (food != null)
        {
            food.Reset();
            food = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawSphere(target, 0.3f);
    }
}
