using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float ThreeModeJumpForce;
    public float ThreeModeFall;
    public float TwoModeJumpForce;
    public float TwoModeFall;

    public float jumpForce = 7.0f;
    public float fallMultiplier = 2.0f;
    public float lowJumpMultiplier = 1.5f;

    private Rigidbody rb;
    public Animator anim;
    public bool onGround = true;
    private int timesJumped = 0;
    private bool isJumpingTracker = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        onGround = true;
        timesJumped = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                JumpAction();
            }
            else if (OtherGlobalVar.doublejumpEnabled && timesJumped <= 2)
            {
                timesJumped++;
                JumpAction();
            }
        }

        if (OtherGlobalVar.isIn2d)
        {
            jumpForce = TwoModeJumpForce;
            fallMultiplier = TwoModeFall;
        }
        else if (OtherGlobalVar.isIn3d)
        {
            jumpForce = ThreeModeJumpForce;
            fallMultiplier = ThreeModeFall;
        }
    }

    private void JumpAction()
    {
        onGround = false;
        isJumpingTracker = true;
        anim.SetBool("isJumping", true);
        anim.SetBool("OnGround", false);
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            onGround = true;
            isJumpingTracker = false;
            timesJumped = 0;
            anim.SetBool("isJumping", false);
            anim.SetBool("OnGround", true);
            Debug.Log("Is on the ground");
        }
    }
    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) + Physics.gravity.y) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) + Physics.gravity.y) * Time.fixedDeltaTime;
        }
    }


}