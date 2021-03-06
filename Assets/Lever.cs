﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Actionner
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    // Active / Desactive le levier selon si il est Desactive/Active
    public void Activate()
    {
        isActive = !isActive;
		Debug.Log ("I ACTIVATE MYSELF !!!!");
        //anim.SetBool("Activated", isActive);
    }


    // On regarde les collisions entre plaque et joueur/cadavre
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            collider.GetComponent<PlayerControl>().leverInRange = this;
		Debug.Log ("Lever In Range");
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) 
            collider.GetComponent<PlayerControl>().leverInRange = this;
    }

    // On regarde si le joueur sort de la plaque
	public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            collider.GetComponent<PlayerControl>().leverInRange = null;
    }
}
