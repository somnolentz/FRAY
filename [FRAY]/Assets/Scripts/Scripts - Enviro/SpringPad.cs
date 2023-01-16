using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    public float bounceForce;
    

    private Vector3 springFacing;

    public Rigidbody rb;

    [SerializeField]
    private bool playerInBouncePos = false;



    // Update is called once per frame
    void Update()
    {
        if (playerInBouncePos == true)
        {

            //rb.AddRelativeForce(Vector3.up * bounceForce, 0f);
            rb.AddForce(Vector3.up * bounceForce, 0f);

            playerInBouncePos = false;
        }

        springFacing = Vector3.forward;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInBouncePos = true;
        }

    }



}
