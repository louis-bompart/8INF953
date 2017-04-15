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
        Jump(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Jump(other);
    }

    private void Jump(Collider other)
    {
        if (!isUsed)
            isUsed = other.GetComponent<PlayerControl>().Jump();
    }
}
