using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActFlag : Flag {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Act(){
		//TODO
	}

	public override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);
		Act ();
	}

}
