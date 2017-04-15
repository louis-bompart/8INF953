using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool Equals(object obj)
    {
        //TODO DUH
        return this.GetType() == obj.GetType();
    }
}
