using System;
using DefaultNamespace;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SeagullDK;
using UnityEngine;

public class Movement : MonoBehaviour {
    private static Movement m;
    public static Movement inst => m;
    
    private Collisions coll;
    private Animator animator;
    public AudioManager audioSearch;
    public Rigidbody rb { get; private set; }

    // Movement Stats
    [Header("Movement Stats")] [Range(1, 50)]
    public float speed = 10f;

    public float moveX;

    // Jump Stats
    [Header("Jump Stats")] [Range(1, 50)] public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.2f;
    private float coyoteTimer;
    public float jumpBufferTime = 0.01f;
    private float jumpBufferCounter;

    // Booleans
    [Header("Booleans")] public bool wallSliding;
    public bool walljumping;
    public bool canMove = true;
    public bool groundTouched;
    public bool? facingRight = true;
    public bool dashing;
    public bool hasDashed;

    private void Awake() {
        m = this;
        coll = GetComponent<Collisions>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSearch = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate() {
        Vector3 upVelo = rb.velocity.time(rb.transform.up);
        
        animator.SetFloat("xVelocity", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("yVelocity", upVelo.x + upVelo.y + upVelo.z);
    }

    private void Update() {
        Run();
        Flip();
        //ApplyGravity();
        HandleJump();
    }

    private void Run() {
        if (!walljumping) {
            if (!canMove) return;
            if (!coll.onGround) return;
            moveX = Input.GetAxis("Horizontal");
            rb.AddForce(transform.forward * Mathf.Abs(moveX) * Time.deltaTime * 100 * speed);
        }
    }
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTween;

    private void Flip() {
        if (facingRight == null) {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                facingRight = true;
                if (rotateTween != null && rotateTween.IsActive() && rotateTween.IsPlaying()) rotateTween.Kill();
                rotateTween = rb.transform.DORotate(facing, 0.25f);
            } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                facingRight = false;
                if (rotateTween != null && rotateTween.IsActive() && rotateTween.IsPlaying()) rotateTween.Kill();
                rotateTween = rb.transform.DORotate(facing.time((-1f).ff_(1)), 0.25f);
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (facingRight.Value) return;
            facingRight = true;
            if (rotateTween != null && rotateTween.IsActive() && rotateTween.IsPlaying()) rotateTween.Kill();
            rotateTween = rb.transform.DORotate(facing, 0.25f);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (!facingRight.Value) return;
            facingRight = false;
            if (rotateTween != null && rotateTween.IsActive() && rotateTween.IsPlaying()) rotateTween.Kill();
            rotateTween = rb.transform.DORotate(facing.time((-1f).ff_(1)), 0.25f);
        }
    }

    private void HandleJump() {
        if (!canMove) return;
        
        if (!coll.onWall || coll.onGround) {
            if (Input.GetButtonDown("Jump")) {
                jumpBufferCounter = jumpBufferTime;

                if (jumpBufferCounter > 0 && (coll.onGround || coyoteTimer > 0f)) {
                    Jump(rb.transform.up);
                    coyoteTimer = 0f; // Reset timer after jumping
                    jumpBufferCounter = 0f;
                }
            }
        }

        if (coll.onGround && !groundTouched) {
            FirstTouch();
            groundTouched = true;
            coyoteTimer = coyoteTime;
            jumpBufferCounter = 0f;
        }
        else if (!coll.onGround && groundTouched) {
            groundTouched = false;
        }
    }

    private void UpdateCoyoteTime() {
        if (!coll.onGround) {
            coyoteTimer -= Time.deltaTime;
            coyoteTimer = Mathf.Max(coyoteTimer, 0f); // Ensure it doesn't go negative
        }

        if (jumpBufferCounter > 0f) {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void ApplyGravity() {
        if (!coll.onGround) {
            if (rb.velocity.y < 0) {
                rb.velocity += Vector3.up * (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime;
            }
            else if (rb.velocity.y > 0) {
                rb.velocity += Vector3.up * (lowJumpMultiplier - 1) * Physics.gravity.y * Time.deltaTime;
            }
        }
        else {
            // Reset Y velocity when grounded to prevent falling
            if (rb.velocity.y < 0) {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0); // Keep X and Z velocity intact
            }
        }
    }

    private void Jump(Vector3 dir) {

        animator.SetTrigger("Jump");
        AudioManager.instance.PlaySFX(audioSearch.jumpSFX);
        Vector3 resetVec = 1f.fff() - dir;
        
        rb.velocity = rb.velocity.time(resetVec); // Reset Y velocity before jumping
        rb.velocity += dir * jumpVelocity; // Apply jump force
    }

    private void FirstTouch() {
        hasDashed = false; // Dash resets when player touches ground
        dashing = false; // Dashing reset
        AudioManager.instance.PlaySFX(audioSearch.landSFX);
    }
    
    internal Vector3 facing = new(0, 90, 0);
    public StageMover middleLayer;
    public StageMover innerLayer;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Portal")) return;
        Portal p = other.gameObject.getComponent<Portal>();

        facingRight = null;
        CameraRotator.inst.playTween(transform);
        middleLayer.playTween();
        innerLayer.playTween();
        rb.MovePosition(p.teleportPosition.position);
        Physics.gravity = p.newGravity.getDelta();
        if (rotateTween != null && rotateTween.IsActive() && rotateTween.IsPlaying()) rotateTween.Kill();
        facing = p.newGravity.getFacing();
        rotateTween = transform.DORotate(p.newGravity.getFacing(), 0.65f);
    }
}