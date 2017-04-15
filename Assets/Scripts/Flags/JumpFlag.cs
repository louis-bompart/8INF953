using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlag : Flag
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
        base.OnTriggerEnter(other);
        other.GetComponent<PlayerControl>().Jump();
    }
}
