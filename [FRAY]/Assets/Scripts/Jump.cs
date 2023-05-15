using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float Movespeed = 3.5f;
    public float Jumpforce = 7.0f;
    public float Fallmultiplier = 2.0f;
    private Rigidbody rb = null;

    public bool onGround = true;
    [SerializeField]
    private Animator anim;

    private void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        onGround = true;
    }



    private void Update()
    {
        /* if (DialogueManager.GetInstance().dialogueIsPlaying == true)
         {
             return;
         }
        */

        if (Input.GetKeyDown(KeyCode.Space) == true && onGround == true)
        {
            Debug.Log("registering spacebar");
           // OtherGlobalVar.timesjumped++;
            onGround = false;
            anim.SetBool("isJumping", true);
            anim.SetBool("OnGround", false);
            rb.AddForce(Vector3.up * Jumpforce, ForceMode.VelocityChange);

        }

        if (Input.GetKeyDown(KeyCode.Space) == true && OtherGlobalVar.doublejumpEnabled == true && OtherGlobalVar.timesjumped <= 1)
        {
            OtherGlobalVar.timesjumped++;
            onGround = false;
            anim.SetBool("isJumping", true);
            anim.SetBool("OnGround", false);
            rb.AddForce(Vector3.up * Jumpforce, ForceMode.VelocityChange);
        }

        if (onGround == true)
        {
            OtherGlobalVar.timesjumped = 0;
        }

    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ground")
        {
            OtherGlobalVar.isjumpingtracker = false;
            onGround = true;
            anim.SetBool("isJumping", false);
            anim.SetBool("OnGround", true);
            Debug.Log("is on the ground");
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "ground")
        {
            onGround = false;
            OtherGlobalVar.isjumpingtracker = true;
            anim.SetBool("isJumping", true);


        }
    }



    private void FixedUpdate()
    {
        if (rb.velocity.y < 0 && onGround == false)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * Fallmultiplier * Time.deltaTime;
            
        }
    }

   

}


