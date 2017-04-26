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
    public override void Update()
    {
        base.Update();
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    public void OnTriggerEnter(Collider collider)
    {
        base.isActive = true;
		Debug.Log (isActive);
    }

    // On regarde les collisions entre plaque et joueur/cadavre
    void OnTriggerStay(Collider collider)
    {
        base.isActive = true;
    }

	public override void OnDisable() {
		base.isActive = false;
		base.OnDisable ();

	}

    // On regarde si le joueur sort de la plaque
    public void OnTriggerExit(Collider collider)
    {
        isActive = false;
    }
}
