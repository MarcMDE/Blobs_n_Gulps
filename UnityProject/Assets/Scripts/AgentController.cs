using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    [SerializeField] float decisionTime = 1f;

    DecisionManager decisionManager;

    NavMeshAgent navMesh;

    Vector3 targetPosition;

    Moods mood;
    Actions action;

    float[][] decisionMatrix;

    float decisionCounter;

    public Vector3 TargetPosition { set { targetPosition = value; } }

    void Start()
    {
        decisionManager = GameObject.Find("Manager").GetComponent<DecisionManager>();

        navMesh = GetComponent<NavMeshAgent>();

        mood = Moods.NEUTRAL;
        action = Actions.NONE;
    }

    void Update()
    {
        if (action == Actions.NONE)
        {

        }
        else
        {

        }

        navMesh.SetDestination(targetPosition);
    }

    void MoveTowards()
    {

    }
}
