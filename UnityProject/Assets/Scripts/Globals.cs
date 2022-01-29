using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions { NONE = -1, PATROL = 0, COLLECT_FOOD, STEAL_FOOD, STEAL_EGG, GIVE_FOOD, GIVE_EGG, SAVE, KILL }
public enum Moods { ANGEL = 0, ALTRUIST, GOOD, NEUTRAL = 3, PISSED, ANGRY, DEMON }

public static class Globals
{
    public static float WorldSize = 10;
    public static uint MOODS_COUNT = 7;
    public static uint ACTIONS_COUNT = 8;
    public static uint SPECIAL_ACTIONS_COUNT = 2;
}
