using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Actionner : MonoBehaviour
{

    //isActive correspond à l'activation ou non de l'actionneur
    public bool isActive;

    // Animator de l'objet
    protected Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
		Debug.Log (isActive);
        //anim.SetBool("Activated", isActive);
    }
}