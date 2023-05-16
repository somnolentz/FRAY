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

    [SerializeField]
    private SpriteDirectionalController sdc;

    private Jump jumpscript;

    [SerializeField]
    private float threeDjumpForce;
    [SerializeField]
    private float threeDfallSpeed;
    [SerializeField]
    private float TwoDJumpForce;
    [SerializeField]
    private float TwoDfallSpeed;



    private void Start()
    {
        jumpscript = GetComponent<Jump>();
    }
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "2DTrigger")
        {
            OtherGlobalVar.isIn2d = true;
            OtherGlobalVar.isIn3d = false;
            //switchCam();
            //jumpscript.Jumpforce = TwoDJumpForce;
            //jumpscript.Fallmultiplier = TwoDfallSpeed;

            
            TwoDTriggerSwitch = true;
            ThreeDTriggerSwitch = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Dash>().enabled = false;

            GetComponent<TwoDPlayerCont>().enabled = true;
            GetComponent<TwoDDash>().enabled = true;
            anim.SetBool("is3D", false);
            GetComponent<WallClimbing>().enabled = false;
            GetComponent<TwoDWallClimb>().enabled = true;


        }
        if (collider.tag == "3DTrigger")
        {
            OtherGlobalVar.isIn2d = false;
            OtherGlobalVar.isIn3d = true;

            //switchCam();
            //variables to change 
            //jumpscript.Jumpforce = threeDjumpForce;
            //jumpscript.Fallmultiplier = threeDfallSpeed;

            TwoDTriggerSwitch = false;
            ThreeDTriggerSwitch = true;
            GetComponent<TwoDPlayerCont>().enabled = false;
            GetComponent<TwoDDash>().enabled = false;

            GetComponent<PlayerController>().enabled = true;
            GetComponent<Dash>().enabled = true;
            anim.SetBool("is3D", true);

            //GetComponent<SpriteDirectionalController>().enabled = true;
            GetComponent<WallClimbing>().enabled = true;
            GetComponent<TwoDWallClimb>().enabled = false;

            jumpscript.onGround = true;

        }
     




        

    }
}
