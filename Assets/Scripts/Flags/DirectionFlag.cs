using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionFlag : Flag
{

    public enum Direction
    {
        Left,
        Right
    }

    public Direction direction;
    public override void ActivateFlag(Collider other)
    {
        base.ActivateFlag(other);
        if (toUse)
        {
            switch (direction)
            {
                case Direction.Left:
                    other.GetComponent<PlayerControl>().accelerationDirection = Vector3.back;
                    break;
                case Direction.Right:
                    other.GetComponent<PlayerControl>().accelerationDirection = Vector3.forward;
                    break;
                default:
                    break;
            }
            other.GetComponent<PlayerControl>().isStopped = false;
        }
    }
}
