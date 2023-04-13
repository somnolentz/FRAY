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
        anim.SetFloat("speed", Mathf.Abs(horizInput));

        //SurfaceAlignment();
        if (rb.velocity.magnitude > ThreeDMaxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ThreeDMaxSpeed);
        }
    }

    private void FixedUpdate()
    {
       /* if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
       */
        if (wcscript.exitingWall) return;


        anim.SetFloat("speed", Mathf.Abs(horizInput));

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;

        Vector3 movementDirection = cameraForward.normalized * vertInput + cameraRight.normalized * horizInput;
        rb.AddForce(movementDirection * speed);

        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("isIdle", false);
            transform.forward = movementDirection;
            CreateDust();
        }
        anim.SetBool("isIdle", true);




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

    //private void SurfaceAlignment()
    //{

    //    Ray ray = new Ray(transform.position, -transform.up);
    //    RaycastHit info = new RaycastHit();
    //    Quaternion RotationRef = Quaternion.Euler(0, 0, 0);
    //    if (Physics.Raycast(ray, out info, WhatIsGround))
    //    {
    //        RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(Time));
    //        transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
    //    }
    //}


    void CreateDust()
    {
        dust.Play();
    }

}
