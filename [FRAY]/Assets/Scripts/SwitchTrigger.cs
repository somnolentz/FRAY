using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    private bool TwoDcam = true;
    public bool TwoDTriggerSwitch;
    public bool ThreeDTriggerSwitch;
    private Animator anim;
  

    //private void start()
    //{
    //    anim = GetComponent<Animator>();

    //}


    //private void switchCam()
    //{
    //    if (TwoDcam)
    //    {
    //        anim.Play("3D cam");
    //    }
    //    else
    //    {
    //        anim.Play("2D cam");
    //    }
    //    TwoDcam = !TwoDcam;

    //}



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
            

        }
        //if (TwoDTriggerSwitch || ThreeDTriggerSwitch == true)
        //{
        //    Debug.Log("camswitch");
        //    switchCam();
        //}
        




        

    }
}
