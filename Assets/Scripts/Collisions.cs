using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.2f;
    public Transform bottomOffset, rightOffset, leftOffset; // Change to Vector3 for 3D
    public Color debugCollisionColor = Color.red;

    public void Update()
    {
        // Check if on ground
        onGround = Physics.CheckSphere(transform.position + bottomOffset.localPosition, collisionRadius, groundLayer);
        if (onGround) Movement.inst.rb.drag = 3;
        else Movement.inst.rb.drag = 0;

        // Check for walls
        onWall = Physics.CheckSphere(transform.position + rightOffset.localPosition, collisionRadius, groundLayer)
            || Physics.CheckSphere(transform.position + leftOffset.localPosition, collisionRadius, groundLayer);

        onRightWall = Physics.CheckSphere(transform.position + rightOffset.localPosition, collisionRadius, groundLayer);
        onLeftWall = Physics.CheckSphere(transform.position + leftOffset.localPosition, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : (onLeftWall ? 1 : 0);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        // Draw spheres for the collision checks
        Gizmos.DrawWireSphere(transform.position + bottomOffset.localPosition, collisionRadius);
        Gizmos.DrawWireSphere(transform.position + rightOffset.localPosition, collisionRadius);
        Gizmos.DrawWireSphere(transform.position + leftOffset.localPosition, collisionRadius);
    }
}
