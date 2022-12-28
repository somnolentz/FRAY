using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraTwoThreeSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CameraSwitcher.ActiveCamera != cam) CameraSwitcher.SwitchCamera(cam);
        }
    }
}
