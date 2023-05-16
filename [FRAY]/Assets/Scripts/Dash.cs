using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField]
    private TrailRenderer tr;
    public ParticleSystem dashdust;

    public bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public Rigidbody rb;
    public Animator anim;

    [SerializeField]
    private float dashAcceleration = 10f;

    [SerializeField]
    private float dashMaxSpeed = 20f;

    [SerializeField]
    private AnimationCurve dashCurve;

    public Camera UIcam;


    // Update is called once per frame
    void Update()
    {
        /*if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        */
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("dashing?");
            StartCoroutine(StartDash());
            
        }
        animateDash();
    }

    private IEnumerator StartDash()
    {
        canDash = false;
        OtherGlobalVar.isdashingtracker = true;
        isDashing = true;
        tr.emitting = true;


        float elapsedTime = 0f;
        float currentSpeed = dashMaxSpeed; // Start with maximum speed instantly

        while (elapsedTime < dashingTime)
        {
            float progress = elapsedTime / dashingTime;
            float acceleration = 0f; // Set acceleration to zero for instant acceleration
            EnableUICam();
            rb.velocity = transform.forward * currentSpeed;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        DisableUICam();
        tr.emitting = false;
        OtherGlobalVar.isdashingtracker = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    private void animateDash()
    {
        if (isDashing == true)
        {
            anim.SetBool("isDashing", true);
        }

        if (isDashing == false)
        {
            anim.SetBool("isDashing", false);
        }
    }

    private void EnableUICam()
    {
        UIcam.enabled = true;
    }

    private void DisableUICam()
    {
        UIcam.enabled = false;
    }


}
