using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    [SerializeField] float decisionTime = 1f;
    [SerializeField] float lifeTime = 120f;
    [SerializeField] int maxMoodLevel = 1;
    [SerializeField] Factions faction;
    [SerializeField] GameObject crown;
    [SerializeField] Material good, bad, neutral;
    [SerializeField] float godSpeed = 20f;

    DecisionManager decisionManager;
    Rigidbody rb;
    float regularSpeed;



    NavMeshAgent navMeshAgent;
    Animator animator;
    Food godFood;
    bool possesed;

    FoodSpawner foodSpawner;
    public Moods Mood { get { return mood; } set { 
            mood = value;
            if (mood == Moods.GOOD)
                crown.GetComponent<Renderer>().material = good;
            else if (mood == Moods.PISSED)
                crown.GetComponent<Renderer>().material = bad;
            else
                crown.GetComponent<Renderer>().material = neutral;
        } }
    Moods mood;

    Actions action;

    DepotManager depotManager;
    DepotManager enemyDepotManager;
    bool godSearch;
    bool dolent, bonet;

    float[][] decisionMatrix;

    float decisionCounter;

    BaseAction [] actions;

    void Start()
    {

        decisionManager = GameObject.Find("Manager").GetComponent<DecisionManager>();

        depotManager = GameObject.Find(Globals.NAMES[(int)faction] + "Depot").GetComponent<DepotManager>();
        enemyDepotManager = GameObject.Find(Globals.NAMES[1 - (int)faction] + "Depot").GetComponent<DepotManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        regularSpeed = navMeshAgent.speed;
        animator = GetComponentInChildren<Animator>();
        foodSpawner = GameObject.Find("FoodSpawner").GetComponent<FoodSpawner>();
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
        godFood = null;
        godSearch = false;
        decisionCounter = 0;
        possesed = false;
        bonet = false;
        dolent = false;

        StartCoroutine(Die());
    }
    void Update()
    {
        if (!possesed)
        {
            navMeshAgent.speed = regularSpeed;

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
        else
        {
            navMeshAgent.speed = godSpeed;
            if (godSearch)
            {
                if(MoveTowards(godFood.transform.position, 9))
                {
                    godFood.transform.parent = transform;
                    godFood.transform.localPosition = Globals.CARRY_OFFSET;
                    godFood.Take();
                    godSearch = false;
                }
            }
            else if (godFood != null && godFood.Taken && !dolent && !bonet)
            {
                if (Utils.SqrDist2D(transform.position, depotManager.transform.position) < 15 * 15)
                {
                    print("recolectar");
                    depotManager.AddFood();
                    godFood.transform.parent = foodSpawner.transform;
                    godFood.gameObject.SetActive(false);
                    godFood = null;
                }
                else if (Utils.SqrDist2D(transform.position, enemyDepotManager.transform.position) < 15 * 15)
                {
                    print("Donar a enemy");
                    enemyDepotManager.AddFood();
                    godFood.transform.parent = foodSpawner.transform;
                    godFood.gameObject.SetActive(false);
                    godFood = null;
                    bonet = true;
                    Mood = Moods.NEUTRAL;
                    StartCoroutine(ResetBonet());
                }
            }
            else if (!godSearch && godFood == null && !dolent && !bonet)
            {
                if (Utils.SqrDist2D(transform.position, enemyDepotManager.transform.position) < 15 * 15)
                {
                    print("Agafar de enemy");
                    if (enemyDepotManager.Food > 0)
                    {
                        enemyDepotManager.GetFood();
                        godFood = foodSpawner.GetNew();

                        godFood.Select();
                        godFood.Take();

                        godFood.transform.parent = transform;
                        godFood.transform.localPosition = Globals.CARRY_OFFSET;
                        dolent = true;
                        Mood = Moods.NEUTRAL;
                        StartCoroutine(ResetDolent());
                    }
                }
            }
        }
    }

    IEnumerator ResetBonet()
    {
        yield return new WaitForSeconds(Globals.VISIBLE_MOOD_TIME);
        bonet = false;
        yield return null;
    }

    IEnumerator ResetDolent()
    {
        yield return new WaitForSeconds(Globals.VISIBLE_MOOD_TIME);
        dolent = false;
        yield return null;
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

        navMeshAgent.speed = godSpeed;
    }

    public void SetDestination(Vector3 p)
    {
        navMeshAgent.SetDestination(p);
    }

    public void LeaveBody()
    {
        Irse();
    }

    private void Irse()
    {
        navMeshAgent.speed = regularSpeed;
        godSearch = false;
        if (godFood != null)
        {
            godFood.Reset();
            godFood.transform.parent = foodSpawner.transform.parent;
            godFood = null;
        }

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

        Irse();

        gameObject.SetActive(false);

        yield return null;
    }

    public Moods GetMoodChange(Moods observerMood)
    {
        int newObserverMood = (int)observerMood;

        if (!possesed)
        {
            if (action != Actions.NONE && actions[(int)action].VisibleMood())
            {
                switch (action)
                {
                    case Actions.STEAL_FOOD:
                        //Debug.Log("Steal food");
                        newObserverMood += 1;
                        break;
                    case Actions.STEAL_EGG:
                        newObserverMood += 2;
                        break;
                    case Actions.GIVE_FOOD:
                        //Debug.Log("Give food");
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

            }
        }
        else
        {
            if (bonet) newObserverMood -= 1;
            if (dolent) newObserverMood += 1;
        }

        if (newObserverMood > ((int)Moods.NEUTRAL) + maxMoodLevel)
            newObserverMood = ((int)Moods.NEUTRAL) + maxMoodLevel;
        else if (newObserverMood < ((int)Moods.NEUTRAL) - maxMoodLevel)
            newObserverMood = ((int)Moods.NEUTRAL) - maxMoodLevel;

        return (Moods)newObserverMood;
    }
    public void pickup(Food food)
    {   
        if(godFood != null)
        {
            godFood.Reset();
            godFood.transform.parent = foodSpawner.transform.parent;
            godFood = null;
        }

        godFood = food;
        godSearch = true;
    }



    bool MoveTowards(Vector3 p, float r)
    {
        bool ret = false;
        navMeshAgent.destination = p;

        if (Utils.SqrDist2D(transform.position, p) < r)
        {
            navMeshAgent.SetDestination(transform.position);
            ret = true;
        }

        return ret;
    }
}


