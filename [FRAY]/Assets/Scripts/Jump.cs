using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float Movespeed = 3.5f;
    public float Jumpforce = 7.0f;
    public float Fallmultiplier = 2.0f;
    private Rigidbody rb = null;

    public bool onGround = true;
    
   

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }



    private void Update()
    {
        //this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Movespeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) == true && onGround == true)
        {
            onGround = false;
            rb.AddForce(Vector3.up * Jumpforce, ForceMode.VelocityChange);
        }

    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ground")
        {
            onGround = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "ground")
        {
            onGround = false;
        }
    }



    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Fallmultiplier * Time.deltaTime;
        }
    }

   

}


