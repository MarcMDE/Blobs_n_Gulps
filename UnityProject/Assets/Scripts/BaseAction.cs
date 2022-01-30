using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAction : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    [SerializeField] protected Factions faction;

    void Awake()
    {
        OnStart();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    protected abstract void OnStart();
    public abstract void Init();
    public abstract bool Update();
    public abstract void End();

    protected bool MoveTowards(Vector3 p, float r)
    {
        bool ret = false;
        navMeshAgent.destination = p;

        if ((transform.position - p).sqrMagnitude < r)
        {
            navMeshAgent.SetDestination(transform.position);
            ret =  true;
        }

        return ret;
    }

    protected void Interact()
    {
        animator.SetFloat("velocity", 0);
        animator.SetBool("interact", true);
    }
}
