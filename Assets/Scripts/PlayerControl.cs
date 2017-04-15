using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject corpse;

    private Rigidbody rb;

    public GroundCollision groundCollider;

    public float speed;
    public float maxSpeed;

    public Vector3 accelerationDirection;
    public float accelerationMagnitude;

    public float jumpAcceleration;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        accelerationDirection = Vector3.forward;
    }
    private void Update()
    {
        speed = rb.velocity.magnitude;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (groundCollider.grounded)
        {
            rb.AddForce(accelerationDirection * accelerationMagnitude);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public void Jump()
    {
        //Add an instant force of X
        if (groundCollider.grounded)
        {
            rb.AddForce(Vector3.up * jumpAcceleration);
        }
    }
}
