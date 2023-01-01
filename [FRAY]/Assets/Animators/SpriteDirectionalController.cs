using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    [SerializeField]
    float backAngle = 65f;
    [SerializeField]
    float sideAngle = 155f;
    [SerializeField]
    Transform mainTransform;
    [SerializeField]
    Animator animator;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    private float animspeed;
    private void Update()
    {
        //animspeed = animator.GetFloat("speed");
        //if (animspeed > 0.01)
        //{
        //    animator.SetBool("isIdle", false);
        //}
        //else
        //{
        //    animator.SetBool("isIdle",false);
        //}
    }
    private void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);


            float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);
        Vector2 animationDirection = new Vector2(0, -1f);
        float angle = Mathf.Abs(signedAngle);

       

        if (angle < backAngle)
        {
            //back idle
            animationDirection = new Vector2(0f, -1f);
            

            //if (animspeed > 0.01)
            //{
            //    animator.Play("player_moveback");
            //}
        }
        else if (angle < sideAngle)
        {
            
            if (signedAngle < 0)
            {
                //right idle
                animationDirection = new Vector2(1f, 0f);

                //if (animspeed > 0.01)
                //{
                //    animator.Play("player_moveright");
                //}
            }
            else
            {
                //left idle 
                animationDirection = new Vector2(-1f, 0f);

                //if (animspeed > 0.01)
                //{
                //    Debug.Log("play left run");
                //    animator.Play("player_moveleft");
                //}
            }

        }
        else
        {
            //front anim idle
            animationDirection = new Vector2(0f, 1f);

            ////if (animspeed > 0.01)
            ////{
            ////    animator.Play("player_movefront");
            ////}
        }

        animator.SetFloat("moveX", animationDirection.x);

        animator.SetFloat("moveY", animationDirection.y);

    }
}
