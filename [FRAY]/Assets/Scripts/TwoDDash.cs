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
        //rb.AddForce(transform.forward * dashingPower, 0f);
        // rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //rb.velocity += transform.forward * Time.deltaTime * dashingPower;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        DisableUICam();
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;



    }
}
