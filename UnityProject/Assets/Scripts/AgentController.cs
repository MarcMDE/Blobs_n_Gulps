using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    [SerializeField] float decisionTime = 1f;
    [SerializeField] float lifeTime = 120f;
    [SerializeField] int maxMoodLevel = 1;

    DecisionManager decisionManager;
    Rigidbody rb;

    NavMeshAgent navMeshAgent;
    Animator animator;

    bool possesed;

    public Moods Mood { get; set; }

    Actions action;

    float[][] decisionMatrix;

    float decisionCounter;

    BaseAction [] actions;

    void Start()
    {

        decisionManager = GameObject.Find("Manager").GetComponent<DecisionManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        actions = new BaseAction[Globals.ACTIONS_COUNT];

        actions[(int)Actions.PATROL] = GetComponent<PatrolAction>();
        actions[(int)Actions.PATROL].enabled = false;
        actions[(int)Actions.COLLECT_FOOD] = GetComponent<CollectFoodAction>();
        actions[(int)Actions.COLLECT_FOOD].enabled = false;
        actions[(int)Actions.STEAL_FOOD] = GetComponent<StealFoodAction>();
        actions[(int)Actions.STEAL_FOOD].enabled = false;
        actions[(int)Actions.GIVE_FOOD] = GetComponent<GiveFoodAction>();
        actions[(int)Actions.GIVE_FOOD].enabled = false;
    }
    void OnEnable()
    {
        Mood = Moods.NEUTRAL;
        action = Actions.NONE;
        decisionCounter = 0;
        possesed = false;

        StartCoroutine(Die());
    }
    void Update()
    {
        if (!possesed)
        {

            if (action == Actions.NONE)
            {
                if (decisionCounter < decisionTime)
                    decisionCounter+=Time.deltaTime;
                else
                {
                    decisionCounter = 0;

                    Debug.Log("Mood: " + Mood);
                    action = decisionManager.GetNewAction(Mood);
                    Debug.Log("Start action" + action.ToString());
                    actions[(int)action].enabled = true;
                    actions[(int)action].Init();
                }
            }
            else
            {
                if (actions[(int)action].Update())
                {
                    Debug.Log("Finsh action" + action.ToString());
                    actions[(int)action].End();
                    actions[(int)action].enabled = false;
                    action = Actions.NONE;
                }
            }
        }
    }

    public void Posses()
    {
        possesed = true;
        if (action != Actions.NONE)
        {
            actions[(int)action].End();
            actions[(int)action].enabled = false;
            action = Actions.NONE;
        }
    }

    public void SetDestination(Vector3 p)
    {
        navMeshAgent.SetDestination(p);
    }

    public void LeaveBody()
    {
        possesed = false;
    }

    void FixedUpdate()
    {
        animator.SetFloat("velocity", navMeshAgent.velocity.sqrMagnitude);
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifeTime);
        if (action != Actions.NONE)
        {
            actions[(int)action].End();
            actions[(int)action].enabled = false;
        }
        gameObject.SetActive(false);
        yield return null;
    }

    public Moods GetMoodChange(Moods observerMood)
    {
        Debug.Log("Enter mood: " + observerMood);
        int newObserverMood = (int)observerMood;

        if (action != Actions.NONE && actions[(int)action].VisibleMood())
        {
            switch (action)
            {
                case Actions.STEAL_FOOD:
                    Debug.Log("Steal food");
                    newObserverMood += 1;
                    break;
                case Actions.STEAL_EGG:
                    newObserverMood += 2;
                    break;
                case Actions.GIVE_FOOD:
                    Debug.Log("Give food");
                    newObserverMood -= 1;
                    break;
                case Actions.GIVE_EGG:
                    newObserverMood -= 2;
                    break;
                case Actions.SAVE:
                    newObserverMood -= 3;
                    break;
                case Actions.KILL:
                    newObserverMood += 3;
                    break;
                default:
                    break;
            }

            if (newObserverMood > ((int)Moods.NEUTRAL) + maxMoodLevel)
                newObserverMood = ((int)Moods.NEUTRAL) + maxMoodLevel;
            else if (newObserverMood < ((int)Moods.NEUTRAL) - maxMoodLevel)
                newObserverMood = ((int)Moods.NEUTRAL) - maxMoodLevel;

            Debug.Log("Exit mood: " + (Moods)newObserverMood);
        }

        return (Moods)newObserverMood;
    }
}
