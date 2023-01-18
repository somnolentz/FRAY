using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDWallClimb : MonoBehaviour
{
    public float slidingSpeed;
    public float slidingTime;
    public bool abletowallslide;
    public bool iswallsliding;

    public float wallDetectionRadius = 0.2f;
    public float wallDetectionLength;
    public LayerMask whatIsWall;
    private bool wallFront;
    private RaycastHit frontwallHit;
    public bool onWall;
    public float wallSlowDown;

    public TwoDPlayerCont playercontroller;
    private Jump jumpscript;

    private Rigidbody rb;

    private float currentVelocity;
    private float desiredVelocity;



    [SerializeField]
    public float climbJumpUpForce;
    public float climbJumpBackForce;
    public int climbJumpsLeft;
    public int climbJumps;
    public bool exitingWall;
    public KeyCode jumpKey = KeyCode.Space;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpscript = GetComponent<Jump>();

        

    }

    // Update is called once per frame
    void Update()
    {
        wallCheck();
        wallSlide();
        wallJump();
        if (jumpscript.onGround == true)
        {
            climbJumpsLeft = climbJumps;
            anim.SetBool("wallSlide", false);
        }

       
    }

    private void wallCheck()
    {
       wallFront = Physics.SphereCast(transform.position, wallDetectionRadius, transform.right, out frontwallHit, wallDetectionLength, whatIsWall);
        if (wallFront)
        {
            abletowallslide = true;
            Debug.Log("wall is hit");
        }
      
        
    }

    private void wallSlide()
    {
        if (abletowallslide == true && jumpscript.onGround == false)
        {
            
            onWall = true;
            iswallsliding = true;
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * slidingSpeed);
            Debug.Log("wallsliding");
            //playercontroller.enabled = false;
            //replace enable and disable with lerp between current velocity desired velocity and back
            anim.SetBool("wallSlide", true);


        }
        if (jumpscript.onGround == true)
        {
            //playercontroller.enabled = true;
            onWall = false;
            abletowallslide = false;
            abletowallslide = false;
            anim.SetBool("wallSlide", false);


        }


    }

    private void wallJump()
    {
        if (wallFront && onWall == true && jumpscript.onGround == false && iswallsliding == true && climbJumpsLeft > 0 && Input.GetKey(jumpKey))
        {
            Debug.Log("walljumping");
            //playercontroller.enabled = true;
            Vector3 forceToApply = transform.up * climbJumpUpForce + frontwallHit.normal * climbJumpBackForce;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(forceToApply, ForceMode.Impulse);
            //climbJumpsLeft--;
            anim.SetBool("wallSlide", true);
        }
    }
    
    

}
