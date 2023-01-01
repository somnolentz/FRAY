using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    [Header("Refs")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;
    public Jump jmp;
    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTIme;
    private float climbTimer;

    private bool climbing;
    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;
    public KeyCode jumpKey = KeyCode.Space;
    public int climbJumps;
    private int climbJumpsLeft;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private Transform lastwall;
    private Vector3 lastWallNormal;
    public float minWalNormalAngleChange;
    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void StateMachine()
    {
        //climbing 1
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if (!climbing && climbTimer > 0) StartClimbing();

            //timer
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            if (climbTimer < 0) StopClimbing();
        }
        //2 exiting
        else if (exitingWall)
        {
            if (climbing) StopClimbing();

            if (exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
            if (exitWallTimer < 0) exitingWall = false;

        }
        //none
        else
        {
            if (climbing) StopClimbing();
        }
        if (wallFront && Input.GetKey(jumpKey) && climbJumpsLeft > 0) ClimbJump();
    }

    private void Update()
    {
       
        WallCheck();
        StateMachine();
        if (climbing && !exitingWall) ClimbingMovement();
    }
    void WallCheck()
    {
        bool newWall = frontWallHit.transform != lastwall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWalNormalAngleChange;
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
       
        //starting pos, radius of sphere cast, the forward orientation of where the player is facing, where is that info going to be stored, the length of the detection and the layer mask.
        wallLookAngle = Vector3.Angle (orientation.forward, - frontWallHit.normal);
        if ((wallFront && newWall) || jmp.onGround)
        {
            climbTimer = maxClimbTIme;
            climbJumpsLeft = climbJumps;
        }
    }
    private void StartClimbing()
    {
        Debug.Log("climbingsettotrue");
        climbing = true;

        lastwall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
    }
    private void ClimbingMovement()
    {
        Debug.Log("climbingmovement");
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }
    private void StopClimbing()
    {
        climbing = false;
    }
    public void ClimbJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }
}
