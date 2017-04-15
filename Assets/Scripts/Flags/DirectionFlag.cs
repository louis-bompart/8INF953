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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        if(!isUsed)
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
    }
}
