using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actionner : MonoBehaviour
{

    //isActive correspond à l'activation ou non de l'actionneur
    public bool isActive;

    // Animator de l'objet
    protected Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}