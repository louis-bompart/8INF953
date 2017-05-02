using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	public AudioClip warpSound;
	public AudioSource noise;
	// Use this for initialization
	void Awake () {
		noise.PlayOneShot (warpSound);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
