using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Moods { DEMON = -3, ANGRY, PISED, NEUTRAL = 0, HAPPY, ALTRUIST, ANGEL }

public class AgentController : MonoBehaviour
{
    const uint MOODS_LENGTH = 7;
    const uint ACTIONS_LENGTH = 8;

    NavMeshAgent navMesh;

    Vector3 targetPosition;

    Moods mood;
    NeutralActions neutralAction;
    SelfishActions selfishAction;
    AltruistActions atruistAction;

    float[][] decisionMatrix;

    bool onAction;

    public Vector3 TargetPosition { set { targetPosition = value; } }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        mood = Moods.NEUTRAL;
        onAction = false;
    }

    void Update()
    {
        navMesh.SetDestination(targetPosition);
    }

    void MoveTowards()
    {

    }
}
