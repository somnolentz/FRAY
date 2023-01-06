using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwoDDash : MonoBehaviour
{

    [SerializeField]
    private TrailRenderer tr;
    public Camera UIcam;
    public ParticleSystem dashdust;

    public bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public Rigidbody rb;

    [SerializeField]
    Animator anim;



    private void Start()
    {
        DisableUICam();

    }
    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            Debug.Log("dashing");
            StartCoroutine(StartDash());
            CreateDust();
            
          

        }
        animateDash();
    }
    
    void CreateDust()
    {
        dashdust.Play();
    }

    void EnableUICam()
    {
        UIcam.enabled = true;
    }

    void DisableUICam()
    {
        UIcam.enabled = false;

    }

    private IEnumerator StartDash()
    {
        canDash = false;
        isDashing = true;

        tr.emitting = true;
        
        EnableUICam();
        rb.velocity += transform.right * dashingPower * Time.deltaTime;
        yield return new WaitForSeconds(dashingTime);
        anim.SetBool("isDashing", false);
        tr.emitting = false;
        DisableUICam();
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
