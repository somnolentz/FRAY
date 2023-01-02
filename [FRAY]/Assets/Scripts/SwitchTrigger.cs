using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    private bool TwoDcam = true;
    public bool TwoDTriggerSwitch;
    public bool ThreeDTriggerSwitch;

    [SerializeField]
    private Animator anim;
 
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "2DTrigger")
        {
            //switchCam();
            TwoDTriggerSwitch = true;
            ThreeDTriggerSwitch = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Dash>().enabled = false;

            GetComponent<TwoDPlayerCont>().enabled = true;
            GetComponent<TwoDDash>().enabled = true;
            anim.SetBool("is3D", false);

            GetComponent<SpriteDirectionalController>().enabled = false;

            GetComponent<WallClimbing>().enabled = false;
            GetComponent<TwoDWallClimb>().enabled = true;


        }
        if (collider.tag == "3DTrigger")
        {
            //switchCam();
            TwoDTriggerSwitch = false;
            ThreeDTriggerSwitch = true;
            GetComponent<TwoDPlayerCont>().enabled = false;
            GetComponent<TwoDDash>().enabled = false;

            GetComponent<PlayerController>().enabled = true;
            GetComponent<Dash>().enabled = true;
            anim.SetBool("is3D", true);
            GetComponent<SpriteDirectionalController>().enabled = true;
            GetComponent<WallClimbing>().enabled = true;
            GetComponent<TwoDWallClimb>().enabled = false;

        }
     




        

    }
}
