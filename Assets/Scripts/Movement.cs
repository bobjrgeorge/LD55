using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Movement : MonoBehaviour 
{
    public Rigidbody2D rb;
    public float speed;
    float multiplier;
    private Vector3 move;
    public InputSystem input;
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
    public Animator animator;
    bool isFacingRight = true;

    // Start is called before the first frame update

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.AddForce(-orientation.up * 800 * Time.deltaTime);
        grounded = Physics2D.OverlapCircle(Groundcheck.position, 0.2f, Ground);
        Jump();

        groundRemeber -= Time.deltaTime;
        if(grounded) 
        {
            groundRemeber = groundRemeberTime;
            multiplier = 1f;
            animator.SetBool("Jump", false);
        }
        if(!grounded)
        {
            animator.SetBool("Jump", true);
            multiplier = 0.75f;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
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
        rb.velocity = new Vector2(move.x * speed * multiplier * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move.x));
        if (move.x == 0 && grounded)
        {
            rb.drag = 6.54678294387f;
        }
        else
        {
            rb.drag = 1;
        }

        if (!isFacingRight && move.x > 0f)
        {
            Flip();
        }
        else if (isFacingRight && move.x < 0f)
        {
            Flip();
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
