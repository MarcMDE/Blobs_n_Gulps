using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : BaseAction
{
    [SerializeField] float arriveRadius = 2.0f;

    Vector3 target;

    public override void OnStart()
    {
        arriveRadius *= arriveRadius;
    }

    public override void Init()
    {
        target = new Vector3(Random.Range(-Globals.WorldSize / 2, Globals.WorldSize / 2), 0, Random.Range(-Globals.WorldSize / 2, Globals.WorldSize / 2));
    }

    public override bool Update()
    {
        return MoveTowards(target, arriveRadius);
    }

    public override void End()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(target, 0.3f);
    }
}
