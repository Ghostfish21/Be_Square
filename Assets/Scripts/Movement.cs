using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collisions coll;
    private Animator animator;
    private Rigidbody rb;

    // Movement Stats
    [Header("Movement Stats")]
    [Range(1, 50)]
    public float speed = 10f;
    public float moveX;

    // Jump Stats
    [Header("Jump Stats")]
    [Range(1, 50)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.2f;
    private float coyoteTimer;
    public float jumpBufferTime = 0.01f;
    private float jumpBufferCounter;

    // Booleans
    [Header("Booleans")]
    public bool wallSliding;
    public bool walljumping;
    public bool canMove = true;
    public bool groundTouched;
    public bool facingRight = true;
    public bool dashing;
    public bool hasDashed;

    private void Awake()
    {
        coll = GetComponent<Collisions>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Freeze Z rotation
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        FlipController();
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(moveX, 0, 0); // 2D movement in a 3D space
        Run(direction);

        HandleJump();
        UpdateCoyoteTime();
        ApplyGravity();
    }

    private void Run(Vector3 direction)
    {
        if (!walljumping)
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, 0); // Keep Z velocity fixed
        }
    }

    private void HandleJump()
    {
        if (!coll.onWall || coll.onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;

                if (jumpBufferCounter > 0 && (coll.onGround || coyoteTimer > 0f))
                {
                    Jump(Vector3.up);
                    coyoteTimer = 0f; // Reset timer after jumping
                    jumpBufferCounter = 0f;
                }
            }
        }

        if (coll.onGround && !groundTouched)
        {
            FirstTouch();
            groundTouched = true;
            coyoteTimer = coyoteTime;
            jumpBufferCounter = 0f;
        }
        else if (!coll.onGround && groundTouched)
        {
            groundTouched = false;
        }
    }

    private void UpdateCoyoteTime()
    {
        if (!coll.onGround)
        {
            coyoteTimer -= Time.deltaTime;
            coyoteTimer = Mathf.Max(coyoteTimer, 0f); // Ensure it doesn't go negative
        }

        if (jumpBufferCounter > 0f)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void ApplyGravity()
    {
        if (!coll.onGround)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime;
            }
            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector3.up * (lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime;
            }
        }
        else
        {
            // Reset Y velocity when grounded to prevent falling
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0); // Keep X and Z velocity intact
            }
        }
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0f, facingRight ? 90f : -90f, 0f); // Adjust rotation based on direction
    }

    private void FlipController()
    {
        if ((rb.velocity.x > 0 && !facingRight) || (rb.velocity.x < 0 && facingRight))
        {
            FlipSprite();
        }
    }

    private void Jump(Vector3 dir)
    {
        animator.SetTrigger("Jump");
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f); // Reset Y velocity before jumping
        rb.velocity += dir * jumpVelocity; // Apply jump force
    }

    private void FirstTouch()
    {
        hasDashed = false; // Dash resets when player touches ground
        dashing = false; // Dashing reset
    }
}
