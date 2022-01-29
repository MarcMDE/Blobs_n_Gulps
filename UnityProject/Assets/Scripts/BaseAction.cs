using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAction : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    void Awake()
    {
        OnStart();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    public abstract void OnStart();
    public abstract void Init();
    public abstract bool Update();
    public abstract void End();

    protected bool MoveTowards(Vector3 p, float r)
    {
        navMeshAgent.destination = p;

        if ((transform.position - p).sqrMagnitude < r)
        {
            navMeshAgent.SetDestination(transform.position);
            return true;
        }
        return false;
    }
}
