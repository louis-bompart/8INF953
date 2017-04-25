using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFlag : Flag
{
    public override void ActivateFlag(Collider other)
    {
        base.ActivateFlag(other);
        if (toUse)
            other.GetComponent<PlayerControl>().Die(transform);
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
