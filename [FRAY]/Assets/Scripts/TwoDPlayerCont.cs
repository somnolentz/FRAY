using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlayerCont : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float horizInput;
    public float vertInput;

    public ParticleSystem dust;


    [SerializeField]
    private LayerMask WhatIsGround;

    [SerializeField]
    private AnimationCurve animCurve;


    [SerializeField]
    private float Time;

    public Transform facing; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //SurfaceAlignment();
        horizInput = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        
    }

    private void FixedUpdate()
    {

        rb.AddForce(new Vector3(1, 0, 0) * speed * horizInput);

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Debug.Log("facing right");
            transform.localRotation = Quaternion.Euler(0, 0, 0);



        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Debug.Log("facing left");
            transform.localRotation = Quaternion.Euler(0, 180, 0);


        }

    }

    private void SurfaceAlignment()
    {

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.Euler(0, 0, 0);
        if (Physics.Raycast(ray, out info, WhatIsGround))
        {
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(Time));
            transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
        }
    }


    void CreateDust()
    {
        dust.Play();
    }


}
