using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionColliderController : MonoBehaviour
{
    AgentController controller;
    bool canSee;

    void Start()
    {
        canSee = true;
        controller = GetComponentInParent<AgentController>();
    }

    private void OnEnable()
    {
        canSee = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (canSee)
        {
            AgentController otherController = other.GetComponent<AgentController>();


            controller.Mood = otherController.GetMoodChange(controller.Mood);
            
            canSee = false;
            StartCoroutine(ResetVision());
            
        }
    }

    IEnumerator ResetVision()
    {
        yield return new WaitForSeconds(Globals.VISIBLE_MOOD_TIME);
        canSee = true;
        yield return null;
    }
}
