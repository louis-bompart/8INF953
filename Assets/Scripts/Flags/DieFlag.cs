using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFlag : Flag {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public override void OnTriggerEnter(Collider other)
    {
        if (!isUsed)
            other.GetComponent<PlayerControl>().Die();
        base.OnTriggerEnter(other);
    }


	/*private void Suicide()
	{
		Instantiate<GameObject>(corpse, transform.position, transform.rotation, transform.parent);
		transform.position=lastWP.transform.position;
		rb.velocity = Vector3.zero;
		timeSinceLastSuicide = Time.time;
		//Instantiate a deadPlayer on the player coordinate
		//Move player to its start wp and set speed and acceleration to 0

	}
	*/
}
