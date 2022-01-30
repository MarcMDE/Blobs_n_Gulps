using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAction : BaseAction
{
    [SerializeField] float timeout = 5f;
    [SerializeField] float arriveRadius = 2.0f;

    Vector3 target = Vector3.zero;
    float counter;

    bool showMood;

    protected override void OnStart()
    {

        arriveRadius *= arriveRadius;
    }

    public override void Init()
    {
        counter = 0;
        target = GetPosition();
    }


    public override bool Update()
    {
        if (counter < timeout)
            counter += Time.deltaTime;
        else
        {
            navMeshAgent.destination = transform.position;
            return true;
        }

        return MoveTowards(target, arriveRadius);
    }

    public override void End()
    {
        navMeshAgent.destination = transform.position;
    }


   

    Vector3 GetPosition()
    {
        Vector3 pos = new Vector3(Random.Range(-Globals.WorldSize / 2, Globals.WorldSize / 2), Globals.WORLD_MAX_HEIGHT, Random.Range(-Globals.WorldSize / 2, Globals.WorldSize / 2));
        RaycastHit hit;
        var ray = new Ray(pos, Vector3.down);

        if (Physics.Raycast(ray, out hit, Globals.WORLD_MAX_HEIGHT+1, Globals.GROUND_LAYER))
        {
            return hit.point;
        }

        return pos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(target, 2);
    }
    
}
