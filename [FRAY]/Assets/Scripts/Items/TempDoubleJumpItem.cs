using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

public class TempDoubleJumpItem : MonoBehaviour
{
    public bool isCollected;
    public float itemDuration;
    MeshRenderer mr;
    BoxCollider bx;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        bx = GetComponent<BoxCollider>();

    }

    private void Update()
    {
        deleteItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            OtherGlobalVar.isjumpingtracker = true;
            Invoke("setGlobalJumpingVarToFalse", itemDuration);

        }
    }

    void setGlobalJumpingVarToFalse()
    {
        OtherGlobalVar.isjumpingtracker = false;
        isCollected = false;
        Destroy(gameObject);
    }

    void deleteItem()
    {
        if (isCollected == true)
        {
            mr.enabled = false;
            bx.enabled = false;
        }
    }
}
