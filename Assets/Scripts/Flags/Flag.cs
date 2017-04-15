using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flag : MonoBehaviour
{

    protected bool isUsed;
    // Use this for initialization
    void Start()
    {
        isUsed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        isUsed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isUsed = false;
    }

    public override bool Equals(object obj)
    {
        //TODO DUH
        return this.GetType() == obj.GetType();
    }
}
