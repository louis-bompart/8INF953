using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlag : Flag
{
    public override void ActivateFlag(Collider other)
    {
        base.ActivateFlag(other);
		if (toUse && !isOnCooldown)
        {
            Jump(other);
        }


		isOnCooldown = true;
    }



    private void Jump(Collider other)
    {
        toDestroy = other.GetComponent<PlayerControl>().Jump();
    }
}
