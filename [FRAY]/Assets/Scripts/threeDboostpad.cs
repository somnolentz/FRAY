using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeDboostpad : MonoBehaviour
{
    public float speedBoost;

    public SwitchTrigger sw;

    public Rigidbody rb;

    [SerializeField]
    public bool playerInSpeedPos = false;


    private void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (playerInSpeedPos == true)
        {

            //rb.AddRelativeForce(Vector3.up * bounceForce, 0f);
           rb.AddForce(Vector3.forward * speedBoost, 0f);
           


        }
        playerInSpeedPos = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSpeedPos = true;
        }

    }
}
