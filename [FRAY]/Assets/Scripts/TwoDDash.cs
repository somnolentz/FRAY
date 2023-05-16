using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDDash : MonoBehaviour
{
    public TrailRenderer tr;
    public Camera UIcam;
    public ParticleSystem dashdust;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public Rigidbody rb;
    public Animator anim;

    private bool canDash = true;
    private bool isDashing;

    private void Start()
    {
        DisableUICam();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(StartDash());
        }
        animateDash();
    }

    private IEnumerator StartDash()
    {
        canDash = false;
        isDashing = true;

        tr.emitting = true;
        EnableUICam();
        rb.AddForce(transform.right * dashingPower, ForceMode.VelocityChange);
        yield return new WaitForSecondsRealtime(dashingTime);
        anim.SetBool("isDashing", false);
        tr.emitting = false;
        DisableUICam();
        isDashing = false;
        yield return new WaitForSecondsRealtime(dashingCooldown);
        canDash = true;
    }

    private void animateDash()
    {
        anim.SetBool("isDashing", isDashing);
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

