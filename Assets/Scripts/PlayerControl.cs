using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject corpse;

    private Rigidbody rb;

    public GroundCollision groundCollider;

    public bool isStopped;
    public float speed;
    public float maxSpeed;

    public Vector3 accelerationDirection;
    public float accelerationMagnitude;

    public float jumpAcceleration;
    public Lever leverInRange;

    public bool isOnCooldown;
    private float coolDownEnd;
    public float cooldownDuration;

	public GameObject gameState;

	public void Awake(){
		gameState = GameObject.Find("GameState");
	}


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        accelerationDirection = Vector3.forward;
        isOnCooldown = false;
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
        if (isOnCooldown && coolDownEnd < Time.time)
            isOnCooldown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (groundCollider.grounded && !isStopped)
        {
            rb.AddForce(accelerationDirection * accelerationMagnitude);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    internal void SetCooldown(bool input)
    {
        isOnCooldown = input;
        if (input)
            coolDownEnd = Time.time + cooldownDuration;
    }

    public bool Jump()
    {
        //Add an instant force of X
        if (groundCollider.grounded)
        {
            rb.AddForce(Vector3.up * jumpAcceleration, ForceMode.Impulse);
            return true;
        }
        return false;
    }

    public void Stop()
    {
        isStopped = true;
        float velocityY = rb.velocity.y;
        rb.velocity = Vector3.zero + Vector3.up * velocityY;
    }

    public void Reverse()
    {
        accelerationDirection *= -1;
    }

    public void Die(Transform transform = null)
    {
		GameState gs = gameState.GetComponent<GameState>();
		gs.nbDeath++;
        if (transform == null)
            transform = gameObject.transform;
        Instantiate<GameObject>(corpse, transform.position, transform.rotation, transform.parent);
        //TODO
        //Gerer la mort

    }

    public void Act()
    {
        if (leverInRange != null)
        {
            leverInRange.Activate();

        }
    }
}
