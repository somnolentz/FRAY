using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlayerCont : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private float horizInput;
    private bool isFacingRight = true;

    public ParticleSystem dust;
    public Animator anim;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(horizInput));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        // Move the player horizontally
        rb.velocity = new Vector3(horizInput * speed, rb.velocity.y, 0);

        // Flip the player's sprite and animation based on movement direction
        if (horizInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizInput < 0 && isFacingRight)
        {
            Flip();
        }

        // Set the animation bools for facing direction
        anim.SetBool("FacingRight", isFacingRight);
        anim.SetBool("FacingLeft", !isFacingRight);

        if (isJumping && rb.velocity.y <= 0)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    void CreateDust()
    {
        dust.Play();
    }
}
