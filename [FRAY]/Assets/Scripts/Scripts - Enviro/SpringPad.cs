using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    public float bounceForce;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add force to player Rigidbody component to bounce it
            other.GetComponent<Rigidbody>().AddForce(transform.up * bounceForce, ForceMode.Impulse);
        }
    }



}
