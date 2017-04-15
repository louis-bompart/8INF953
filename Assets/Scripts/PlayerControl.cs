using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject corpse;
    public GameObject lastWP;
    private float timeSinceLastSuicide;
    public float speed;
    private Rigidbody rb;
    public GroundCollision groundCollider;
    public bool grounded;
    public float maxSpeed;
    public float horizontalAcceleration;
    public float verticalAcceleration;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        grounded = true;
        timeSinceLastSuicide = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.z;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(Vector3.forward * horizontalAcceleration);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(Vector3.back * horizontalAcceleration);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            Jump();
        }
        if (Input.GetAxis("Vertical") < 0 && Time.time-timeSinceLastSuicide>2)
        {
            Suicide();
        }
        LimitSpeed();
    }

    private void LimitSpeed()
    {
        if (Mathf.Abs(rb.velocity.z) > maxSpeed)
        {
            rb.velocity = rb.velocity.y * Vector3.up + Vector3.ClampMagnitude(rb.velocity.z * Vector3.forward,Mathf.Sqrt(maxSpeed));
        }
    }

    private void Jump()
    {
        //Add an instant force of X
        if (groundCollider.grounded)
        {
            rb.AddForce(Vector3.up * verticalAcceleration);

        }
    }

    private void Suicide()
    {
        Instantiate<GameObject>(corpse, transform.position, transform.rotation, transform.parent);
        transform.position=lastWP.transform.position;
        rb.velocity = Vector3.zero;
        timeSinceLastSuicide = Time.time;
        //Instantiate a deadPlayer on the player coordinate
        //Move player to its start wp and set speed and acceleration to 0

    }
}
