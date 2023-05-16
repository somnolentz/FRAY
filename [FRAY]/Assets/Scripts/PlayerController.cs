using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private float horizInput;
    private float vertInput;

    public ParticleSystem dust;

    [SerializeField]
    private LayerMask WhatIsGround;

    [SerializeField]
    private AnimationCurve animCurve;

    [SerializeField]
    private float Time;

    public bool switchtoTwoDMode;

    public WallClimbing wcscript;

    [SerializeField]
    private Animator anim;

    public float ThreeDMaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        anim.SetFloat("speed", Mathf.Abs(horizInput));

        if (rb.velocity.magnitude > ThreeDMaxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ThreeDMaxSpeed);
        }

        if (horizInput == 0 && vertInput == 0)
        {
            anim.SetBool("isIdle", true);
            // Apply opposing force to slow down the movement
            rb.AddForce(-rb.velocity * 5f);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }

    }


    private void FixedUpdate()
    {
        if (wcscript.exitingWall) return;

        anim.SetFloat("speed", Mathf.Abs(horizInput));

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;

        Vector3 movementDirection = cameraForward.normalized * vertInput + cameraRight.normalized * horizInput;
        rb.AddForce(movementDirection.normalized * speed);

        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("isIdle", false);
            transform.forward = movementDirection;

        }
        else
        {
            anim.SetBool("isIdle", true);
            // Apply damping or drag to slow down the movement
            //rb.drag = 0.4f;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetBool("FacingRight", true);
            anim.SetBool("FacingLeft", false);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetBool("FacingRight", false);
            anim.SetBool("FacingLeft", true);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            anim.SetBool("FacingBack", true);
            anim.SetBool("FacingUp", false);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            anim.SetBool("FacingFacing", false);
            anim.SetBool("FacingLeft", true);
        }
    }

}