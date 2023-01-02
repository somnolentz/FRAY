using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlayerCont : MonoBehaviour
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

    public Transform facing;

    public Animator anim;

    public float TwoDMaxSpeed;


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

        if (rb.velocity.magnitude > TwoDMaxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, TwoDMaxSpeed);
        }
        
    }

    private void FixedUpdate()
    {

        rb.AddForce(new Vector3(1, 0, 0) * speed * horizInput);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //Debug.Log("facing right");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("FacingRight", true);
           

        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //Debug.Log("facing left");
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("FacingRight", false);


        }
       ;
    }

    


    void CreateDust()
    {
        dust.Play();
    }


}
