using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topdowncam : MonoBehaviour
{
    public Transform player;

    public float smooth = 0.3f;

    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 pos = player.position;
        pos.x += offset.x;
        pos.z += offset.z;
        pos.y += offset.y;


        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }
}
