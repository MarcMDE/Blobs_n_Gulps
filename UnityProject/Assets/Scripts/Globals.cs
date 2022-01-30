using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Factions { Blob, Gulp}

public enum Actions { NONE = -1, PATROL = 0, COLLECT_FOOD, STEAL_FOOD, STEAL_EGG, GIVE_FOOD, GIVE_EGG, SAVE, KILL }
public enum Moods { ANGEL = 0, ALTRUIST, GOOD, NEUTRAL = 3, PISSED, ANGRY, DEMON }

public static class Globals
{
    public static float WorldSize = 190;
    public static int MOODS_COUNT = 7;
    public static int ACTIONS_COUNT = 8;
    public static int SPECIAL_ACTIONS_COUNT = 2;
    public static float WORLD_MAX_HEIGHT = 15;
    public static int GROUND_LAYER = 64;
    public static Vector3 CARRY_OFFSET = new Vector3(0, 2.5f, 2.5f);
    public static string[] NAMES = { "Blob", "Gulp" };
    public static float VISIBLE_MOOD_TIME = 3f;
}
