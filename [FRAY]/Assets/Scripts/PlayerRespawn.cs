using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> checkpoints = new List<GameObject>();

    private GameObject currentCheckpoint;

    void Start()
    {
        // Set the first checkpoint as the starting point
        currentCheckpoint = checkpoints[0];
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player passes through a checkpoint, set it as the new checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
        }

        if (other.CompareTag("HarmfulObject"))
        {
            Respawn();
        }

        
    }

    public void Respawn()
    {
        // Respawn the player at the current checkpoint
        player.transform.position = currentCheckpoint.transform.position;
    }


}
