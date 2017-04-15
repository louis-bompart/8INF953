using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseFlag : Flag
{
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
        if (!isUsed)
            other.GetComponent<PlayerControl>().Reverse();
        base.OnTriggerEnter(other);
    }
}
