using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RizzardBoss : MonoBehaviour
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
    public bool InSlowdownRange;
    public float attackRange;
    public float slowdownRange;
    public float TimeBetweenAttacks;
    bool alreadyAttacked;
    public LayerMask Player;
    public int Playerdamage;
    public Animator anim;
    bool attacking;
    public TimeManager timeManager;
    public Movement move;
    public AudioSource Rain;
    public AudioSource Song;
    bool invince;
    public Transform SlowDown;
    public float DamageRange;
    public LayerMask Slow;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
    }
    private void Update()
    {
        timeManager.Slowmotion();
        if (InSlowdownRange && !move.grounded)
        {
            timeManager.SlowDownFactor = timeManager.slowTimeScale;
            Rain.pitch = 0.08f;
            Song.pitch = 0.3f;
        }
        else if (!InSlowdownRange || move.grounded)
        {
            timeManager.SlowDownFactor = 1;
            Rain.pitch = 1f;
            Song.pitch = 1f;
        }

    }

    private void FixedUpdate()
    {
        InAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, Player);
        InSlowdownRange = Physics2D.OverlapCircle(SlowDown.position, slowdownRange, Slow);

        if (InAttackRange)
        {
            Attack();
            attacking = true;
        }

    }

    void Attack()
    {
        MovementLogic();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !invince)
        {
            Damage();
        }
    }

    void Damage()
    {
        Collider2D[] HitPlayer = Physics2D.OverlapCircleAll(transform.position, DamageRange, Player);
        foreach (Collider2D PlayerHealth in HitPlayer)
        {
            if (PlayerHealth.GetComponent<PlayerHealth>() != null && !invince)
            {
                PlayerHealth.GetComponent<PlayerHealth>().TakeDamage(Playerdamage);
                invince = true;
                StartCoroutine(invinsibleTime());
            }
        }

        Collider2D[] HitAlly = Physics2D.OverlapCircleAll(transform.position, DamageRange, Player);
        foreach (Collider2D Ally in HitAlly)
        {
            Ally.GetComponent<Ally>().TakeDamage(Playerdamage);
        }
    }

    void Chase()
    {
        MovementLogic();
    }
    void UpdatePath()
    {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.position, OnPathComplete);
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

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < NextWaypointDistace)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(SlowDown.position, slowdownRange);
        Gizmos.DrawWireSphere(transform.position, DamageRange);
    }
    IEnumerator invinsibleTime()
    {
        yield return new WaitForSeconds(1f);
        invince = false;
    }
}
