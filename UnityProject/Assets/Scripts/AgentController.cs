using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    [SerializeField] float decisionTime = 1f;
    [SerializeField] float arriveRadius = 1f;

    DecisionManager decisionManager;
    Rigidbody rb;

    NavMeshAgent navMesh;

    Vector3 targetPosition;

    Moods mood;
    Actions action;

    float[][] decisionMatrix;

    float decisionCounter;

    BaseAction [] actions;

    public Vector3 TargetPosition { set { targetPosition = value; } }

    void Start()
    {
        decisionManager = GameObject.Find("Manager").GetComponent<DecisionManager>();

        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        actions = new BaseAction[Globals.ACTIONS_COUNT];

        actions[(int)Actions.PATROL] = GetComponent<PatrolAction>();
        actions[(int)Actions.PATROL].enabled = false;
        actions[(int)Actions.COLLECT_FOOD] = GetComponent<CollectFoodAction>();
        actions[(int)Actions.COLLECT_FOOD].enabled = false;

        mood = Moods.NEUTRAL;
        action = Actions.NONE;

        decisionCounter = 0;

        // Squared radius
        arriveRadius += arriveRadius;
    }

    void Update()
    {
        if (action == Actions.NONE)
        {
            if (decisionCounter < decisionTime)
                decisionCounter+=Time.deltaTime;
            else
            {
                decisionCounter = 0;

                action = decisionManager.GetNewAction(mood);
                actions[(int)action].enabled = true;
                actions[(int)action].Init();
                Debug.Log("Start action" + action.ToString());
            }
        }
        else
        {
            if (actions[(int)action].Update())
            {
                Debug.Log("Finsh action" + action.ToString());
                actions[(int)action].enabled = false;
                action = Actions.NONE;
            }
        }
    }

    
}
