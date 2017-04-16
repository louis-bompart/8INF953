using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Actionner
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Activated", isActive);
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerEnter(Collider collider)
    {
        isActive = true;
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerStay(Collider collider)
    {
        isActive = true;
    }

    // On regarde si le joueur sort de la plaque
    void OnTriggerExit(Collider collider)
    {
        isActive = false;
    }
}
