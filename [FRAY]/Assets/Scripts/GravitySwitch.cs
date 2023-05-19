using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public float targetGravity = -9.81f; // The target gravity to switch to
    private float defaultGravity; // The default gravity of the scene
    private bool isUpsideDown = false; // Whether the gravity is currently upside down
    private Vector3 upsideDownRotation = new Vector3(180f, 0f, 0f); // The rotation to apply when upside down
    private Vector3 normalRotation = Vector3.zero; // The normal rotation of the player
    public SpriteRenderer sr;

    private void Start()
    {
        defaultGravity = Physics.gravity.y;
        normalRotation = transform.rotation.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.gravity = new Vector3(0f, targetGravity, 0f);
            isUpsideDown = true;
            other.transform.Rotate(upsideDownRotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.gravity = new Vector3(0f, defaultGravity, 0f);
            isUpsideDown = false;
            other.transform.rotation = Quaternion.Euler(normalRotation);
        }
    }

    private void Update()
    {
        if (isUpsideDown)
        {
            sr.flipY = true;
        }
        else
        {
            sr.flipY = false;
        }
    }
}



