using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float horizInput;
    public float vertInput;

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
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        //SurfaceAlignment();
        if (rb.velocity.magnitude > ThreeDMaxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ThreeDMaxSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (wcscript.exitingWall) return;


        rb.AddForce(new Vector3(1, 0, 0) * speed * horizInput);
        rb.AddForce(new Vector3(0, 0, 1) * speed * vertInput);

        Vector3 movementDirection = new Vector3(horizInput, 0, vertInput);
        //transform.Translate(movementDirection * speed, Space.World);
        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("isIdle", false);
            transform.forward = movementDirection;
            CreateDust();
        }
        anim.SetBool("isIdle", true);

    }

    private void SurfaceAlignment()
    {

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.Euler(0, 0, 0);
        if (Physics.Raycast(ray, out info, WhatIsGround))
        {
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(Time));
            transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
        }
    }


    void CreateDust()
    {
        dust.Play();
    }

}
