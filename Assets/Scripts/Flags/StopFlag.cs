using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFlag : Flag
{
    public override void ActivateFlag(Collider other)
    {
        base.ActivateFlag(other);
		if (toUse && !isOnCooldown) {
			other.GetComponent<PlayerControl> ().Stop ();
		}


		isOnCooldown = true;
    }
}
