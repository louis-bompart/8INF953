using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFlag : Flag
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
            other.GetComponent<PlayerControl>().Stop();
        base.OnTriggerEnter(other);
    }
}
