using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{

    public Transform target;

    public float speed = 200;
    public float NextWaypointDistace = 3;

    public Transform EnemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool ReachedEndOfPath;
    Seeker seeker;
    Rigidbody2D rb;
    public bool InAttackRange;
    public bool InSightRange;
    public float sightRange, attackRange;
    public float TimeBetweenAttacks;
    bool alreadyAttacked;
    public LayerMask Player;
    float nextAttackTime = 0;
    float attackRate = 1;
    public int Playerdamage;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
