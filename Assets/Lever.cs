using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Actionner
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Active / Desactive le levier selon si il est Desactive/Active
    public void Activate()
    {
        isActive = !isActive;
        anim.SetBool("Activated", isActive);
    }


    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            collider.GetComponent<PlayerControl>().actionnerInRange = this;
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) 
            collider.GetComponent<PlayerControl>().actionnerInRange = this;
    }

    // On regarde si le joueur sort de la plaque
    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            collider.GetComponent<PlayerControl>().actionnerInRange = null;
    }
}
