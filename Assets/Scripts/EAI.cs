using UnityEngine;
using Pathfinding;

public class EAI : MonoBehaviour
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
    Vector3 Startpos;
    public float DeBug;
    public int Playerdamage;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
        Startpos = new Vector2(transform.position.x, transform.position.y);
        
    }
    private void FixedUpdate()
    {
        InAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, Player);
        InSightRange = Physics2D.OverlapCircle(transform.position, sightRange, Player);

        if (InAttackRange && InSightRange) {
            Attack();
        }
        if(InSightRange && !InAttackRange) { 
            Chase(); 
        }
        if (!InSightRange)
        {
            float distance = Vector2.Distance(rb.position, Startpos);
            DeBug = 5;
            if(distance <= 5)
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
        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, Player);
        foreach (Collider2D PlayerHealth in HitPlayer)
        {
            PlayerHealth.GetComponent<PlayerHealth>().TakeDamage(Playerdamage);
        }
    }

    void Chase()
    {
        MovementLogic();
    }
    void UpdatePath()
    {
        if(InSightRange) 
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        else
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, Startpos, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
         if(!p.error)
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

    // Update is called once per frame


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(Startpos, 1f);
        Gizmos.DrawWireSphere(Startpos, DeBug);
    }
}