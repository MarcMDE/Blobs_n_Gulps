using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions { NONE = -1, PATROL = 0, COLLECT_FOOD, STEAL_FOOD, STEAL_EGG, GIVE_FOOD, GIVE_EGG, SAVE, KILL }
public enum Moods { ANGEL = 0, ALTRUIST, GOOD, NEUTRAL=3, PISSED, ANGRY, DEMON } 


public class DecisionManager : MonoBehaviour
{
    const uint MOODS_COUNT = 7;
    const uint ACTIONS_COUNT = 8;
    const uint SPECIAL_ACTIONS_COUNT = 2;

    readonly float[,] decisionMatrix = new float[,] { 
        { 0,    0,      0,      0,      0.5f,   0.5f,   1,       0   },
        { 0,    0.2f,   0,      0,      0.4f,   0.4f,   0.1f,    0   },
        { 0,    0.5f,   0,      0,      0.4f,   0.1f,   0,       0   },
        { 0.3f, 0.6f,   0.1f,   0,      0,      0,      0,       0   },
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
        } while (i < ACTIONS_COUNT - SPECIAL_ACTIONS_COUNT && r > accProb);

        if (i == ACTIONS_COUNT - SPECIAL_ACTIONS_COUNT)
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
