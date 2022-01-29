using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    NavMeshAgent navMesh;

    Vector3 targetPosition;

    public Vector3 TargetPosition { set { targetPosition = value; } }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMesh.SetDestination(targetPosition);
    }
}
