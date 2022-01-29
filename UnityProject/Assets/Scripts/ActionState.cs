using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState
{
    ActionState [] nextStates;
    float[] probs;
    Actions action;

    //public ActionState NextState{ get { return nextState; } }
    public Actions Action { get { return action; } }

    public ActionState(Actions action, ActionState [] nextStates, float [] probs)
    {
        this.nextStates = nextStates;
        this.action = action;
        this.probs = probs;
    }
}
