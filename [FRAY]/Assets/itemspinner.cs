using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemspinner : MonoBehaviour
{
    public float floatHeight = 0.5f; // How high the item floats
    public float floatSpeed = 0.5f; // How fast the item floats
    public float spinSpeed = 180f; // How fast the item spins

    private float startY; // Starting y position of the item

    private void Start()
    {
        startY = transform.position.y; // Record the starting y position of the item
    }

    private void Update()
    {
        // Make the item float up and down
        float newY = startY + (Mathf.Sin(Time.time * floatSpeed) * floatHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Make the item spin around its y-axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
