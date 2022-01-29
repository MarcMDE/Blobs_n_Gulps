using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions { NONE = -1, PATROL = 0, COLLECT_FOOD, STEAL_FOOD, STEAL_EGG, GIVE_FOOD, GIVE_EGG, SAVE }

public class DecisionManager : MonoBehaviour
{
    const uint MOODS_COUNT = 7;
    const uint ACTIONS_COUNT = 8;

    readonly float[,] decisionMatrix = new float[,] { 
        { 0, 0, 0, 0, 0, 0.5f, 0.5f, 1 },
        { 0, 0.2f, 0, 0, 0, 0.4f, 0.4f, 0.1f },
        { 0, 0.5f, 0, 0, 0, 0.4f, 0.1f, 0 },
        { 0.3f, 0.6f, 0.1f, 0, 0, 0, 0, 0 },
        { 0, 0.5f, 0.4f, 0.1f, 0, 0, 0, 0 },
        { 0, 0.2f, 0.4f, 0.4f, 0.1f, 0, 0, 0},
        { 0, 0, 0.5f, 0.5f, 1, 0, 0, 0 }
    };
    
    void Start()
    {
    }

    void Update()
    {
        
    }
}
