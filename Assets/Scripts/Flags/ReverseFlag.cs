﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseFlag : Flag
{
    public override void ActivateFlag(Collider other)
    {
        base.ActivateFlag(other);
        if (toUse)
            other.GetComponent<PlayerControl>().Reverse();
    }
}
