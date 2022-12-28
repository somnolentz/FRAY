using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mxspeed : MonoBehaviour
{
    public float maxSpeed = 200f;//Replace with your max speed
    public Rigidbody rb;
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
