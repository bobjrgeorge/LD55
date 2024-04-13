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

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0, .5f);
        
    }
    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
         if(!p.error)
         {
            path = p;
            currentWaypoint = 0;
         }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
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
        if(distance < NextWaypointDistace)
        {
            currentWaypoint++;
        }

        if(rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
}
