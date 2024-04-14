using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding;
using System.Linq;

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
    public LayerMask Enemy;
    float nextAttackTime = 0;
    float attackRate = 1;
    public int damage;
    public Transform PlayerTrans;
    public float DeBug;
    public List<Transform> Enemies;
    public string ID;
    public Summons summon;
    public List<GameObject> SummonSorter;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
        ID = name + summon.AvalibleSummons;

        for (int i = 0; i < Object.FindObjectsOfType<Ally>().Length; i++)
        {
            if (Object.FindObjectsOfType<Ally>()[i] != this)
            {
                if (Object.FindObjectsOfType<Ally>()[i].ID == ID)
                {
                    GameObject summon = FindObjectOfType<Ally>().gameObject;
                    SummonSorter.Add(summon);
                    List<GameObject> onlyUniqueObjects = SummonSorter.Distinct().ToList();
                    for(int j = 0; j < SummonSorter.Count; j++)
                    {
                        Destroy(SummonSorter[j]);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        InAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, Enemy);
        InSightRange = Physics2D.OverlapCircle(transform.position, sightRange, Enemy);

        if (InAttackRange && InSightRange)
        {
            Attack();
        }
        if (InSightRange && !InAttackRange)
        {
            FindClosestEnemy();
            Chase();
        }
        if (!InSightRange)
        {
            float distance = Vector2.Distance(rb.position, PlayerTrans.position);
            if (distance <= DeBug)
            {
                rb.velocity = Vector2.zero;
                return;
            }
            else
            {
                MovementLogic();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
            }
        }




    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Damage();
            nextAttackTime = Time.time + 1 / attackRate;
        }
        rb.velocity = Vector3.zero;
    }

    void Damage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, Enemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(damage);
        }
    }

    void Chase()
    {
        MovementLogic();
    }

    void UpdatePath()
    {
        if (InSightRange)
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        else
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, PlayerTrans.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void MovementLogic()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else
        {
            ReachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.velocity = new Vector2(force.x, rb.velocity.y);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < NextWaypointDistace)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FindClosestEnemy()
    {
        target = FindClosestTransform(Enemies, transform);
    }

    Transform FindClosestTransform(List<Transform> transforms, Transform targetTransform)
    {
        Transform closestTransform = transforms.OrderBy(t => Vector3.Distance(t.position, targetTransform.position)).FirstOrDefault();
        return closestTransform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}