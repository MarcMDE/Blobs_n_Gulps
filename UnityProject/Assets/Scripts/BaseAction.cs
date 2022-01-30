using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAction : MonoBehaviour
{
    [SerializeField] protected Factions faction;
    protected bool showMood;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    
    void Awake()
    {
        OnStart();
        showMood = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    protected abstract void OnStart();
    public abstract void Init();
    public abstract bool Update();
    public abstract void End();
    public bool VisibleMood()
    {
        return showMood;
    }

    protected bool MoveTowards(Vector3 p, float r)
    {
        bool ret = false;
        navMeshAgent.destination = p;

        if (Utils.SqrDist2D(transform.position, p) < r)
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
    
    protected IEnumerator ResetMood()
    {
        yield return new WaitForSeconds(Globals.VISIBLE_MOOD_TIME);
        showMood = false;
        yield return null;
    }
}
