using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actionnable {

	public GameObject door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();

		if (isActive) {
			door.SetActive (false);
		} else {
			door.SetActive (true);
		}

	}
}
