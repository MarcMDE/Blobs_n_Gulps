using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DecisionManager : MonoBehaviour
{

    readonly float[,] decisionMatrix = new float[,] { 
        // Patrol, CollectF, StealF, StealE, GiveF, GiveE, Save, Kill
        { 0,    0,      0,      0,      0.5f,   0.5f,   1,       0   },
        { 0,    0.2f,   0,      0,      0.4f,   0.4f,   0.1f,    0   },
        { 0,    0.5f,   0,      0,      0.4f,   0.1f,   0,       0   },
        { 0.3f, /*0.6f*/0.7f,   /*0.1f*/0.0f,   0,      0,      0,      0,       0   },
        { 0,    0.5f,   0.4f,   0.1f,   0,      0,      0,       0   },
        { 0,    0.2f,   0.4f,   0.4f,   0,      0,      0,       0.1f},
        { 0,    0,      0.5f,   0.5f,   0,      0,      0,       1   }
    };
    
    void Start()
    {
    }

    public Actions GetNewAction(Moods mood)
    {
        float r = Random.Range(0.0f, 1.0f);
        float accProb = 0.0f;

        int i = -1;

        do
        {
            i++;
            accProb += decisionMatrix[(uint)mood, i];
        } while (i < Globals.ACTIONS_COUNT - Globals.SPECIAL_ACTIONS_COUNT && r > accProb);

        if (i == Globals.ACTIONS_COUNT - Globals.SPECIAL_ACTIONS_COUNT)
        {
            Debug.LogWarning("No action choosen");
        }

        return (Actions)i;
    }

    public bool ShouldSpecialAction(Moods mood, Actions action)
    {
        float r = Random.Range(0.0f, 1.0f);

        return r <= decisionMatrix[(int)mood, (int)action];
    }

    void Update()
    {
        
    }
}
