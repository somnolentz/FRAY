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
            CreateDust();
        }
        animateDash();
    }
    void CreateDust()
    {
        dashdust.Play();
    }
    private IEnumerator StartDash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        rb.AddRelativeForce(Vector3.forward * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
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




}
