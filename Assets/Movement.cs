using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    float multiplier;
    private Vector3 move;
    private InputSystem input;
    public Transform orientation;
    public Transform Groundcheck;
    public LayerMask Ground;
    private bool grounded;
    public float JumpFroce;
    public Transform InRangeCheck;
    public float JumprememberTime = 1f;
    float jumpRemeber = 0;
    float groundRemeber;
    float groundRemeberTime = 0.2f;
    // Start is called before the first frame update

    private void Awake()
    {
        input = InputSystem.Instance;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        grounded = Physics2D.OverlapCircle(Groundcheck.position, 0.2f, Ground);
        Jump();

        groundRemeber -= Time.deltaTime;
        if(grounded) 
        {
            groundRemeber = groundRemeberTime;
            multiplier = 1f;
        }
        if(!grounded)
        {
            multiplier = 0.5f;
        }

    }
    private bool InRange()
    {
        return Physics2D.OverlapCircle(InRangeCheck.position, 0.25f, Ground);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        Vector2 movement = input.GetPlayerMovement();
        move.Set(movement.x, 0, movement.y);
        rb.AddForce(orientation.right * move.x * speed * multiplier * Time.deltaTime );
        if (move.x == 0 && grounded)
        {
            rb.drag = 6.54678294387f;
        }
        else
        {
            rb.drag = 1;
        }
        if(rb.velocity.x >= 5)
        {
            float cappedVel = rb.velocity.x;
            cappedVel = 5;
        }
    }

    void Jump()
    {
        jumpRemeber -= Time.deltaTime;
        if (input.PlayerJumped())
        {
            jumpRemeber = JumprememberTime;
        }
        if(jumpRemeber > 0 && groundRemeber > 0)
        {
            jumpRemeber = 0;
            groundRemeber = 0;
            rb.velocity = new Vector2(rb.velocity.x, JumpFroce);
        }
    }
}
